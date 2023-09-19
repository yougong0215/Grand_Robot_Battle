using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum SoundSetting
{
    background,
    sfx,
    uisound,
    rv

}

public class Setting : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;

    private Slider _bgSlider;
    private Slider _sfxSlider;
    private Slider _uiSlider;
    private Slider _rvSliders;
    private Label[] _percentage = new Label[4];
    private Button _exitBtn;
    Button RemoveAcount;
    VisualElement RemovePanel;
    Button NoRemoveBtn;
    Button YesRemoveBtn;
    TextField _input;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _exitBtn = _root.Q<Button>("exit-btn");
        _exitBtn.clicked += () => ActivePanel(false);

        _bgSlider = _root.Q<Slider>("bg-slider");
        _percentage[0] = _bgSlider.Q<Label>("percentage");
        _sfxSlider = _root.Q<Slider>("sfx-slider");
        _percentage[1] = _sfxSlider.Q<Label>("percentage");
        _uiSlider = _root.Q<Slider>("ui-slider");
        _percentage[2] = _uiSlider.Q<Label>("percentage");
        _rvSliders = _root.Q<Slider>("rv-slider");
        _percentage[3] = _rvSliders.Q<Label>("percentage");

        RemovePanel = _root.Q<VisualElement>("RemovePanel");
        NoRemoveBtn = _root.Q<Button>("NoBtn");
        YesRemoveBtn = _root.Q<Button>("RemoveBtn");
        RemoveAcount = _root.Q<Button>("AcountRemoveBtn");

        RemoveAcount.clicked += () => Debug.Log("ㅁㅁ");
        RemoveAcount.clicked += () => RemovePanel.RemoveFromClassList("off");

        NoRemoveBtn.clicked += () => RemovePanel.AddToClassList("off");
        YesRemoveBtn.clicked += () => RemoveAcountMethod();


        _bgSlider.RegisterValueChangedCallback(evt => OnSliderValueChange(evt, SoundSetting.background));
        _sfxSlider.RegisterValueChangedCallback(evt => OnSliderValueChange(evt, SoundSetting.sfx));
        _uiSlider.RegisterValueChangedCallback(evt => OnSliderValueChange(evt, SoundSetting.uisound));
        _rvSliders.RegisterValueChangedCallback(evt => OnSliderValueChange(evt, SoundSetting.rv));

        _root.style.display = DisplayStyle.None;
    }

    public void ActivePanel(bool isActive)
    {
        Debug.LogWarning("정상 실행됨");
        _root.style.display = isActive ? DisplayStyle.Flex : DisplayStyle.None;
    }

    private void OnSliderValueChange(ChangeEvent<float> evt, SoundSetting ss)
    {
        _percentage[(int)ss].text = $"{evt.newValue}%";
    }

    void RemoveAcountMethod()
    {
        if(_input.value == "") // 이거 추가
        {
            // 여서 삭제
        }
    }
}
