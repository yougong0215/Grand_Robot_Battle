using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy")]
public class BattleEnemySO : ScriptableObject
{
    public PartSO Head;
    public PartSO Body;
    public PartSO LeftHand;
    public PartSO RightHand;
    public PartSO Leg;  
}
