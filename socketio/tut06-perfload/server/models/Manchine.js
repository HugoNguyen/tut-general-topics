class Machine {
    constructor(data = {}){
        this.macA = data.macA;
        this.cpuLoad = data.cpuLoad;
        this.freeMem = data.freeMem;
        this.totalMem = data.totalMem;
        this.usedMem = data.usedMem;
        this.memUsage = data.memUsage;
        this.osType = data.osType;
        this.upTime = data.upTime;
        this.cpuModel = data.cpuModel;
        this.numCores = data.numCores;
        this.cpuSpeed = data.cpuSpeed;
    }
}

module.exports = Machine;
