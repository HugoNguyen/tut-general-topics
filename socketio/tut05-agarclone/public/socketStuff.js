let socket = io.connect('/');

socket.on('init', data => {
    // uiStuff.orbs
    orbs = data.orbs;
});