import type { CreateBidAttrs, Bid } from '$services/types';
import { bidHistoryKey, itemsKey, itemsByPriceKey } from '$services/keys';
import { client, withLock } from '$services/redis';
import { DateTime } from 'luxon';
import { getItem } from './items';

export const createBid = async (attrs: CreateBidAttrs) => {
	return await withLock(attrs.itemId, async (lockedRClient: typeof client, signal: any) => {
		// 1) Fetching the item
		// 2) Doing validation
		// 3/ Writing some data

		const item = await getItem(attrs.itemId);

		if (!item) {
			throw new Error('Item does not exist');
		}

		if (item.price >= attrs.amount) {
			throw new Error('Bid too low');
		}

		if (item.endingAt.diff(DateTime.now()).toMillis() < 0) {
			throw new Error('Item closed to bidding');
		}

		const serialized = serializeHistory(
			attrs.amount,
			attrs.createdAt.toMillis()
		);

		// Use Proxy to check lock is expired
		// if (signal.expired) {
		// 	throw new Error('Lock expired, cant write any more data');
		// }

		return await Promise.all([
			lockedRClient.rPush(bidHistoryKey(attrs.itemId), serialized),
			lockedRClient.hSet(itemsKey(item.id), {
				bids: item.bids + 1,
				price: attrs.amount,
				highestBidUserId: attrs.userId
			}),
			lockedRClient.zAdd(itemsByPriceKey(), {
				value: item.id,
				score: attrs.amount
			}),
		]);
	});
};

const createBidV1UsingWatch = async (attrs: CreateBidAttrs) => {
	return client.executeIsolated(async (isolated) => {
		await isolated.watch(itemsKey(attrs.itemId));

		const item = await getItem(attrs.itemId);

		if (!item) {
			throw new Error('Item does not exist');
		}

		if (item.price >= attrs.amount) {
			throw new Error('Bid too low');
		}

		if (item.endingAt.diff(DateTime.now()).toMillis() < 0) {
			throw new Error('Item closed to bidding');
		}

		const serialized = serializeHistory(
			attrs.amount,
			attrs.createdAt.toMillis()
		);

		return isolated
			.multi()
			.rPush(bidHistoryKey(attrs.itemId), serialized)
			.hSet(itemsKey(item.id), {
				bids: item.bids + 1,
				price: attrs.amount,
				highestBidUserId: attrs.userId
			})
			.zAdd(itemsByPriceKey(), {
				value: item.id,
				score: attrs.amount
			})
			.exec();
	});
};

export const getBidHistory = async (itemId: string, offset = 0, count = 10): Promise<Bid[]> => {
	const startIndex = -1 * offset - count;
	const endIndex = -1 - offset;

	const range = await client.lRange(
		bidHistoryKey(itemId),
		startIndex,
		endIndex
	);

	return range.map(history => deserializeHistory(history));
};

const serializeHistory = (amount: number, createdAt: number) => {
	return `${amount}:${createdAt}`;
};

const deserializeHistory = (history: string) => {
	const [amount, createdAt] = history.split(':');
	return {
		amount: parseFloat(amount),
		createdAt: DateTime.fromMillis(parseInt(createdAt))
	};
};