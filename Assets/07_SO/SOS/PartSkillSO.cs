using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


//[CreateAssetMenu(menuName = "SO/Skill")]
public abstract class PartSkillSO : ScriptableObject
{
    protected Label _panel;
    protected PartSO _part;
    protected RobotSettingAndSOList _me;
    protected RobotSettingAndSOList _enemy;

    protected AnimationBind _anim;

    public Action _act;

    bool bSelected = false;
    public virtual void Init(Label _lb,RobotSettingAndSOList me, RobotSettingAndSOList Enemy, PartSO so, AnimationBind anim)
    {
        _panel = _lb;   
        _me = me;
        _enemy = Enemy;
        _part = so;
        _anim = anim;
        
        if(bSelected==false)
        {
            bSelected= true;
            _act += AnimStart;
            _act += UseingSKill;
        }
    }

    public void AnimStart()
    {
        _me.GetComponent<AnimationBind>().AnimationChange(_part.clips);
    }

    public abstract bool IsEnd();
    protected abstract void UseingSKill();
}
