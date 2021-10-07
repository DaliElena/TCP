const net = require('net');
const option =
    {
        host: '127.0.0.1',
        port: 3000
    }
const client = net.createConnection(option, () => {
    let numberWorld = Math.floor(Math.random() * 16) + 5;
    client.write(numberWorld.toString());
})

client.on('data', function (data) {
    console.log('Server return data : ' + data);

});

client.on('end', function () {
    console.log('Client disconnect.');
});

client.on('error', function (err) {
    console.error(JSON.stringify(err));
});
