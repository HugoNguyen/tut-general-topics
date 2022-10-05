import express from 'express';
import { json } from 'body-parser';
// This is enable cors
const cors = require('cors')
import { indexLongServicesRouter } from './routes/long-services/index';
import { postLongServicesRouter } from './routes/long-services/post';
import { pingSocketRouter } from './routes/sockets/ping';

const { Server } = require("socket.io");
const http = require('http');
const app = express();
app.set('trust proxy', true);
app.use(json());

// Enable All CORS Request
app.use(cors({
  origin: '*'
}));

// routes
app.use(indexLongServicesRouter);
app.use(postLongServicesRouter);

app.use(pingSocketRouter);

// create Server
const server = http.createServer(app);

// init ws server
const io = new Server(server, {
    cors: {
      origin: "*"
    },
    path: `/${process.env.WEBSOCKET_PATH || 'socket.io'}/`
  });

io.on('connection', (socket: any) => {
    console.log(`a user connected: ${socket.id}`);
});

export {
    app,
    server,
    io,
};