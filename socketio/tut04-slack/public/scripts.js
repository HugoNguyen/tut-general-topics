const username = prompt("What is your username?");
// const socket = io('/'); // => /namespace endpoint

const socket = io('/', {
    query: { username }
});

let nsSocket = '';
console.log(socket.io);

// listen for nsList, which is a list of all ns
socket.on('nsList', nsData => {
    console.log('The list of ns has arrived');
    const namespacesDev = document.querySelector('.namespaces');
    namespacesDev.innerHTML = "";
    nsData.forEach(ns => {
        namespacesDev.innerHTML += `<div class="namespace" ns=${ns.endpoint}><img src="${ns.img}" /></div>`;
    });

    // Add a click listener for each ns
    Array.from(document.getElementsByClassName('namespace')).forEach(el => {
        el.addEventListener('click', e => {
            const nsEndpoint = el.getAttribute('ns');
            // console.log(`${nsEndpoint} I should go to now`);
            joinNs(nsEndpoint);
        });
    });

    joinNs('/wiki');
});
