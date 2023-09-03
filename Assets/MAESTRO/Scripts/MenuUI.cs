using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using LitJson;

struct MailPreview {
    public int id;
    public string user;
    public string title;
    public string content;
    public string sender;
    public int time;
}

public class MenuUI : MonoBehaviour
{
    private UIDocument _doc;
    //public VisualTreeAsset _storyView;
    public VisualTreeAsset _mailView;
    public VisualTreeAsset _mailDocument;
    private VisualElement _root;
    private Button _battleBtn;
    private Button _gongBtn;
    private Button _makeBtn;
    private Button _storeBtn;
    private Button _garageBtn;

    private VisualElement _charImg;

    

    VisualElement _storyElem;
    private Button _storyBtn;
    //Button _storyExitBtn;
    //private StorySelectUI _stdUI;

    VisualElement _mailElem;
    Button _mailExitBtn;
    Button _postBtn;


    bool isStroyPanelON = false;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        NetworkCore.EventListener["mail.resultMails"] = ResultMails;
    }

    void OnDestroy() {
        NetworkCore.EventListener.Remove("mail.resultMails");
    }

    private void Start()
    {
        // TEST CODE
        // SceneLoad("MetalBattleGround");
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _battleBtn = _root.Q<Button>("BattleBtn");
        _gongBtn = _root.Q<Button>("GongBtn");
        //_makeBtn = _root.Q<Button>("MakeBtn");
        _storeBtn = _root.Q<Button>("StoreBtn");
        //_garageBtn = _root.Q<Button>("GarageBtn");
        _storyBtn = _root.Q<Button>("StoryBtn");
        _postBtn = _root.Q<Button>("Postbtn");

        _charImg = _root.Q<VisualElement>("CharImg");
        //_makeBtn.clicked += () => SceneLoad("Gacha");
        // _battleBtn.clicked += () => SceneLoad("PVP");
        _battleBtn.clicked += () => LoadManager.LoadScene(SceneEnum.GameMatching);
        _storyBtn.clicked += () => LoadManager.LoadScene(SceneEnum.Story);
        _gongBtn.clicked += () => LoadManager.LoadScene(SceneEnum.MakeRobot);// SceneLoad("MakeRobot");
        _storeBtn.clicked += () => LoadManager.LoadScene(SceneEnum.SelectStoreScene);//SceneLoad("SelectStoreScene");
        //_garageBtn.clicked += () => SceneLoad("Garage");

        //_storyView.CloneTree(_root);
        //_storyElem = _root.Q<VisualElement>("StoryBoard");
        //_storyExitBtn = _storyElem.Q<Button>("ExitBtn");

        //_storyExitBtn.clicked += () => LoadStroyView();
        //_storyElem.Blur();
        _storyElem.AddToClassList("off");

        
        _mailView.CloneTree(_root);
        _mailElem = _root.Q<VisualElement>("MailView");
        _mailExitBtn = _mailElem.Q<Button>("ExitBtn");

         _mailElem.AddToClassList("off");
        _mailExitBtn.clicked += () => _mailElem.AddToClassList("off");
        _postBtn.clicked += () => {
            NetworkCore.Send("mail.requestMails", 0);
            _mailElem.RemoveFromClassList("off");
        };
    }




    void LoadStroyView()
    {

        if (_storyElem.ClassListContains("off") == false)
        {
            _storyElem.AddToClassList("off");
            Debug.Log("1");
        }
        else
        {
            _storyElem.RemoveFromClassList("off");
            Debug.Log("1");
        }

    }

    public void AddQuest()
    {
        // 퀘스트 보상 등등 추가 해줘야됨 ( UIDocument 자체는 있음 )
    }

    public void AddMail()
    {
        // 이게 메일(post)용임 내용은 추후에 예기 해야댬
        // 멇핣걻없낣 싦싦핢닯 놂곫싮닮 앎앍앖앇앎
        //_mailDocument.CloneTree(_mailElem);
        VisualElement rooted = _mailDocument.Instantiate();
        //Button GetResult = rooted.Q<Button>("")


        //Label name = _mailElem;
    }

    void ResultMails(JsonData data) {
        var mails = JsonMapper.ToObject<MailPreview[]>(data.ToJson());
        foreach (MailPreview item in mails)
        {
            print("------- 메일 ---------");
            print(item.id);
            print(item.user);
            print(item.title);
            print(item.content);
            print(item.sender);
            print(item.time);
        }
    }
}
