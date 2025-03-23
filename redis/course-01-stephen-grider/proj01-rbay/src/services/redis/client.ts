import { createClient, defineScript } from 'redis';

const client = createClient({
	socket: {
		host: process.env.REDIS_HOST,
		port: parseInt(process.env.REDIS_PORT)
	},
	password: process.env.REDIS_PW,
	scripts: {
		addOneAndStore: defineScript({
			NUMBER_OF_KEYS: 1,
			SCRIPT: `
				return redis.call('SET', KEYS[1], 1 + tonumber(ARGV[1]))
			`,
			transformArguments(key: string, value: number) {
				return [key, value.toString()];
				// ['books:count', '5]
				// EVALSHA <ID> 1 'books:count' '5'
			},
			transformReply(reply: any) {
				return reply;
			}
		})
	}
});

client.on('connect', async () => {
	client.addOneAndStore('books:count', 5);
	const result = await client.get('books:count');
	console.log(result); // 6
});

client.on('error', (err) => console.error(err));
client.connect();

export { client };
