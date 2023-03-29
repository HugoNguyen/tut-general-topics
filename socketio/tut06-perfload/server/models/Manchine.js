class Machine {
    constructor(data = {}){
        this.macA = data.macA;
        this.cpuLoad = data.cpuLoad;
        this.freeMem = data.freeMem;
        this.totalMem = data.totalMem;
        this.usedMem = data.usedMem;
        this.memUseage = data.memUseage;
        this.osType = data.osType;
        this.upTime = data.upTime;
        this.cpuModel = data.cpuModel;
        this.numCores = data.numCores;
        this.cpuSpeed = data.cpuSpeed;
        this.isActive = data.isActive;
    }
}

module.exports = Machine;
