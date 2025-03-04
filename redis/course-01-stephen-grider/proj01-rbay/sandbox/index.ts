import 'dotenv/config';
import { client } from '../src/services/redis';

const run = async () => {
    const data: any = {
        color: 'red',
        year: 1950,
        engine: { cylinders: 8 },
        owner: '',
        service: '',
    };
    await client.hSet('car', data);

    const car = await client.hGetAll('car');

    console.log(car);

    // Get not existing key
    const notExistingKey = await client.hGetAll('car#100000');
    if (Object.keys(notExistingKey).length === 0) {
        console.log('Car not found, respond with 404');
    } else {
        console.log('Car found');
    }
};
run();
