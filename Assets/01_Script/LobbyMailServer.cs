using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class LobbyMailServer : MonoBehaviour
{
    struct MailPreview {
        public int id;
        public string title;
        public string sender;
        public int time;
    }

    private void Awake() {
        NetworkCore.EventListener["mail.resultMails"] = ResultMails;
        NetworkCore.EventListener["mail.resultContent"] = ResultContent;
    }
    private void OnDestroy() {
        NetworkCore.EventListener.Remove("mail.resultMails");
        NetworkCore.EventListener.Remove("mail.resultContent");
    }

    private void Start() {
        LoadMail(0);
        ShowContentMail(0);
    }

    public void LoadMail(int page) {
        NetworkCore.Send("mail.requestMails", page);
    }

    public void ShowContentMail(int id) {
        NetworkCore.Send("mail.requestContent", id);
    }

    void ResultMails(JsonData data) {
        var mails = JsonMapper.ToObject<MailPreview[]>(data.ToJson());
        foreach (MailPreview item in mails)
        {
            print("------- 메일 ---------");
            print(item.id);
            print(item.title);
            print(item.sender);
            print(item.time);
        }
    }
    
    void ResultContent(JsonData data) {
        if (data == null) {
            // 파일 내용 없음
            return;
        }

        // 파일 내용 있음 ㅁㄴㅇㄹ
    }
}
