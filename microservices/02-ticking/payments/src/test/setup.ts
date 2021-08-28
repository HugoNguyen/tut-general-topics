import { MongoMemoryServer } from 'mongodb-memory-server';
import mongoose from 'mongoose';
import jwt from 'jsonwebtoken';

declare global {
    namespace NodeJS {
        interface Global {
            signin(id?: string): string[];
        }
    }
}

jest.mock('../nats-wrapper');

let mongo: any;

process.env.STRIPE_KEY = 'sk_test_51JRB2gEwfPm4lLW0bXuety3Vof2yayqhZd4CoZHIwVls6w44ri9nRdX3GKYi7KHMbQKD1JgLBltpK3peNYEQzFBp00HzBqTolw';

// Run before all of our test executed
beforeAll(async () => {
    process.env.JWT_KEY = 'test';

    mongo = await MongoMemoryServer.create();
    const mongoUri = mongo.getUri();

    await mongoose.connect(mongoUri, {
        useNewUrlParser: true,
        useUnifiedTopology: true
    });
});


// Run before each of test started
beforeEach(async () => {
    jest.clearAllMocks();
    const collections = await mongoose.connection.db.collections();

    for (let collection of collections) {
        await collection.deleteMany({});
    }
});

// After all test executed
afterAll(async () => {
    await mongo.stop();
    await mongoose.connection.close();
});

global.signin = (id?: string) => {
    // Build a JWT payload. { id, email }
    const payload = {
        id: id || mongoose.Types.ObjectId().toHexString(),
        email: 'test@test.com'
    }
    // Create the JWT!
    const token = jwt.sign(payload, process.env.JWT_KEY!);

    // Build session object { jwt: MY_JWT }
    const session = { jwt: token };

    // Turn that session into JSON
    const sessionJSON = JSON.stringify(session);

    // Take JSON and encode it as base64
    const base64 = Buffer.from(sessionJSON).toString('base64');

    // return a string thats the cookie with the encoded data
    return [`express:sess=${base64}`];

}