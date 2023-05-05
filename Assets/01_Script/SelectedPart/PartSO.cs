using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartEnum
{
    None,
    RightArm,
    LeftArm,
    Legs,
    Head,
    Body,
}


[System.Serializable]
public class Stat
{
    public int HP;
    public int ATK;
    public int DEF;
    public int SPEED;
}

[CreateAssetMenu(menuName =("SO/PartUI"))]
public class PartSO : ScriptableObject
{
    [SerializeField] public PartEnum Type;
    [SerializeField] public GameObject PartAsset;
    [SerializeField] public Sprite Sprite;
    [SerializeField] public Stat Statues;
    [SerializeField][TextArea] public string Explain;
    [SerializeField] public bool RepalceMesh = false;
    [SerializeField] public bool EquipPart = false;
}
