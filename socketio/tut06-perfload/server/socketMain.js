const { db } = require('./dbMain');
const Machine = require('./models/Manchine');

function socketMain(io, socket) {
    console.log(`A socket connected! ${socket.id}`);
    
    let macA;

    socket.emit('requestClientAuth');

    socket.on('clientAuth', key => {
        if(key === 'clients-abczyz123456') {
            // valid node-client
            socket.join('clients');
            console.log(`socket ${socket.id} join room clients`);
        } else if(key === 'ui-zxcvbn123456') {
            // valid ui client has joined
            socket.join('ui');
            console.log(`socket ${socket.id} join room ui`);
        } else {
            // an invalid client has joined. Goodbye
            socket.disconnect(true);
            console.log(`socket ${socket.id} disconnected`);
        }
    });
    
    // a machine has connected, check to see if it's new
    // if it is, add it
    socket.on('initPerfData', async (data) => {
        // update our socket connect fn scoped variable
        macA = data.macA;

        // save the machine
        await checkAndAdd(data);
    });

    socket.on('perfData', async (data) => {
        // console.log("Tick...");
        io.to('ui').emit('data', data);
    });
}

async function checkAndAdd(data){
    let machineCollection = [];
    
    try {
        machineCollection = await db.getData('/machine-collection');
    } catch(err) {
        console.error(`Cannot find the path`);
    }
    
    const exist = machineCollection.find(q => q.macA === data.macA);

    if (!exist) {
        const newMachine = new Machine(data);
        await db.push('/machine-collection', [newMachine], false);
        return 'added';
    }

    return 'found';
}

module.exports = socketMain;
