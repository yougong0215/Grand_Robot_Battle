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

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _bgSlider = _root.Q<Slider>("bg-slider");
        _percentage[0] = _bgSlider.Q<Label>("percentage");
        _sfxSlider = _root.Q<Slider>("sfx-slider");
        _percentage[1] = _sfxSlider.Q<Label>("percentage");
        _uiSlider = _root.Q<Slider>("ui-slider");
        _percentage[2] = _uiSlider.Q<Label>("percentage");
        _rvSliders = _root.Q<Slider>("rv-slider");
        _percentage[3] = _rvSliders.Q<Label>("percentage");

        _bgSlider.RegisterValueChangedCallback(evt => OnSliderValueChange(evt, SoundSetting.background));
        _sfxSlider.RegisterValueChangedCallback(evt => OnSliderValueChange(evt, SoundSetting.sfx));
        _uiSlider.RegisterValueChangedCallback(evt => OnSliderValueChange(evt, SoundSetting.uisound));
        _rvSliders.RegisterValueChangedCallback(evt => OnSliderValueChange(evt, SoundSetting.rv));
    }

    private void OnSliderValueChange(ChangeEvent<float> evt, SoundSetting ss)
    {
        _percentage[(int)ss].text = $"{evt.newValue}%";
    }
}
