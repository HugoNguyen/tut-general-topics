function joinRoom(roomName) {
    // Send this roomName to the server!!!!
    // nsSocket define at global
    nsSocket.emit('joinRoom', roomName, (newNumberOfMembers) => {
        // we want to update the room member total now that we have joined
        // We don't need a callback to update nr of client in this room
        // document.querySelector('.curr-room-num-users').innerHTML = `${newNumberOfMembers} <span class="glyphicon glyphicon-user"></span>`;
    });

    nsSocket.on('historyCatchUp', history => {
        const messageUl = document.querySelector('#messages');
        messageUl.innerHTML = '';
        history.forEach(msg => {
            const newMsg = buildHTML(msg);
            messageUl.innerHTML += newMsg;
        })

        // Scroll to last recent msg
        messageUl.scrollTo(0, messageUl.scrollHeight);
    })

    nsSocket.on('updateMembers', numMembers => {
        document.querySelector('.curr-room-num-users').innerHTML = `${numMembers} <span class="glyphicon glyphicon-user"></span>`;
        document.querySelector('.curr-room-text').innerText = `${roomName}`;
    });

    let searchBox = document.querySelector('#search-box');
    searchBox.addEventListener('input', e => {
        console.log(e.target.value);
        let messages = Array.from(document.getElementsByClassName('message-text'));
        messages.forEach(msg => {
            if(msg.innerText.toLowerCase().indexOf(e.target.value.toLowerCase()) === -1) {
                // the msg does not contain the user search tearm!
                msg.style.display = 'none';
            } else {
                msg.style.display = 'block';
            }
        })
    });
}