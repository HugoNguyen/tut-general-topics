import io from 'socket.io-client';

const socket = io.connect('http://localhost:8000');

socket.on('requestClientAuth', () => {
    socket.emit('clientAuth', 'ui-zxcvbn123456');
});

export default socket;