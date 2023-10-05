using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using LitJson;
using UnityEditor.VersionControl;
using UnityEngine.Purchasing.MiniJSON;

public enum SFXSoundType
{
    BtnClick = 0,
    Beep1 = 1,
    Beep2,
    Drill,
    Explosion,
    Gun1,
    Gun2,
    Gun3,
    Gun4,
    HatchOpen,
    Hit,
    Ignite,
    PowerDown1,
    PowerDown2,
    PowerDown3,
    R_Emergency,
    R_GrrSound,
    R_StandardSound1,
    R_StandardSound2,
    R_StandardSound3,
    R_StandardSound4,
    R_StandardSound5,
    R_StandardSound6,
    Setting1,
    Setting2
}

public enum BGSoundType
{
    Opening = 0,
    Lobby = 1,
    Game1,
    Game2,
    Win
}

public class SoundJson
{
    public float Master = 50.0f;
    public float SFX= 50.0f;
    public float UI= 50.0f;
    public float BGM= 50.0f;
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private List<AudioClip> _sfxList = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _bgmList = new List<AudioClip>();

    [SerializeField] private AudioSource _backgroundSound;
    public AudioMixer _mixer;

    private SoundJson _data;

    private void Start()
    {
        
        DontDestroyOnLoad(this.gameObject);
        SoundJson savedata = new SoundJson();
        try
        {
            string path = Path.Combine(Application.dataPath, "playerData.json");
            string jsonData = File.ReadAllText(path);
            savedata = JsonUtility.FromJson<SoundJson>(jsonData);
            Debug.LogWarning("성공");
        }
        catch
        {
            string jsonData = JsonUtility.ToJson(savedata);
            string path = Path.Combine(Application.dataPath, "playerData.json");
            File.WriteAllText(path, jsonData);
            Debug.LogWarning("첫실행 or 그냥 애러");
        }


        _data = savedata;
        
        print(savedata.Master);
        MixerSave(SoundSetting.Master, _data.Master);
        MixerSave(SoundSetting.background, _data.BGM);
        MixerSave(SoundSetting.UISound, _data.UI);
        MixerSave(SoundSetting.SFX,_data.SFX);
    }

    public void MixerSave(SoundSetting ss, float value)
    {
        value = Mathf.RoundToInt(value);
        if (value<= .0f)
        {
            _mixer.SetFloat(ss.ToString(), -80f);
        }
        else
        {
            _mixer.SetFloat(ss.ToString(), Mathf.Lerp(-40,0, value * 0.01f));
        }
        switch (ss)
        {
            case SoundSetting.Master:
                _data.Master = value;
                break;
            case SoundSetting.UISound:
                _data.UI = value;
                break;
            case SoundSetting.SFX:
                _data.SFX = value;
                break;
            case SoundSetting.background:
                _data.BGM = value;
                break;
            default:
                break;
        }
        
        
        string path = Path.Combine(Application.dataPath, "playerData.json");
        string jsonData = JsonUtility.ToJson(_data);
        File.WriteAllText(path, jsonData);
        
        Debug.LogWarning((value));
    }

    public float GetValue(SoundSetting ss)
    {
        switch (ss)
        {
            case SoundSetting.Master:
                return _data.Master;
                case SoundSetting.UISound:
                    return _data.UI;
                case SoundSetting.SFX:
                    return _data.SFX;
                case SoundSetting.background:
                    return _data.BGM;
                default:
                    break;
        }

        return _data.Master;
    }
    
    
    public void PlaySFX(SFXSoundType _soundType)
    {
        GameObject bg = Instantiate(new GameObject());
        _backgroundSound = bg.AddComponent<AudioSource>();
        
        _backgroundSound.clip = _sfxList[(int)_soundType];
        _backgroundSound.Play();
    }

    public void SceneLoadDestory()
    {
        AudioSource[] ad = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource s in ad)
        {
            if (s.isPlaying)
            {
                s.Stop();
            }
            Destroy(s);
        }
    }
    public void PlayBGM(BGSoundType _bgType)
    {
        if (_backgroundSound != null)
        {
            _backgroundSound.Stop();

            Destroy(_backgroundSound.gameObject);
        }

        
        GameObject bg = Instantiate(new GameObject());
        
        DontDestroyOnLoad(bg);
        _backgroundSound = bg.AddComponent<AudioSource>();
        
        _backgroundSound.clip = _bgmList[(int)_bgType];
        _backgroundSound.Play();
    }
}
