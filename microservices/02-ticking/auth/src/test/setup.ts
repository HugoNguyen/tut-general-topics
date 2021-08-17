import { MongoMemoryServer } from 'mongodb-memory-server';
import mongoose from 'mongoose';
import { app } from '../app';

let mongo: any;

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