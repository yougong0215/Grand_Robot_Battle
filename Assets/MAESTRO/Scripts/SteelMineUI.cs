using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SteelMineUI : MonoBehaviour
{
    UIDocument _doc;
    VisualElement _root;
    Button _myRobotSelectBtn;
    Label _enemyHPTxt;
    Button _miningStartBtn;
    Label _limitTrunText;
    Label _abilityName;
    Label _abilityInfoText;
    Label _enterCountTxt;

    VisualElement _errorPanel;
    Button _okBtn;
    bool onPanel;

    [field:SerializeField] public int EnterCount { get; set; } 

    private void Awake()
    {
        EnterCount = 3;
        _doc = GetComponent<UIDocument>();
    }

    private void SetPanel()
    {
        if(onPanel)
        {
            _errorPanel.AddToClassList("off");
        }
        else
        {
            _errorPanel.RemoveFromClassList("off");
        }
        onPanel = !onPanel;
    }

    private void EnterMining()
    {
        if(EnterCount > 0)
        {
            EnterCount--;
            _enterCountTxt.text = $"{EnterCount}/3";
        }
        else
        {
            SetPanel();
        }
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _myRobotSelectBtn = _root.Q<Button>("MyRobotSelectBtn");
        _enemyHPTxt = _root.Q<Label>("HPTxt");
        _miningStartBtn = _root.Q<Button>("MiningStartBtn");
        _miningStartBtn.clicked += EnterMining;
        _limitTrunText = _root.Q<Label>("LimitTrunText");
        _abilityName = _root.Q<Label>("AbilityName");
        _abilityInfoText = _root.Q<Label>("AbilityInfoText");
        _errorPanel = _root.Q<VisualElement>("ErrorPanel");
        _okBtn = _root.Q<Button>("OkBtn");
        _okBtn.clicked += SetPanel;
        _enterCountTxt = _root.Q<Label>("EnterCount");
    }
}
