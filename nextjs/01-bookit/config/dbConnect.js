import mongoose from 'mongoose';
import getConfig from "next/config";

const dbConnect = () => {
    const { serverRuntimeConfig: { DB_LOCAL_URI } } = getConfig();

    if (mongoose.connection.readyState >= 1) {
        return;
    }

    mongoose.connect(DB_LOCAL_URI, {
        useNewUrlParser: true,
        useUnifiedTopology: true
    }).then(con => console.log('Connected to local db'));
}

export default dbConnect;
