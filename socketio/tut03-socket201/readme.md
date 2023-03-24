# Socket 201
- Chat app

# Run Chat app
- Run chat server
    `npm run start:chat`

- Client, http://localhost:9000/chat.html

# Concepts

## Namespaces
- A Namespace is a communication channel that allows you to split the logic of your application over a single shared connection (also called "multiplexing").

- namespace#events
- namespace#rooms
- namespace#middleware

socket.io-server {
    namespaces: [
        namespace: {
            path: '/'
            rooms: []
        },
        namespace: {
            path: '/orders'
            rooms: []
        },
        namespace: {
            path: '/users'
            rooms: []
        }
    ]
}

## Tips
### Server
1. Send an event from the server to this socket only:

socket.emit()
socket.send()


2. Send an event from a socket to a room:

NOTE: remember, this will not go to the sending socket

socket.to(roomName).emit()
socket.in(roomName).emit()


3. Because each socket has it's own room, named by it's socket.id, a socket can send a message to another socket:

socket.to(anotherSocketId).emit('hey');
socket.in(anotherSocketId).emit('hey');


4. A namespace can send a message to any room:

io.of(aNamespace).to(roomName).emit()
io.of(aNamespace).in(roomName).emit()


5. A namespace can send a message to the entire namespace

io.emit()
io.of('/').emit()
io.of('/admin').emit()

# Socket
- a socket is the calss for interacting with browser clients. A socket belongs to a certain Namespace (default /)
- a socket can join and leave a room.

socket {
    id,
    rooms,
    join: fn(room),
    leave: fn(room),
    to: fn(room),
}

# Flow of message move
1/ Send an event from server to this socket only
socket.emit()
socket.send()

2/ Send an event from a socket to a room
socket.to(room).emit()
socket.in(room).emit()

3/ Send msg from a socket to another socket
socket.to(anotherSocketId).emit()

4/ A namespace can send a msg to any room
io.of(namespace).to(room).emit()
io.of(namespace).in(room).emit()

5/ A namespace can send a message to the entire namespace
io.emit()
io.of('/').emit()
io.of(namespace).emit()