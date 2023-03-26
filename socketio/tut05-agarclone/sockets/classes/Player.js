// this is where ALL the data is stored about a given play
class Player {
    constructor(socketId, playerConfig, playerData) {
        this.socketId = socketId;
        this.playerConfig = playerConfig;
        this.playerData = playerData;
    }
}

module.exports = Player;
