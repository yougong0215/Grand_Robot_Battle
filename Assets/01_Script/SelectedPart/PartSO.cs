using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public int HP = 100;
    public int ATK = 10;
    public int DEF = 10;
    public int SPEED = 10;
    public int Barrier = 0;
}

[CreateAssetMenu(menuName =("SO/PartUI"))]
public class PartSO : ScriptableObject
{
    [SerializeField] public PartEnum Type;
    [SerializeField] public GameObject PartAsset;
    [SerializeField] public Sprite Sprite;
    [SerializeField] public Stat Statues;
    [SerializeField] [TextArea] public string Explain;
    [SerializeField] public bool RepalceMesh = false;
    [SerializeField] public bool EquipPart = false;
    [SerializeField] public Sprite SkillImage;
    [SerializeField] public SkillScriptBase Skill;
    
}
