using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using LitJson;

public class LobbyPlayerInfoPacket {
    public string ID;
    public string Name;
    public int Coin;
    public int Crystal;
}

public class LobbyGetInfo : MonoBehaviour
{
    [SerializeField] UIDocument Document;

    private void Awake() {
        NetworkCore.EventListener["Lobby.ResultInfo"] = SetInfo;
    }
    private void OnDestroy() {
        NetworkCore.EventListener.Remove("Lobby.ResultInfo");
    }

    private void Start() {
        NetworkCore.Send("Lobby.RequestInfo", null);
    }

    void SetInfo(JsonData data) {
        var PlayerInfo = JsonMapper.ToObject<LobbyPlayerInfoPacket>(data.ToJson());
        Document.rootVisualElement.Q<Label>("Name").text = PlayerInfo.Name + " - " + PlayerInfo.ID;

        Document.rootVisualElement.Q("GoldBar").Q<Label>("Gemtxt").text = PlayerInfo.Coin.ToString();
        Document.rootVisualElement.Q("GemBar").Q<Label>("Gemtxt").text = PlayerInfo.Crystal.ToString();
    }
}
