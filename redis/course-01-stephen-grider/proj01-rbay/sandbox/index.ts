import 'dotenv/config';
import { client } from '../src/services/redis';
import { Bidder } from './bid';

// const run = async () => {
//     const data: any = {
//         color: 'red',
//         year: 1950,
//         engine: { cylinders: 8 },
//         owner: '',
//         service: '',
//     };
//     await client.hSet('car', data);

//     const car = await client.hGetAll('car');

//     console.log(car);

//     // Get not existing key
//     const notExistingKey = await client.hGetAll('car#100000');
//     if (Object.keys(notExistingKey).length === 0) {
//         console.log('Car not found, respond with 404');
//     } else {
//         console.log('Car found');
//     }
// };

// const run = async () => {
//     await client.hSet('car1', {
//         color: 'red',
//         year: 1950,
//     });
//     await client.hSet('car2', {
//         color: 'green',
//         year: 1955,
//     });
//     await client.hSet('car3', {
//         color: 'blue',
//         year: 1960,
//     });
//     const result = await Promise.all([
//         client.hGetAll('car1'),
//         client.hGetAll('car2'),
//         client.hGetAll('car3'),
//     ]);
//     console.log(result);
// }

const run = async () => {
    return await Bidder();
}

run();
