using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

/// <summary>
/// Right/Left | Upper/Middle/Lower | Arm/Leg
/// </summary>
public enum PartEnum
{
    

    None,
    ALU,
    ALM,
    ALL,
    ARU,
    ARM,
    ARL,
    LLU,
    LLM,
    LLL,
    LRU,
    LRM,
    LRL,
    Head,
    UpperBody,
    MiddleBody,
    LowerBody
}


[System.Serializable]
public class Stat
{
    public float HP = 0;
    public float ATK = 0;
    public float DEF = 0;
    public float SPEED = 0;
    public float Barrier = 0;

    public float PercentDef = 1;


    public static Stat operator +(Stat output, Stat input)
    {

        output.HP += input.HP;
        output.ATK += input.ATK;
        output.DEF += input.DEF;
        output.SPEED+= input.SPEED;
        output.Barrier+= input.Barrier;
        return output;
    }

    public static Stat operator -(Stat output, Stat input)
    {
        //output.HP -= input.HP;
        output.ATK -= input.ATK;
        output.DEF -= input.DEF;
        output.SPEED -= input.SPEED;
        output.Barrier -= input.Barrier;
        if (output.Barrier < 0)
        {
            output.Barrier = 0;
        }
        return output;
    }
    public static Stat operator *(Stat output, Stat input)
    {
        //output.HP -= input.HP;
        if (input.ATK != 0 || input.ATK != 0)
            output.ATK = output.ATK * input.ATK;
        if (input.DEF != 0 || input.DEF != 0)
            output.DEF = output.DEF * input.DEF;
        if (input.SPEED != 0 || input.SPEED != 0)
            output.SPEED = output.SPEED * input.SPEED;
        if (input.Barrier != 0 || input.Barrier != 0)
            output.Barrier = output.Barrier * input.Barrier;
        if (output.Barrier < 0)
        {
            output.Barrier = 0;
        }
        return output;
    }

    public static Stat operator /(Stat output, Stat input)
    {
        //output.HP -= input.HP;
        if (input.ATK != 0 || input.ATK != 0)
            output.ATK = output.ATK / input.ATK;
        if (input.DEF != 0 || input.DEF != 0)
            output.DEF = output.DEF / input.DEF;
        if (input.SPEED != 0 || input.SPEED != 0)
            output.SPEED = output.SPEED / input.SPEED;
        if (input.Barrier != 0 || input.Barrier != 0)
            output.Barrier = output.Barrier / input.Barrier;
        if (output.Barrier < 0)
        {
            output.Barrier = 0;
        }
        return output;
    }
}



[System.Serializable]
public class Parts
{
    public GameObject part;
    public PartEnum enums;

}

public enum PartBaseEnum
{
    Left,
    Right,
    Head,
    Body,
    Leg,
    Error
}

[CreateAssetMenu(menuName =("SO/PartUI"))]
public class PartSO : ScriptableObject
{
    [SerializeField] public PartBaseEnum PartBase;


    [SerializeField] public List<Parts> _part;

    [SerializeField] public string SOname;
    [SerializeField] public Sprite Sprite;

    [SerializeField] public Stat Statues;


    [SerializeField] [TextArea] public string Explain;
    [SerializeField] [TextArea] public string SkillExplain;
    [SerializeField] public bool ReplaceMesh = false;
    [SerializeField] public bool EquipPart = false;



    [SerializeField] public Sprite SkillImage;
    
    //[SerializeField] public Sprite EquipImage;


    [Header("??? ???")]
        [SerializeField] public AnimationClip clips;
    [SerializeField] public string names = "";
    [SerializeField] public float Count = 1;
    [SerializeField] [TextArea] public string Daesa = "[�����ȴ�簡�����ϴ�]";
    [FormerlySerializedAs("_skill")] [SerializeField] public PartSkillSO skillSo;

}

