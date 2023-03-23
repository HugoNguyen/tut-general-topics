#Resources
- https://socket.io/docs/v4/
- https://cdnjs.com/libraries/socket.io

# Socket.io
- A node.js server
- a js client lib for browser (can run fom Node.js)
    => client can be a node.js server

- Offer many feature that WS doesn't

- Socket.io use WS when possible


# Proj 1: Basic
- server, run at port 8000
    `npm run start:basic`
- client, a html run via live serve addin

# Proj 2: Chat
- sever is express, sever socket.io and client
- run on port 9000
    `npm run start:chat`
- client open chat.html[`localhost:9000/chat.html`]

# Socket Heartbeat
- Client
`js
            setInterval(() => {
                const start = Date.now();

                // volatile, so the packet will be discarded if the socket is not connected
                socket.volatile.emit("ping", () => {
                    const latency = Date.now() - start;
                    console.log(`Pong was sent to the server | ${latency}`);
                });
            }, 5000);

`

- Server
`js
    socket.on("ping", (cb) => {
        if (typeof cb === "function"){
            cb();
        }
    });
`