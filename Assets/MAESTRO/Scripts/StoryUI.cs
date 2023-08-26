using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryUI : MonoBehaviour
{
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

    int currentRound = 0;
    int _maxStage;


    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
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

        _maxStage = _storySOList.Count - 1;

        
    }   

    void MovementStage(int value)
    {
        if(currentRound >= 0 && currentRound < _maxStage)
        {
            currentRound += value;
        }
        StoryUISO _so = _storySOList[currentRound];

        _titleTxt.text = _so.TitleName;
        _imagePanel.style.backgroundImage = new StyleBackground(_so.StageSprite);
        _expTxt.text = _so.StageExample;
        _enemyImage.style.backgroundImage = new StyleBackground(_so.EnemySprite);
        _enemyNameTxt.text = _so.EnemyName;
        _enemyInfoTxt.text =_so.EnemyInfo;
        

        


    }



     
}
