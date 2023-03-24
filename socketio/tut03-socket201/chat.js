const express = require('express');
const app = express();
const socketio = require('socket.io');

app.use(express.static(__dirname + '/public'));

const expressServer = app.listen(9000);
const io = socketio(expressServer);

io.on('connection', socket => {
    socket.emit('messageFromServer', {
        data: 'Welcome to the socketio server'
    });
    socket.on('messageToServer', dataFromClient => {
        console.log(dataFromClient);
    });

    //join room
    socket.join('level1');

    // broadcast a message to users in the room
    // socket.to('level1').emit('joined', `${socket.id} says I have joined the level 1 room!`);
    io.of('/').to('level1').emit('joined', `${socket.id} says I have joined the level 1 room!`);
});

io.of('/admin').on('connection', socket => {
    console.log(`Someone connected to namespace /admin`);
    io.of('/admin').emit('welcome', "Welcome to the admin channel!");
});
