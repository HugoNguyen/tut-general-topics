# Resource
- https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API
- https://github.com/websockets/ws/blob/master/doc/ws.md#event-headers

# Projects

## server
- use ws module from npm
- run at port 8000

### Events
- headers: Emitted before the response headers are written to the socket as part of the handshake. This allows you to inspect/modify the headers before they are sent.

- connection: Emitted when the handshake is complete. request is the http GET request sent by the client. Useful for parsing authority headers, cookie headers, and other information.

### Methods
- websocket.send(data[, options][, callback])

## client
- just a html file, has call to ws server by ws api

## Events
- message / onmessage
    The message event is fired when data is received through a WebSocket

