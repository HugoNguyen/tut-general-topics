import { client } from '$services/redis';
import { deserialize } from './deserialize';
import { itemsIndexKey } from '$services/keys';

export const searchItems = async (term: string, size: number = 5) => {
    // Pre-processing
    const cleaned = term
        .replaceAll(/[^a-zA-Z0-9]/g, ' ')
        .trim()
        .split(' ')
        .map(word => word ? `%${word}%` : '')
        .join(' ');

    // Look at cleaned and make sure it is valid
    if  (cleaned === '') {
        return [];
    }

    // Apply weight, to show more relevant results in field Name first
    const query = `(@name:(${cleaned}) => {$weight: 5.0}) | @description:(${cleaned}))`;

    // Use the client to do an actual search
    const results = await client.ft.search(
        itemsIndexKey(),
        query,
        {
            LIMIT: {
                from: 0,
                size: size,
            }
        }
    );

    // Deserialize and return the result
    return results.documents.map(({ id, value }) => deserialize(id, value as any));
};
