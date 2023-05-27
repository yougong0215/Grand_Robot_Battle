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

    void RequestPOST(string path, object data, UnityAction<int, JsonData> callback) => StartCoroutine(StartPOST(path, data, callback));

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

    class dodmidomdidksd {
        public string ID;
        public string password;

        public dodmidomdidksd(string _id, string _password) {
            ID = _id;
            password = _password;
        }
    }
    private void Start() {
        print("Test HTTP");
        RequestPOST("login", new dodmidomdidksd("domi", "asdasd"), (int statusCode, JsonData data) => {
            print(statusCode);
            print(data["why"]);
        });
    }
}
