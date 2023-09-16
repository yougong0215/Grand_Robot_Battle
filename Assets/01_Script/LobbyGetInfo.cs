using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using LitJson;
using UnityEngine.Networking;

public class LobbyPlayerInfoPacket {
    public string ID;
    public string Name;
    public string Prefix;
    public string AvatarURL;
    public int Coin;
    public int Crystal;
    public int ADtime;
}

public class LobbyGetInfo : MonoBehaviour
{
    static Dictionary<string, Texture2D> cache_Profile = new();
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
        Document.rootVisualElement.Q<Label>("Name").text = PlayerInfo.Name /*+ " - " + PlayerInfo.ID*/;
        Document.rootVisualElement.Q<Label>("style").text = PlayerInfo.Prefix; // element ID 가 #Style인건 수정해야될거같다ㅏㅏ

        // 돈 부분
        Document.rootVisualElement.Q("GoldBar").Q<Label>("Gemtxt").text = PlayerInfo.Coin.ToString();
        Document.rootVisualElement.Q("GemBar").Q<Label>("Gemtxt").text = PlayerInfo.Crystal.ToString();



        if (PlayerInfo.AvatarURL != null)
            StartCoroutine(GetProfileImage(PlayerInfo.AvatarURL));

        if (PlayerInfo.ADtime > 0) {
            StartCoroutine("WaitAdBtn", PlayerInfo.ADtime);
        }
    }

    IEnumerator GetProfileImage(string url) {
        VisualElement profile =  Document.rootVisualElement.Q("Profile").Q("ProfileImg");
        if (cache_Profile.TryGetValue(url, out var img)) {
            profile.style.backgroundImage = new StyleBackground(img);
        } else {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                profile.style.backgroundImage = new StyleBackground(((DownloadHandlerTexture)www.downloadHandler).texture);
                cache_Profile[url] = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
        }
    }

    IEnumerator WaitAdBtn(int time) {
        var _ADbtn = Document.rootVisualElement.Q<Button>("ADbtn");
        _ADbtn.style.unityBackgroundImageTintColor = new Color(0.4f, 0.4f, 0.4f);

        while (-- time > 0) {
            yield return new WaitForSecondsRealtime(1);
        }
        
        _ADbtn.style.unityBackgroundImageTintColor = new Color(1,1,1);
    }
}
