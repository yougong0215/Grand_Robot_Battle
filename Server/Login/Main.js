// const express = require
const express = require("express");
const app = express();

app.use(express.urlencoded({extended: true}));
app.use(express.json());

// 로그인 로직
app.post("/login", require("./Login.js"));

app.listen(Config.LoginPort, () => console.info("로그인 시스템이 준비되었습니다. Port: "+ Config.LoginPort));