const RoomManager = require("./RoomManager.js");

RoomManager.RoomClass.prototype.SkillChoice = function() {
    this.SelectSkills = {};
    Object.keys(this.players).forEach(id => {
        const player = UserList[id];
        
        this.SelectSkills[id] = false;
        player.socket.send("ingame.AttackControl", null);
        console.log(this.players[id].parts);
    });


    console.log("[RoomManager]("+ this.roomID +") 스킬 선택을 기다리고 있습니다.");
}