TriggerEvent["story.getClearNum"] = function(id) {
    const player = UserList[id];
    if (player === undefined) return;

    player.socket.send("story.resultClearNum", player.clearStory);
}

TriggerEvent["story.clear"] = function(id, story) {
    const player = UserList[id];
    if (player === undefined) return;
    
    if (story <= player.clearStory) {
        return
    }

    player.clearStory += 1;
}