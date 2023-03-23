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
const io = socketio(expressServer, {
    path: "/socket.io",
    serveClient: true
});

io.on('connection', socket => {
    socket.emit('messageFromServer', {
        data: 'Welcome to the socketio server'
    });
    socket.on('messageToServer', dataFromClient => {
        console.log(dataFromClient);
    });

    // the heartbeat mechanism
    /*
    socket.on("ping", (cb) => {
        if (typeof cb === "function"){
            cb();
        }
    });
    */

    // Chat logic
    socket.on('newMessageToServer', msg => {
        // console.log(msg);
        io.emit('messageToClients', { text: msg.text });
    });
});

