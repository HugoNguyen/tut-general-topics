const express = require('express');
const app = express();
const socketio = require('socket.io');

let namespaces = require('./src/data/namespaces');

app.use(express.static(__dirname + '/public'));

const expressServer = app.listen(9000);
const io = socketio(expressServer);

io.on('connection', socket => {
    // build an array to send back with the img and endpoint for each NS
    const nsData = namespaces.map(q => {
        return {
            img: q.img,
            endpoint: q.endpoint
        }
    });

    // send the nsData back to the client. We need to use socket, NOT is, becasue we wnat it to
    // go to jsut this client
    socket.emit('nsList', nsData);
});

// loop through each ns and listen for a connection
namespaces.forEach(ns => {
    io.of(ns.endpoint).on('connection', nsSocket => {
        const username = nsSocket.handshake.query.username;

        // console.log(`${nsSocket.id} has join ${ns.endpoint}`);
        // a socket has connected to one of out chatgroup ns
        // send that ns group info back
        nsSocket.emit('nsRoomLoad', ns.rooms);
        nsSocket.on('joinRoom', async (roomToJoin, numberOfUsersCallback) => {
            // deal with history ... once we have it
            const roomToLeave = Array.from(nsSocket.rooms)[1];
            nsSocket.leave(roomToLeave);
            await updateUsersInRoom(ns, roomToLeave);
            nsSocket.join(roomToJoin);

            // Namespace.clients() is renamed to Namespace.allSockets() and now returns a Promise
            // io.of('/wiki').in(roomToJoin).
            // namespace.allSockets(): Promise<Set<SocketId>>
            // const clientIds = await io.of('/wiki').in(roomToJoin).allSockets();
            
            // if (numberOfUsersCallback) {
            //     numberOfUsersCallback(clientIds.size);
            // }
            // We don't need a callback to update nr of client in this room

            const nsRoom = ns.rooms.find(room => {
                return room.roomTitle === roomToJoin;
            });

            nsSocket.emit('historyCatchUp', nsRoom.history);

            await updateUsersInRoom(ns, roomToJoin);
        });

        nsSocket.on('newMessageToServer', msg => {
            const fullMsg = {
                text: msg,
                time: Date.now(),
                username: username,
                avatar: 'https://via.placeholder.com/30'
            }

            // Send this message to All the sockets that are in the room that THIS socket is in
            // socket.room // Set { <socket.id>, "room1" }
            // the user will be in the 2nd room in the object list
            // this is becuase the socket ALWAYS joins its own room on connection
            // get the keys
            const roomTitle = Array.from(nsSocket.rooms)[1];

            // we need to find the Room object for this room
            const nsRoom = ns.rooms.find(room => {
                return room.roomTitle === roomTitle;
            });
            nsRoom.addMessage(fullMsg);
            io.of(ns.endpoint).to(roomTitle).emit('messageToClients', fullMsg);
        })
    });
});

async function updateUsersInRoom(ns, roomName){
    // Send back the number of users in this room to ALL sockets connected to this room
    const clientIds = await io.of(ns.endpoint).in(roomName).allSockets();
    io.of(ns.endpoint).in(roomName).emit('updateMembers', clientIds.size)
}