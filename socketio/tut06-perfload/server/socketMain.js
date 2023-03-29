const { db } = require('./dbMain');
const Machine = require('./models/Manchine');

function socketMain(io, socket) {
    console.log(`A socket connected! ${socket.id}`);
    
    let macA;

    socket.emit('requestClientAuth');

    socket.on('clientAuth', async(key) => {
        if(key === 'clients-abczyz123456') {
            // valid node-client
            socket.join('clients');
            console.log(`socket ${socket.id} join room clients`);
        } else if(key === 'ui-zxcvbn123456') {
            // valid ui client has joined
            socket.join('ui');
            console.log(`socket ${socket.id} join room ui`);

            // on load, assume that all machines are offine
            const machines = (await getMachines()).map(q => ({...q, isActive: false }));
            io.to('ui').emit('data', machines);
        } else {
            // an invalid client has joined. Goodbye
            socket.disconnect(true);
            console.log(`socket ${socket.id} disconnected`);
        }
    });

    socket.on('disconnect', async() => {
        const updateObj = await updateMachineActiveStatus(macA, false);
        io.to('ui').emit('data', updateObj);
    });
    
    // a machine has connected, check to see if it's new
    // if it is, add it
    socket.on('initPerfData', async (data) => {
        // update our socket connect fn scoped variable
        macA = data.macA;

        // save the machine
        const rs = await checkAndAdd(data);
        console.log(`initPerfData| ${rs} the macA ${macA}`);
    });

    socket.on('perfData', async (data) => {
        // console.log("Tick...");
        io.to('ui').emit('data', data);
    });
}

async function checkAndAdd(data){
    try {
        exist = await db.getData(`/machine-collection/${data.macA}`);
        return 'found';
    } catch(err) {
        const newMachine = new Machine(data);
        await db.push(`/machine-collection/${data.macA}`, newMachine, false);
        return 'added';
    }
}

async function getMachines(){
    try {
        const col = await db.getData('/machine-collection');
        return Object.entries(col).map(([key, value]) => value)
    } catch(err) {
        return [];
    }
}

async function updateMachineActiveStatus(macA, isActive) {
    let exist;
    
    try {
        exist = await db.getData(`/machine-collection/${macA}`);
        exist = {...exist, isActive};
        await db.push(`/machine-collection/${data.macA}`, exist, false);
    } catch(err) {
    }

    return exist;
}

module.exports = socketMain;
