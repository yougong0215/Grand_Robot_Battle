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