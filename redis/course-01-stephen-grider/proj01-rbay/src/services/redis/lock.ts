import { randomBytes, sign } from "crypto";
import { client } from "./client";

export const withLock = async (key: string, cb: (redisClient: Client, signal: any) => any) => {
	// Initialize a few variables to control retry behavior
	const retryDelayMs = 100;
	let retries = 20;

	// Generate a random value to store at a lock key
	const token = randomBytes(6).toString('hex');
	// Crate the lock key
	const lockKey = `lock:${key}`;

	// Set up a while loop to implement the retry behavior
	while (retries > 0) {
		retries--;

		// Try to do a SET NX operation
		const acquired = await client.set(lockKey, token, {
			NX: true,
			PX: 2000,
		});

		if (!acquired) {
			// ELSE brief pause (retryDelayMs) and then retry
			await pause(retryDelayMs);
			continue;
		}

		// IF the set is successfull, then run the callback
		// Finally, Unset the lock
		try{
			const signal = { expired: false };
			setTimeout(() => {
				signal.expired = true;
			}, 2000);
			const proxiedClient = buildClientProxy(2000);
			const result = await cb(proxiedClient, signal);
			return result;
		} finally {
			// await client.del(lockKey);
			await client.unlock(lockKey, token);
		}
	}
};

type Client = typeof client;

const buildClientProxy = (timeoutMs: number) => {
	const startTime = Date.now();

	const handler = {
		get(target: Client, prop: keyof Client) {
			if (Date.now() >= startTime + timeoutMs) {
				throw new Error('Lock has expired');
			}

			const value = target[prop];
			return typeof value === 'function' ? value.bind(target) : value;
		}
	}

	return new Proxy(client, handler) as Client;
};

const pause = (duration: number) => {
	return new Promise((resolve) => {
		setTimeout(resolve, duration);
	});
};
