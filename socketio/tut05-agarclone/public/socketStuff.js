let socket = io.connect('/');

// this fn is called when user clicks on the start button
function init() {
    // start drawing the screen
    draw();

    // call the init event when the client is ready for the data
    socket.emit('init', {
        playerName: player.name,
    })
}

socket.on('initReturn', data => {
    // uiStuff.orbs
    orbs = data.orbs;
});