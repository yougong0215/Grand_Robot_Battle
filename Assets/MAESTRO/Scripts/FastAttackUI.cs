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


    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void DownEnterCount(StageEnterData data)
    {
        data.LeftCount--;
        data.CountTxt.text = $"{data.LeftCount}/3";
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

    }
}
