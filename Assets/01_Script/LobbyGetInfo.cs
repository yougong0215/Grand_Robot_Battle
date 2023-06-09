using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using LitJson;

public class LobbyPlayerInfoPacket {
    public string ID;
    public string Name;
    public string Prefix;
    public int Coin;
    public int Crystal;
}

public class LobbyGetInfo : MonoBehaviour
{
    [SerializeField] UIDocument Document;

    // 이벤트 리스너 등록
    private void Awake() {
        NetworkCore.EventListener["Lobby.ResultInfo"] = SetInfo;
    }
    // 이벤트 리스너 해제
    private void OnDestroy() {
        NetworkCore.EventListener.Remove("Lobby.ResultInfo");
    }

    private void Start() {
        NetworkCore.Send("Lobby.RequestInfo", null); // 서버에게 정보 달라고 요청함
    }

    // 서버한테 정보 받음
    void SetInfo(JsonData data) {
        var PlayerInfo = JsonMapper.ToObject<LobbyPlayerInfoPacket>(data.ToJson());
        // 프로필 부분
        Document.rootVisualElement.Q<Label>("Name").text = PlayerInfo.Name + " - " + PlayerInfo.ID;
        Document.rootVisualElement.Q<Label>("style").text = PlayerInfo.Prefix; // element ID 가 #Style인건 수정해야될거같다ㅏㅏ

        // 돈 부분
        Document.rootVisualElement.Q("GoldBar").Q<Label>("Gemtxt").text = PlayerInfo.Coin.ToString();
        Document.rootVisualElement.Q("GemBar").Q<Label>("Gemtxt").text = PlayerInfo.Crystal.ToString();
    }
}
