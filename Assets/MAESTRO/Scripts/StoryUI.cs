using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryUI : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;

    private Button _rightbtn;
    private Button _leftBtn;

    private Label _roundTxt;
    private Label _titkeTxt;
    private VisualElement _phaseImage;
    private Label _expTxt;
    private VisualElement _ingPanel;
    private Label _ingTxt;

    private VisualElement _enemyImage;
    private Label _enemyNameTxt;
    private Label _enemyInfoTxt;

    int currentRound = 1;


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
        _titkeTxt = _root.Q<Label>("TitleTxt");
        _phaseImage = _root.Q<VisualElement>("ImagePanel");
        _expTxt = _root.Q<Label>("ExpText");
        _ingPanel = _root.Q<Label>("ingPanel");
        _ingTxt = _root.Q<Label>("ingTxt");
        _enemyImage = _root.Q<VisualElement>("EnemyImage");
        _enemyNameTxt = _root.Q<Label>("EnemyName");
        _enemyInfoTxt = _root.Q<Label>("infoTxt");
    }
}
