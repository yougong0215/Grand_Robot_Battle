using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public int HP = 100;
    public int ATK = 10;
    public int DEF = 10;
    public int SPEED = 10;
    public int Barrier = 0;
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
    [SerializeField] public List<Parts> _part;
    [SerializeField] public string SOname;
    [SerializeField] public Sprite Sprite;
    [SerializeField] public Stat Statues;
    [SerializeField] [TextArea] public string Explain;
    [SerializeField] public bool ReplaceMesh = false;
    [SerializeField] public bool EquipPart = false;
    [SerializeField] public Sprite SkillImage;
    [SerializeField] public PartBaseEnum PartBase;
    [SerializeField] public Sprite EquipImage;
    [SerializeField] public AnimationClip clips;


    [Header("스킬 계수")]
    [SerializeField] public string names = "";
    [SerializeField] public float Count = 1;
    [SerializeField] [TextArea] public string Daesa = "아직 스킬 대사가 없습니다";

}
