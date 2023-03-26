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
    setInterval(() => {
        // waiting init of player location
        if(!player.xVector || !player.yVector) {
            return;
        }
        socket.emit('tick', {
            xVector: player.xVector,
            yVector: player.yVector,
        });
    }, 33);
});

socket.on('tock', data => {
    // uiStuff.players
    players = data.players;
});

socket.on('orbSwitch', data => {
    orbs.splice(data.orbIndex, 1, data.newOrb);
});

socket.on('tickTock', data => {
    // uiStuff.players
    player.locX = data.playerX;
    player.locY = data.playerY;
});