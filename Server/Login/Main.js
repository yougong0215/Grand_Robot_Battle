// const express = require
const express = require("express");
const app = express();

app.use(express.urlencoded({extended: true}));
app.use(express.json());

// 로그인 로직
app.post("/login", require("./Login.js"));
app.post("/logout", require("./Logout.js"));

// 이름 중복확인
app.get("/nameCompare/:name", function(req, res) {
    const name = req.params.name;
    console.log(name);
});

app.listen(Config.LoginPort, () => console.info("[HTTP] 로그인 시스템이 준비되었습니다. Port: "+ Config.LoginPort));