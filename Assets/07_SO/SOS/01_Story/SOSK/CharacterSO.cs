using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character_Face
{
    Default = 0,
    Laugh = 1,
    Sad = 2,
    Angry = 3,
    ridiculous = 4,

}

[CreateAssetMenu(menuName = "SO/Story/Character")]
public class CharacterSO : ScriptableObject
{
    [Header("Info")]
    [SerializeField] public string names;
    [SerializeField] public string Exname;
    [Header("Default is Very Importent")]
    [SerializeField] Sprite Default;

    [Header("Is it null = Default")]
    [SerializeField] Sprite Laugh;
    [SerializeField] Sprite Sad;
    [SerializeField] Sprite Angry;
    [SerializeField] Sprite Ridiculous;

    Dictionary<Character_Face, Sprite> _info = new();

    
    public void Init()
    {
        _info[Character_Face.Default] = Default;
        _info[Character_Face.Laugh] = Laugh;
        _info[Character_Face.Sad] = Sad;
        _info[Character_Face.Angry] = Angry;
        _info[Character_Face.ridiculous] = Ridiculous;
    }

    public Sprite ReturnSprite(Character_Face en)
    {
        Init();
        if (_info[en] != null)
        {

            return _info[en];
        }
        return Default;
    }
}
