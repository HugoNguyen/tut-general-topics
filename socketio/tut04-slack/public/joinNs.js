function joinNs(endpoint) {
    if(nsSocket) {
        // check to see if nsSocket is actually a socket
        nsSocket.close();

        // remove the eventlistener before it's added again
        document.querySelector('.message-form').removeEventListener('submit', formSubmission);
    }

    // nsSocket define at global
    nsSocket = io(endpoint);
    nsSocket.on('nsRoomLoad', nsRoom => {
        const roomList = document.querySelector('.room-list');
        roomList.innerHTML = "";
        nsRoom.forEach(room => {
            let glpyh;
            if (room.privateRoom) {
                glpyh = 'lock';
            } else {
                glpyh = 'globe';
            }
            roomList.innerHTML += `<li class='room'><span class="glyphicon glyphicon-${glpyh}"></span>${room.roomTitle}</li>`;
        })

        // Add a click listener to each room
        const roomNodes = document.getElementsByClassName('room');
        Array.from(roomNodes).forEach(el => {
            el.addEventListener('click', e => {
                joinRoom(e.target.innerText);
            });
        });

        // add room auto ... first time here
        const topRoom = document.querySelector('.room');
        const topRoomName = topRoom.innerText;
        joinRoom(topRoomName);
    });

    nsSocket.on('messageToClients', msg => {
        const newMsg = buildHTML(msg);
        document.querySelector('#messages').innerHTML += newMsg;
    });

    document.querySelector('.message-form').addEventListener('submit', formSubmission);
}

function formSubmission(e) {
    e.preventDefault();
    const newMessage = document.querySelector('#user-message').value;
    nsSocket.emit('newMessageToServer', newMessage);
};

function buildHTML(msg) {
    const convertedDate = new Date(msg.time).toLocaleTimeString();
    const newHtml = `
    <li>
        <div class="user-image">
            <img src="${msg.avatar}" />
        </div>
        <div class="user-message">
            <div class="user-name-time">${msg.username} <span>${convertedDate}</span></div>
            <div class="message-text">${msg.text}</div>
        </div>
    </li>
    `;
    return newHtml;
}