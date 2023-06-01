using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using LitJson;

public class HTTP_manager : MonoBehaviour
{
    [Header("서버주소 설정")]
    [SerializeField, Tooltip("서버 아이피")] string Endpoint = "127.0.0.1";
    [SerializeField, Tooltip("서버 포트")] int Port = 3001;

    static HTTP_manager instance;
    private void Awake() {
        if (instance == null) instance = this;
        else Debug.LogError("[domiHTTP Manager] HTTP 매니저는 1개만 사용해야 합니다.");
    }

    // 신기한 도미방법??? 일요일이 소중하기 때문에 HTTP_manager.instance.RequestPOST 보다 HTTP_manager.RequestPOST 로 하는게 좋음
    public static void RequestPOST(string path, object data, UnityAction<int, JsonData> callback) => instance._RequestPOST(path, data, callback);
    void _RequestPOST(string path, object data, UnityAction<int, JsonData> callback) => StartCoroutine(StartPOST(path, data, callback));

    public static void RequestGET(string uri, UnityAction<int, JsonData> callback) => instance._RequestGET(uri, callback);
    void _RequestGET(string uri, UnityAction<int, JsonData> callback) => StartCoroutine(StartGET(uri, callback));

    IEnumerator StartPOST(string path, object data, UnityAction<int, JsonData> callback) {
        // 데이터
        var FormData = new Dictionary<string, string>();
        FormData["domi"] = JsonUtility.ToJson(data);

        using (var request = UnityWebRequest.Post($"http://{Endpoint}:{Port}/{path}", FormData)) {
            yield return request.SendWebRequest(); // 기달..

            // JSON 이 안풀리면 null로 보내는 방식
            JsonData json_decode = null;
            try {
                json_decode = JsonMapper.ToObject(request.downloadHandler.text);
            } catch {};

            callback.Invoke((int)request.responseCode, json_decode);
        }
    }

    // url은 전체 경로고 uri 는 주소 뒤메 있는거지롱
    IEnumerator StartGET(string uri, UnityAction<int, JsonData> callback) {
        using (var request = UnityWebRequest.Get($"http://{Endpoint}:{Port}/{uri}")) {
            yield return request.SendWebRequest(); // 기달..

            // JSON 이 안풀리면 null로 보내는 방식
            JsonData json_decode = null;
            try {
                json_decode = JsonMapper.ToObject(request.downloadHandler.text);
            } catch {};

            callback.Invoke((int)request.responseCode, json_decode);
        }
    }
}
