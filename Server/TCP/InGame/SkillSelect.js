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
RoomManager.RoomClass.prototype.SkillChoiceFinish = function() {
    if (this.SelectSkills === undefined) return;

    for (const [_, part] of Object.entries(this.SelectSkills))
        if (part === false) return;

    const SelectSkill = this.SelectSkills;
    delete this.SelectSkills;

    console.log("[RoomManager]("+ this.roomID +") 모든 플레이어가 스킬 선택을 완료했습니다.");
    console.log(SelectSkill);
}

TriggerEvent["ingame.selectSkill"] = function(id, part) {
    const room = RoomManager.getRoomToPlayer(id);
    console.log(room.SelectSkills, room.SelectSkills[id], Number(part));
    if (room === undefined || room.SelectSkills === undefined || room.SelectSkills[id] !== false || Number(part) > 4) return;

    console.log(id, part);

    room.SelectSkills[id] = Number(part);
    room.SkillChoiceFinish();
}