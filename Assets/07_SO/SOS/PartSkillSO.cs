using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "SO/Skill")]
public abstract class PartSkillSO : ScriptableObject
{
    protected PartSO _part;
    protected RobotSettingAndSOList _me;
    protected RobotSettingAndSOList _enemy;

    public Action _act;
    public virtual void Init(RobotSettingAndSOList me, RobotSettingAndSOList Enemy, PartSO so)
    {
        _me = me;
        _enemy = Enemy;
        _part = so;
        
        
        _act += UseingSKill;
    }

    protected abstract void UseingSKill();
}
