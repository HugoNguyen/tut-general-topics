import io from 'socket.io-client';
import env from '../enviroment';

const socketServerUrl = env.socketServerUrl || 'http://localhost:8000';

const socket = io.connect(socketServerUrl);

socket.on('requestClientAuth', () => {
    socket.emit('clientAuth', 'ui-zxcvbn123456');
});

export default socket;