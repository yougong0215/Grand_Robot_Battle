using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ServerSOPacket {
    public string code;
    public int level;
}

public class GetServerToSO : MonoBehaviour
{
    private void Awake() {
        NetworkCore.EventListener["MakeRobot.ResultSO"] = ResultSO;
    }

    private void Start() {
        NetworkCore.Send("MakeRobot.GetSO", null);
    }
    private void OnDestroy() {
        NetworkCore.EventListener.Remove("MakeRobot.ResultSO");
    }

    void ResultSO(JsonData data) {
        var SO_List = JsonMapper.ToObject<Dictionary<string, ServerSOPacket>>(data.ToJson());
        print(SO_List.Count);

        foreach (var SOOOO in SO_List) {
            print("-------- "+SOOOO.Key+" ---------");
            print(SOOOO.Value.code);
            print(SOOOO.Value.level);
        }
    }
}
