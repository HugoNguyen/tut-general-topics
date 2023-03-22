const  http = require('http');

// 3rd party module, ws!
const websocket = require('ws');

const server = http.createServer((req, res) => {
    res.end('I am connected');
});

const wss = new websocket.Server({ server });

// This allows you to inspect/modify the headers before they are sent.
wss.on('headers', (msg) => {
    console.log(msg);
});

// Emitted when the handshake is complete.
// Useful for parsing authority headers, cookie headers, and other information.
// request is the http GET request sent by the client
wss.on('connection', (ws, req) => {
    ws.send('Welcome to the websocket server!!');
    ws.on('message', (msg) => {
        console.log(msg);
    });
});

server.listen(8000);