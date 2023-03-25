const express = require('express');
const app = express();
const socketio = require('socket.io');

app.use(express.static(__dirname + '/public'));

const expressServer = app.listen(9000);

/**
 * <script src="/[path]/socket.io.js"></script>
 * Ex: <script src="/socket.io/socket.io.js"></script>
 * serveClient allow client access socket.io.js
 */
const io = socketio(expressServer);

io.on('connection', socket => {
    socket.emit('messageFromServer', {
        data: 'Welcome to the socketio server'
    });
    socket.on('messageToServer', dataFromClient => {
        console.log(dataFromClient);
    });

    // Chat logic
    socket.on('newMessageToServer', msg => {
        // console.log(msg);
        io.emit('messageToClients', { text: msg.text });
        // io.of('/').emit('messageToClients', { text: msg.text });
    });
});

io.of('/admin').on('connection', socket => {
    console.log(`Someone connected to namespace /admin`);
    io.of('/admin').emit('welcome', "Welcome to the admin channel!");
});
