using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FinishBattle : MonoBehaviour
{
    public float MaxExpAmountValue; // EXP 통 크기
    public float CurrentExpAmountValue; // 현재 EXP 양
    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _resultPanel;
    private Label _stageTxt;
    private Label _resultText;
    private Button _resultExitBtn;
    private Button _resultConnectBtn;
    private VisualElement _expValue;
    private Label _plusExpTxt;
    private VisualElement _getItemList;
    [SerializeField] private VisualTreeAsset _getItem;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _resultPanel = _root.Q<VisualElement>("ResultPanel");
        _stageTxt = _root.Q<Label>("StageText");
        _resultText = _root.Q<Label>("ResultText");
        _resultExitBtn = _root.Q<Button>("ResultExitbtn");
        _resultConnectBtn = _root.Q<Button>("ResultConnectbtn");
        _expValue = _root.Q<VisualElement>("ExpValue");
        _plusExpTxt = _root.Q<Label>("PlusExpValue");
        _getItemList = _root.Q<VisualElement>("GetItemList");

        _resultExitBtn.clicked += ExitInGame;
        _resultConnectBtn.clicked += ConnectInGame;
    }

    public void OpenPanel(bool isOpen)
    {
        if(isOpen)
        {
            _resultPanel.RemoveFromClassList("off");
        }
        else
        {
            _resultPanel.AddToClassList("off");
        }
    }

    public void SetInfoText(string stageText, bool isWin, float expValue)
    {
        _stageTxt.text = stageText;
        if(isWin)
        {
            _resultText.text = "Victory";
        }
        else
        {
            _resultText.text = "Defeat";
        }
        _plusExpTxt.text = expValue.ToString();
        CurrentExpAmountValue += expValue;
        _expValue.style.width = CurrentExpAmountValue / MaxExpAmountValue;
    }

    VisualElement _getItemElement;
    public void GetItemListSetting(/*var itemInfo*/)
    {
        _getItemElement = _getItem.Instantiate();
        _getItemList.Add(_getItemElement);
    }

    private void ExitInGame()
    {

    }

    private void ConnectInGame()
    {

    }
}
