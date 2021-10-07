const net = require('net');
var randomWords = require('random-words');
const option=
    {
        host:'127.0.0.1',
        port: 3000
    }

const client = function(socket) {
    console.log('A new connection has been established.');
    socket.write('The connection is established. ');

    socket.on('data', function(data) {
        console.log(`Data received from client: ${data.toString()}`);
        socket.write(randomWords({ exactly: parseInt(data), join: ' ' }))
    });

    socket.on('end', function() {
        console.log('Closing connection with the client');
    });

    socket.on('error', function(err) {
        console.log(`Error: ${err}`);
    });
};

const server = new net.Server();
server.on('connection', client);
server.listen(option, function() {
    console.log(`Server listening for connection requests on socket localhost:`);
});
