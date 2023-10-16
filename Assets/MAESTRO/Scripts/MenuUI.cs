using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using LitJson;
using System.Linq;

struct MailPreview {
    public int id;
    public string user;
    public string title;
    public string content;
    public string sender;
    public string items;
    public int time;
}

public class MenuUI : MonoBehaviour
{
    [SerializeField] FriendUI _friendui;
    PurchaseUI purchaseUI;
    AdSystem _AdSys;
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
    private Setting _setting;
    private Button _gemplusbtn;
    private Button _goldplusbtn;
    private Button _settingbtn;
    private VisualElement _charImg;
    private Button _ADbtn;
    private Button _adAcceptBtn;
    private Button _adCancleBtn;
    private VisualElement _adPanel;
    private Button _friendBtn;

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
        NetworkCore.EventListener["mail.successGiveItem"] = successGiveItem;
        NetworkCore.EventListener["ad.ResultTryShow"] = ADshow;
        purchaseUI = GameObject.Find("PURCHASE").GetComponent<PurchaseUI>();
        _setting = GameObject.Find("SETTING").GetComponent<Setting>();
        _AdSys = GetComponent<AdSystem>();
    }

    void OnDestroy() {
        NetworkCore.EventListener.Remove("mail.resultMails");
        NetworkCore.EventListener.Remove("mail.successGiveItem");
        NetworkCore.EventListener.Remove("ad.ResultTryShow");
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
        // _storyElem.AddToClassList("off");
        _settingbtn = _root.Q<Button>("Setting");
        _settingbtn.clicked += () => _setting.ActivePanel(true);
        
        _mailView.CloneTree(_root);
        _mailElem = _root.Q<VisualElement>("MailView");
        _mailExitBtn = _mailElem.Q<Button>("ExitBtn");

         _mailElem.AddToClassList("off");
        _mailExitBtn.clicked += () => _mailElem.AddToClassList("off");
        _postBtn.clicked += () => {
            NetworkCore.Send("mail.requestMails", 0);
            _mailElem.RemoveFromClassList("off");
        };

        _gemplusbtn = _root.Q<Button>("Gemplus");
        _gemplusbtn.clicked += () => purchaseUI.ActivePanel(true);
        _goldplusbtn = _root.Q<Button>("Goldplus");
        //골드는 어카냐
        _ADbtn = _root.Q<Button>("ADbtn");
        _ADbtn.clicked += () => LookADPanel(true);
        _adAcceptBtn = _root.Q<Button>("ad-accept-btn");
        _adAcceptBtn.clicked += LookAD;
        _adCancleBtn = _root.Q<Button>("ad-cancle-btn");
        _adCancleBtn.clicked += () => LookADPanel(false);
        _adPanel = _root.Q("ad-panel");
        _friendBtn = _root.Q<Button>("Friendbtn");

        _friendBtn.clicked += () => _friendui.OpenFriendList(true);
    }

    private void LookADPanel(bool isOk)
    {
        if (isOk)
            _adPanel.AddToClassList("on");
        else
            _adPanel.RemoveFromClassList("on");
    }

    private void LookAD()
    {
        NetworkCore.Send("ad.TryShow", null);
        LookADPanel(false);
    }

    void ADshow(JsonData data) {
        if ((bool)data == false) return;
        // 광고 연결
        _AdSys.ShowRewardedAd((GoogleMobileAds.Api.Reward reward) => {
            // _ADbtn.style.unityBackgroundImageTintColor = new Color(0.4f, 0.4f, 0.4f);
            NetworkCore.Send("ad.give", null);
            GetComponent<LobbyGetInfo>().ResetWaitAD();
        });
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
        mails = mails.OrderBy(value => value.time).Reverse().ToArray(); // 정렬
        
        var scrollView = _mailElem.Q<VisualElement>("MailArea");
        var container = scrollView.Q<VisualElement>("unity-content-container");
        container.Clear(); // 초깅화

        int i = 0;
        foreach (MailPreview item in mails)
        {
            _mailDocument.CloneTree(container);
            var element = container.ElementAt(i);
            var label = element.Q<VisualElement>("Label");
            var button = element.Q<Button>("GetBtn");

            label.Q<Label>("MailName").text = item.title;
            label.Q<Label>("MailResult").text = item.sender;
            label.Q<Label>("MailExplain").text = item.content;
            if (item.items == "[]") // 아무것도 없는거임
                button.SetEnabled(false);
            else
                button.clicked += () => {
                    button.SetEnabled(false);
                    NetworkCore.Send("mail.openItem", item.id);
                };

            print("------- 메일 ---------");
            print(item.id);
            print(item.user);
            print(item.title);
            print(item.content);
            print(item.sender);
            print(item.time);
            i ++;

            
        }
    }
    
    struct MailItem
    {
        public string code;
        public int amount;
    }
    void successGiveItem(JsonData data) {
        var items = JsonMapper.ToObject<MailItem[]>(data.ToJson());
        
        print("보상을 성공적으로 받았습니다.");
        foreach (var item in items)
        {
            print("- " + item.code + " x"+item.amount);
        }
    }
}
