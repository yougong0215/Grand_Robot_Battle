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
    }
    private void OnDestroy() {
        NetworkCore.EventListener.Remove("mail.resultMails");
    }

    private void Start() {
        LoadMail(0);
    }

    public void LoadMail(int page) {
        NetworkCore.Send("mail.requestMails", page);
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
}
