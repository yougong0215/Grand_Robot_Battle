using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StoryUI : MonoBehaviour
{
    public StoryScriptSO Init;
    public StoryScriptSO Out;

    public List<StoryUISO> _storySOList = new List<StoryUISO>();
    private UIDocument _doc;
    private VisualElement _root;

    private Button _rightbtn;
    private Button _leftBtn;

    private Label _roundTxt;
    private Label _titleTxt;
    private VisualElement _imagePanel;
    private Label _expTxt;
    private VisualElement _ingPanel;
    private Label _ingTxt;

    private VisualElement _enemyImage;
    private Label _enemyNameTxt;
    private Label _enemyInfoTxt;
    private Button _gameEnter;
    [SerializeField] int currentRound = 0;
    int _maxStage;


    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        NetworkCore.EventListener["story.resultClearNum"] = ResultClearNum;
    }

    private void OnDestroy() {
        NetworkCore.EventListener.Remove("story.resultClearNum");
    }

    private void Start() {
        // NetworkCore.Send("story.clear", 1 /* 스토리 ID (1부터) */);
        // NetworkCore.Send("story.clear", 2);
        // NetworkCore.Send("story.clear", 3);
        NetworkCore.Send("story.getClearNum", null); // 불러오기 요청
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _leftBtn = _root.Q<Button>("LeftArrow");
        _rightbtn = _root.Q<Button>("RightArrow");
        _roundTxt = _root.Q<Label>("RoundText");
        _titleTxt = _root.Q<Label>("TitleTxt");
        _imagePanel = _root.Q<VisualElement>("ImagePanel");
        _expTxt = _root.Q<Label>("ExpText");
        _ingPanel = _root.Q<Label>("ingPanel");
        _ingTxt = _root.Q<Label>("ingTxt");
        _enemyImage = _root.Q<VisualElement>("EnemyImage");
        _enemyNameTxt = _root.Q<Label>("EnemyName");
        _enemyInfoTxt = _root.Q<Label>("infoTxt");
        
        

        _maxStage = _storySOList.Count;
        _gameEnter = _root.Q<Button>("EnterBtn");


        _gameEnter.clicked += () => SceneManager.LoadScene((int)StoryLoadResource.Instance.NextScene());
        _gameEnter.clicked += () => StoryLoadResource.Instance.Save(_storySOList[currentRound]);
        // _leftBtn.clicked += ()=> MovementStage(-1);
        // _rightbtn.clicked += () => MovementStage(1);
        // MovementStage(0);

    }   

    void ResultClearNum(LitJson.JsonData data) {
        void MovementStage(int value)
        {
            Debug.Log("눌림");
            currentRound += value;
            if(currentRound < 0)
            {
                currentRound =0;
            }
            if(currentRound >= _maxStage)
            {
                currentRound = _maxStage - 1;
            }
            StoryUISO _so = _storySOList[currentRound];
            StoryLoadResource.Instance.Init = Init;
            StoryLoadResource.Instance.Out = Out;

            _roundTxt.text = $"Round {_so.id}.";
            _titleTxt.text = _so.TitleName;
            _imagePanel.style.backgroundImage = new StyleBackground(_so.StageSprite);
            _expTxt.text = _so.StageExample;
            _enemyImage.style.backgroundImage = new StyleBackground(_so.EnemySprite);
            _enemyNameTxt.text = _so.EnemyName;
            _enemyInfoTxt.text =_so.EnemyInfo;

            if (_so.id <= ((int)data + 1)) 
            {
                _gameEnter.text = "에피소드 입장"; // 플레이 가능
                _gameEnter.SetEnabled(true);
            }else
            {
                _gameEnter.text = "입장 불가";
                _gameEnter.SetEnabled(false);
            }
        }
        _leftBtn.clicked += ()=> MovementStage(-1);
        _rightbtn.clicked += () => MovementStage(1);

        MovementStage(0);
        print("현재 꺤 스테이지까지 : "+(int)data);
    }
}
