using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

[CreateAssetMenu(menuName = "SO/Story/StoryScript")]
public class StoryScriptSO : ScriptableObject
{

    public Texture2D DefaultBackGround;
    public List<StoryClass> Script = new();
}

[System.Serializable]
public class StoryClass
{
    [Header("Background")]
    public Texture2D BG;
    [Header("false : Side | true : Middle")]
    public bool IsMiddle = false;
    [Header("false : Ch1 | true : Ch2")]
    public bool IsSay = false;
    public CharacterSO Ch;
    public Character_Face _faceOne;
    [TextArea] public string Script;
    [Header("----- Two-----")]
    //[Header("fale = Single | true = Dobule")]
    //public bool Value = false;
    //[ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(Value))]
    public CharacterSO TwoCh;
    //[ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(Value))]
    [Header("false : Default | true : reverse")] public bool Position;
    //[ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(Value))]
    public Character_Face _faceTwo;
    

}
