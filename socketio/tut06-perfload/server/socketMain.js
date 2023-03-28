function socketMain(io, socket) {
    console.log(`A socket connected! ${socket.id}`);
    
    socket.on('clientAuth', key => {
        if(key === 'clients-abczyz123456') {
            // valid node-client
            socket.join('clients');
        } else if(key === 'ui-zxcvbn123456') {
            // valid ui client has joined
            socket.join('ui');
        } else {
            // an invalid client has joined. Goodbye
            socket.disconnect(true);
        }
    });
    
    socket.on('perfData', data => {
        console.log(data);
    });
}

module.exports = socketMain;
