using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

class StageEnterData
{
    public Label CountTxt;
    public int LeftCount = 3;
}

public class FastAttackUI : MonoBehaviour
{
    UIDocument _doc;
    VisualElement _root;

    Dictionary<Button, StageEnterData> _stageEnterBtn = new Dictionary<Button, StageEnterData>();

    VisualElement _panel;
    Button _okBtn;
    bool onPanel;
    bool isRope;

    Label _ropeCountTxt;
    Label _errorTxt;

    [field:SerializeField] public int RopeCount { get; set ; }

    private void Awake()
    {
        RopeCount = 9;
        _doc = GetComponent<UIDocument>();
    }

    private void DownEnterCount(StageEnterData data)
    {
        if(data.LeftCount > 0 && RopeCount > 0)
        {
            data.LeftCount--;
            RopeCount--;
            data.CountTxt.text = $"{data.LeftCount}/3";
            _ropeCountTxt.text = $"{RopeCount} / 9";
        }
        else
        {
            if(RopeCount == 0)
            {
                isRope = true;
            }
            PanelSet(isRope);
        }
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        for(int i = 0; i < 3; i++)
        {
            Button _selectbtn = _root.Q<Button>($"StageEnterBtn_{i + 1}");
            Label _selectLabel = _selectbtn.Q<Label>("EnterCount");
            StageEnterData _data = new StageEnterData();
            _data.CountTxt = _selectLabel;
            _stageEnterBtn.Add(_selectbtn, _data);
        }
        foreach (KeyValuePair<Button, StageEnterData> item in _stageEnterBtn)
        {
            item.Key.clicked += () => DownEnterCount(item.Value);
        }

        _panel = _root.Q<VisualElement>("Panel");
        _okBtn = _root.Q<Button>("OkBtn");
        _ropeCountTxt = _root.Q<Label>("RopeCount");
        _okBtn.clicked += () => PanelSet(false);
        _errorTxt = _root.Q<Label>("ErrorTxt");
    }

    private void PanelSet(bool isRope)
    {
        if (!isRope)
            _errorTxt.text = "금일 도전 횟수를 전부\n 사용하셨습니다.";
        else
            _errorTxt.text = "금일 지급된 횟수를 전부\n 사용하셨습니다.";

        if(onPanel)
        {
            _panel.RemoveFromClassList("on");
        }
        else
        {
            _panel.AddToClassList("on");
        }
        onPanel = !onPanel;
    }
}
