// Wher all our main socket stuff will go
const io = require('../servers').io;
const checkForOrbCollisions = require('./checkCollisions').checkForOrbCollisions;
const checkForPlayerCollisions = require('./checkCollisions').checkForPlayerCollisions;

// ==========CLASSES===========
const Player = require('./classes/Player');
const PlayerData = require('./classes/PlayerData');
const PlayerConfig = require('./classes/PlayerConfig');
const Orb = require('./classes/Orb');
const orbs = [];
const players = [];
const settings = {
    defaultOrbs: 50,
    defaultSpeed: 6,
    defaultSize: 6,
    // as player gets bigger, the zoom needs to go out
    defaultZoom: 1.5,
    worldWidth: 500,
    worldHeight: 500,
}

initGame();

// issue a message to EVERY connected socket every 30 fps
setInterval(() => {
    if(players.length > 0) {
        io.to('game').emit('tock', {
            players,
        });
    }
}, 33); // there are 30 33s in 1000 mili, or 1/30th of a second, or 1 of 30fps

io.sockets.on('connect', socket => {
    let player;
    // a player has connected
    socket.on('init', data => {
        // add the player to the game room
        socket.join('game');

        // make a playerConfig object
        let playerConfig = new PlayerConfig(settings);
        // make a playerData object
        let playerData = new PlayerData(data.playerName, settings);
        // make a master player object to hold both
        player = new Player(socket.id, playerConfig, playerData);

        // issue a message to THIS client with it's loc 30/sec
        setInterval(() => {
            socket.emit('tickTock', {
                playerX: player.playerData.locX,
                playerY: player.playerData.locY,
                score: player.playerData.score,
            });
        }, 33);

        socket.emit('initReturn', { orbs });
        players.push(playerData);
    });

    // the client sent over a tick
    // That means we know what direction to move the socket
    socket.on('tick', data => {
        if(!player) return;

        const speed = player.playerConfig.speed;

        // update the player config obj with the new direction in data
        // and at the same time create a local variable for this callback for readability
        const xV = player.playerConfig.xVector = data.xVector;
        const yV = player.playerConfig.yVector = data.yVector;

        if ((player.playerData.locX < 5 && player.playerData.xVector < 0) || (player.playerData.locX > settings.worldWidth) && (xV > 0)) {
            player.playerData.locY -= speed * yV;
        } else if ((player.playerData.locY < 5 && yV > 0) || (player.playerData.locY > settings.worldHeight) && (yV < 0)) {
            player.playerData.locX += speed * xV;
        } else {
            player.playerData.locX += speed * xV;
            player.playerData.locY -= speed * yV;
        }

        // Orbs collision
        let capturedOrb = checkForOrbCollisions(player.playerData, player.playerConfig, orbs, settings);
        capturedOrb.then(data => {
            // then runs if resolve runs! a collision happended!
            // console.log(`orb collision!!! at ${data}`);
            // emit to all sockets the orb to replace
            const orbData = {
                orbIndex: data,
                newOrb: orbs[data],
            }
            // every socket needs to know the leaderBoard has changed
            io.sockets.emit('updateLeaderBoard', getLeaderBoard());
            io.sockets.emit('orbSwitch', orbData);
        }).catch(() => {
            // catch runs if the reject runs!
            // console.log("no collision!!!");
        })

        // Player collision
        let playerDeath = checkForPlayerCollisions(player.playerData, player.playerConfig, players, player.socketId);
        playerDeath.then(data => {
            // console.log('player collision');
            // every socket needs to know the leaderBoard has changed
            io.sockets.emit('updateLeaderBoard', getLeaderBoard());
        }).catch(() => {
            // console.log('no player collision');
        })
    });
})

function getLeaderBoard() {
    // sort players in desc order
    players.sort((a,b) => {
        return b.score - a.score;
    });

    let leaderBoard = players.map((currPlayer) => {
        return {
            name: currPlayer.name,
            score: currPlayer.score,
        }
    });

    return leaderBoard;
}

// Run at the begining of a new game
function initGame() {
    for (let i = 0; i < settings.defaultOrbs; i++) {
        orbs.push(new Orb(settings));
    }
}

module.exports = io;
