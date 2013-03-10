var conn = [];
var WebSocketServer = require('websocket').server;
var http = require('http');
var originIsAllowed = function(){return true;}
var server = http.createServer(function(request, response) {
    console.log((new Date()) + " Received request for " + request.url);
    response.writeHead(404);
    response.end();
});
var redis = require("redis"),
  rc = redis.createClient();

var msgs = [];

var reloadVideos = function(socket) {
  rc.zcard('vine:link:realtime', function(err, count) {
    rc.zrange('vine:link:realtime', count - 20, count - 1, function(err, replies) {
      for (var i = 0; i < replies.length - 1; i++) {

        rc.hvals(replies[i], function(err, replies2) {
          var msg = [];
          for (var j = 0; j < replies2.length; j++) {
            msg.push(replies2[j]);
          }
          msgs.push(msg);
          console.log(msgs.length);

        });

      }

      rc.hvals(replies[replies.length - 1], function(err, replies2) {
        var msg = [];
        for (var j = 0; j < replies2.length; j++) {
          msg.push(replies2[j]);
        }

        for(var c = 0; c < conn.length; c++){
            if(!conn[c].closed){
                conn[c].sendUTF(msg[0] + ':' + msg[1] + ':' + msg[2]);
            }
        }
      });
    });
  });
}

server.listen(10888, function() {
    console.log((new Date()) + "Server is listening on port 10888");
});

wsServer = new WebSocketServer({
    httpServer: server,
    autoAcceptConnections: false
});

wsServer.on('close', function(request) {
    console.log("Closed");
});

wsServer.on('request', function(request) {
    if (!originIsAllowed(request.origin)) {
        request.reject();
        console.log((new Date()) + " Connection from origin " + request.origin + " rejected.");
        return;
    }
    console.log("Connected");
    var con = request.accept(null, request.origin)
    conn.push(con);
    con.on('message', function(mg) {
        console.log(mg.utf8Data);
        if (msgs.length != 0) {
            msg = msgs.pop()
            for(var c = 0;c < conn.length;c++){
                if(!conn[c].closed){
                    conn[c].sendUTF(msg[0] + ':' + msg[1] + ':' + msg[2]);
                }
            }

        } else {
            reloadVideos(conn);
        }
    });
});