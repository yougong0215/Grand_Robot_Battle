using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static ShowIfAttribute;
using static UnityEngine.Rendering.DebugUI;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEditor;

[CreateAssetMenu(menuName = "SO/Story/StoryScript")]
public class StoryScriptSO : ScriptableObject
{


    public List<StoryClass> Script = new();
}

[System.Serializable]
public class StoryClass
{
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

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class ShowIfAttribute : PropertyAttribute
{
    public enum ConditionOperator
    {
        And,
        Or,
    }

    public enum ActionOnConditionFail
    {

        DontDraw,
        JustDisable,
    }

    public ActionOnConditionFail Action { get; private set; }
    public ConditionOperator Operator { get; private set; }
    public string[] Conditions { get; private set; }

    public ShowIfAttribute(ActionOnConditionFail action, ConditionOperator conditionOperator, params string[] conditions)
    {
        Action = action;
        Operator = conditionOperator;
        Conditions = conditions;
    }


}