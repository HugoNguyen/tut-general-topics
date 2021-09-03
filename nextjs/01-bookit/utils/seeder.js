// Error:  Cannot use import statement outside a module
// import Room from '../models/rooms';
const mongoose = require('mongoose');
const Room = require('../models/rooms');
const rooms = require('../data/rooms');

mongoose.connect(`mongodb://root:root@localhost:27017/bookit?authSource=admin`, {
    useNewUrlParser: true,
    useUnifiedTopology: true
}).then(con => console.log('Connected to local db'));

const seedRooms = async() => {
    try {
        await Room.deleteMany();
        console.log('Rooms are deleted');

        await Room.insertMany(rooms);
        console.log('All Rooms are added');

    } catch (error) {
        console.log(error.message);
    } finally {
        process.exit();
    }
}

seedRooms();

