const RoomManager = require("./RoomManager.js");

function getRandomNumber(min, max) {
    min = Math.ceil(min); // 최소값 올림
    max = Math.floor(max); // 최대값 내림
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

TriggerEvent["ingame.selectSkill"] = function(id, part) {
    const room = RoomManager.getRoomToPlayer(id);
    if (room === undefined || room.SelectSkills === undefined || room.SelectSkills[id] !== false || Number(part) > 4) return;

    room.SelectSkills[id] = Number(part);
    room.SkillChoiceFinish();
}

RoomManager.RoomClass.prototype.SkillChoice = function(disableControl) {
    this.SelectSkills = {};
    Object.keys(this.players).forEach(id => {
        const player = UserList[id];
        
        this.SelectSkills[id] = false;
        if (!disableControl)
            player.socket.send("ingame.AttackControl", null);
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
    
    let attackid = Object.keys(this.players)[0];
    let hitid = Object.keys(this.players)[1];
    const swapID = function() {
        let cache_attack = attackid;
        attackid = hitid;
        hitid = cache_attack;
    }
    
    if (this.players[attackid].speed === this.players[hitid].speed) {
        if (getRandomNumber(1, 2) === getRandomNumber(1,2)) swapID();
    } else if (this.players[attackid].speed < this.players[hitid].speed) swapID();

    const attacker_name = UserList[attackid].name;
    const hitter_name = UserList[hitid].name;

    const result_1st = AttackPlayer(this, attackid, hitid, SelectSkill[attackid]);
    console.log(`[RoomManager](${this.roomID}) ${attacker_name} ⚔️ --->  ${hitter_name} ${(result_1st.answer ? `(남은 HP: ${result_1st.health})` : `공격불가: ${result_1st.why}`)}`);
    const result_2st = AttackPlayer(this, hitid, attackid, SelectSkill[hitid]);
    console.log(`[RoomManager](${this.roomID}) ${hitter_name} ⚔️ --->  ${attacker_name} ${(result_2st.answer ? `(남은 HP: ${result_2st.health})` : `공격불가: ${result_2st.why}`)}`);

    Object.keys(this.players).forEach(id => {
        const player = UserList[id];

        player.socket.send("ingame.gameresult", [
            {
                my: attackid === id,
                attacker: attacker_name,
                hitter: hitter_name,
                ...result_1st
            },
            {
                my: hitid === id,
                attacker: hitter_name,
                hitter: attacker_name,
                ...result_2st
            }
        ]);
    });

    if (result_2st.why === "domiNotHealthEvent") {
        const winName = result_2st.answer ? hitter_name : attacker_name;
        console.log(`[RoomManager](${this.roomID}) 게임종료!! / ${winName} 이김!`);
        this.Reward(result_2st.answer ? hitid : attackid);
        this.Destroy(); // 방폭
        return;
    }

    this.SkillChoice(true);
}

function AttackPlayer(room, attackid, hitid, partid) {
    const attack_part = GetPartName(partid);
    const attacker = room.players[attackid];
    const hitter = room.players[hitid];

    if (attacker.health <= 0)
        return {
            answer: false,
            power: 0,
            health: 0,
            soid: null,
            why: "domiNotHealthEvent"  
        }

    // 스킵
    if (partid === -1)
        return {
            answer: false,
            power: 0,
            health: 0,
            soid: null,
            why: "domiSkipEvent"
        }

    const attackerParts = attacker.parts[attack_part];
    if (attackerParts === undefined) {
        return {
            answer: false,
            power: 0,
            health: 0,
            soid: null,
            why: "파츠가 없어 공격할 수 없습니다."  
        }
    }
    // 쿨타임
    if (attackerParts.activeTime !== undefined && (attackerParts.cooltime * 1000) > (new Date() - attackerParts.activeTime)) {
        return {
            answer: false,
            power: 0,
            health: 0,
            soid: null,
            why: "쿨타임이 지나지 않아 공격할 수 없습니다."  
        }
    }
    
    attackerParts.activeTime = new Date(); // 사용시간 등록
    hitter.health -= attackerParts.attack;
    if (hitter.health < 0) hitter.health = 0;

    return {
        answer: true,
        power: attackerParts.attack,
        health: hitter.health,
        soid: attackerParts.id,
        why: hitter.health === 0 ? "domiNotHealthEvent" : null
    }
}

function GetPartName(id) {
    switch (id) {
        case 0:
            return "left";
        case 1:
            return "right";
        case 2:
            return "head";
        case 3:
            return "body";
        case 4:
            return "leg";
        default:
            return "unknown";
    }
}