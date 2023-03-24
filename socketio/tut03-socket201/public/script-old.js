const socket = io('http://localhost:9000'); // => /namespace endpoint
const socket2 = io('http://localhost:9000/admin'); // => /admin namespace

socket.on('connect', _ => {
    console.log(`[id:${socket.id}]`);
});

socket2.on('connect', _ => {
    console.log(`[id:${socket2.id}]`);
});

socket2.on('welcome', msg => {
    console.log(msg);
});

socket.on('messageFromServer', dataFromServer => {
    console.log(dataFromServer);
    socket.emit('messageToServer', {
        data: "Data from the client!"
    });
});


document
    .querySelector('#message-form')
    .addEventListener('submit', event => {
        event.preventDefault();
        const newMessage = document.querySelector('#user-message').value;
        socket.emit('newMessageToServer', { text: newMessage });
    });

socket.on('messageToClients', msg => {
    console.log(msg);
    document.querySelector('#messages').innerHTML += `<li>${msg.text}</li>`;
});