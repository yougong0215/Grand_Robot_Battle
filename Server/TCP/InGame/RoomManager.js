const Rooms = {};
const JoinPlayers = {};

class Room {
    players = []; // 방안에 플레이어들
    control = 0; // 현재 누가 공격중인지
    ready = false; // 모든 플레이어가 준비됨? (게임 시작됨?)

    Join = function(playerID) {
        this.players.push(playerID);
        JoinPlayers[playerID] = this;
    }
    GameStart = function() {
        this.ready = true;
        console.log("[RoomManager] 게임 시작!");
    }
}

exports.RoomClass = Room;
// Room 스크립트 (확장)
require("./RoomWait.js");

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
    const room = Rooms[RoomID] = new Room();

    return room;
}

exports.isPlayerRoom = function(playerID) {
    return JoinPlayers[playerID] !== undefined;
}

exports.getRoomToPlayer = function(playerID) {
    return JoinPlayers[playerID];
}