const net = require("net");

global.TriggerEvent = {};
require("./lib/UserManager.js");

const server = net.createServer(require("./connection.js"));
server.listen(Config.TCPport, () => console.log("[TCP] 서버 출항 준비 완료! Port: "+Config.TCPport));