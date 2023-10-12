const net = require("net");

global.TriggerEvent = {};
require("./lib/UserManager.js");
require("./ItemStat");
require("./Lobby/main.js");
require("./MakeRobot/main.js");
require("./Gacha/main.js");
require("./InGame/main.js");
require("./Garage/main.js");
require("./Mail/triggers.js");
require("./Story/trigger.js");
require("./Puzzel/main.js");
require("./AD/main.js");
require("./Store/main.js");
require("./Friend/main.js");
require("./TestCodes.js");

const server = net.createServer(require("./connection.js"));
server.listen(Config.TCPport, () => console.log("[TCP] 서버 출항 준비 완료! Port: "+Config.TCPport));