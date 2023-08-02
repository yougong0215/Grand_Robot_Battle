global.Config = require("./config.json"); // 전역 변수 선언
global.__rootdir = process.cwd();

require("./Login/main.js");
require("./TCP/main.js");