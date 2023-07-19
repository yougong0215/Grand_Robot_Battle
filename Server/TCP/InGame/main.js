const RoomManager = require("./RoomManager.js");

const MatchPlayers = exports.MatchPlayers = new Set();

// 랜덤으로 한명 갖고옴
function getRandomValueFromSet(set, ignore) {
    if (!(set instanceof Set) || set.size === 0) {
      return undefined;
    }
  
    // Set을 배열로 변환하여 랜덤 인덱스를 생성합니다.
    const valuesArray = Array.from(set);
    const randomIndex = Math.floor(Math.random() * valuesArray.length);

    if (valuesArray[randomIndex] === ignore) { // 결과값이 제외하는 값이면
        return getRandomValueFromSet(set, ignore);
    }
  
    // 랜덤 인덱스를 사용하여 값을 반환합니다.
    return valuesArray[randomIndex];
}

// 매칭 등록
TriggerEvent["Match.Add"] = function(id) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready || MatchPlayers.has(id) || RoomManager.isPlayerRoom(id)) return;

    // 매치 시작하면 일단 대기열에 넣음
    MatchPlayers.add(id);


    // 매치 플레이어가 없거나 나밖에 없으면 아래 코드는 실행 안함
    console.log(`[InGame] ${player.name}님이 매칭을 시작함.`);
    if (MatchPlayers.size <= 1) return;
    
    console.log(`[InGame] ${player.name}님이랑 같이할 사람 찾는중...`);
    
    const other_playerID = getRandomValueFromSet(MatchPlayers, id /* 내 아이디는 제외 */);
    const other_player = UserList[other_playerID];
    if (other_player === undefined) return; // 잉 왜없지

    // 매치 성공
    console.log(`[InGame] Match - ${Player.name} <-> ${other_player.name} 매칭되었음`);

    MatchPlayers.delete(id);
    MatchPlayers.delete(other_playerID);

    // 방 만듬
    const room = RoomManager.Create();
    room.Join(id);
    room.Join(other_playerID);

    room.WaitForPlayer(); // 접속 대기
}