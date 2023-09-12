using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class SoundManager : Singleton<SoundManager>
{


    private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _sfxList = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _bgmList = new List<AudioClip>();

    private void Awake()
    {

                DontDestroyOnLoad(this.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(SFXSoundType _soundType)
    {
        AudioSource asource = SoundManager.Instance.gameObject.AddComponent<AudioSource>();
        asource.clip = _sfxList[(int)_soundType];
        asource.Play();
        Debug.Log("되긴함");
        StartCoroutine(Destoryed(asource));
    }

    IEnumerator Destoryed(AudioSource sd)
    {
        yield return null;
        yield return new WaitUntil(() => sd.isPlaying == false);
        Destroy(sd);
    }

    public void PlayBGM(BGSoundType _bgType)
    {
        if (_audioSource.isPlaying)
            _audioSource.Stop();
        _audioSource.clip = _bgmList[(int)_bgType];
        _audioSource.Play();
    }
}
