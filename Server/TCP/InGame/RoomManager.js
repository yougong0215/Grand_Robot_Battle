const Gacha = require("../Gacha/main.js");
const ItemUtil = require("../lib/itemUtils.js");

const Rooms = {};
const JoinPlayers = {};

class Room {
    roomID = undefined;
    players = {}; // 방안에 플레이어들
    // control = null; // 현재 누가 공격중인지
    ready = false; // 모든 플레이어가 준비됨? (게임 시작됨?)

    constructor(id) {
        this.roomID = id;
    }

    Join = function(playerID) {
        // 플레이어 저장
        this.players[playerID] = {
            health: 100,
            attack: 0,
            shield: 0,
            speed: 0,
            parts: {}
        }
        JoinPlayers[playerID] = this;
    }

    Destroy = function() {
        Object.keys(this.players).forEach(id => delete JoinPlayers[id]);
        this.players = {};
        this.ready = false;
        delete Rooms[this.roomID];
        console.log(`[RoomManager](${this.roomID}) 방 삭제됨`);
    }
    
    PlayerLeft = function(id, name) {
        if (this.ready) { // 이미 게임이 시작된 경우
            console.log(`[RoomManager](${this.roomID}) ${name}(${id})님이 탈주하였습니다.`);
        } else { // 아직 게임이 시작되지 않은 경우
            console.log(`[RoomManager](${this.roomID}) ${name}(${id})님이 나가셨습니다.`);
        }

        Object.keys(this.players).forEach(nplayerID => {
            if (nplayerID === id) return;

            const player = UserList[nplayerID];
            player.socket.send("ingame.destory", name);
        });
        this.Destroy(); // 방폭
    }

    // 승리 보상
    Reward = function(id) {
        const rewards = [];
        for (let i = 0; i < 5; i++) {
            const itemID = Gacha.RandomItemCode();
            ItemUtil.Give(id, itemID+"_puzzel", 1);
            rewards.push(itemID);
        }
        UserList[id]?.socket?.send("ingame.reward", rewards);
    }
}

exports.RoomClass = Room;
// Room 스크립트 (확장)
require("./RoomWait.js");
require("./GameStart.js");
require("./SkillSelect.js");

// 랜덤 숫자
function getRandom(min, max) {
    return Math.floor((Math.random() * (max - min + 1)) + min);
}

function RandomRoomID() {
    const randomID = getRandom(10000,99999);
    if (Rooms[randomID] !== undefined) {
        return RandomRoomID();
    }

    return randomID;
}

exports.Create = function() {
    const RoomID = RandomRoomID();
    const room = Rooms[RoomID] = new Room(RoomID);

    return room;
}

exports.isPlayerRoom = function(playerID) {
    return JoinPlayers[playerID] !== undefined;
}

exports.getRoomToPlayer = function(playerID) {
    return JoinPlayers[playerID];
}