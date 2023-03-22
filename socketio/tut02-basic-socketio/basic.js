// we need http because we don't have express
const  http = require('http');

//3rd party
const socketio = require('socket.io');

// make an http server with node!
const server = http.createServer((req, res) => {
    res.end('I am connected');
});

const io = socketio(server, {
    cors: {
        origin: "*",
    },
});

io.on('connection', (socket, req) => {
    // ws.send = socket.emit
    socket.emit('welcome', 'Welcome to the websocket server!!');
    socket.on('message', (msg) => {
        console.log(msg);
    });
});

server.listen(8000);