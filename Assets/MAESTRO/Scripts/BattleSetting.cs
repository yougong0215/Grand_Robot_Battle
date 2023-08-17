using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleSetting : MonoBehaviour
{
    UIDocument _doc;
    VisualElement _root;
    VisualElement _settingPanel;
    Slider _volSlider;
    Button _exitBtn;
    Button _surenderBtn;
    Label _stageInfoTxt;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _settingPanel = _root.Q<VisualElement>("SettingPanel");
        _volSlider = _root.Q<Slider>("VolumeSlider");
        _exitBtn = _root.Q<Button>("SettingExit");
        _surenderBtn = _root.Q < Button > ("SurrenderBtn");
        _stageInfoTxt = _root.Q<Label>("StageInfoTxt");

        _exitBtn.clicked += () => SetPanel(false, null);
        //_surenderBtn.clicked += 항복로직
    }

    public void SetPanel(bool isActive, string stageInfo)
    {
        if(isActive)
        {
            _stageInfoTxt.text = stageInfo;
            _settingPanel.RemoveFromClassList("off");
        }
        else
        {
            _settingPanel.AddToClassList("off");
        }
    }    
}
