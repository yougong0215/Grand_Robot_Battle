using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


//[CreateAssetMenu(menuName = "SO/Skill")]
public abstract class PartSkillSO : ScriptableObject
{
    protected PVPUI _pvp;
    protected PartSO _part;
    protected RobotSettingAndSOList _me;
    protected RobotSettingAndSOList _enemy;

    protected AnimationBind _anim;

    public Action _act;

    bool bSelected = false;
    bool isEnd= false;
    public virtual void Init(PVPUI pvp,RobotSettingAndSOList me, RobotSettingAndSOList Enemy, PartSO so, AnimationBind anim)
    {
         _pvp = pvp;
        _me = me;
        _enemy = Enemy;
        _part = so;
        _anim = anim;
        isEnd= false;
        
        if(bSelected==false)
        {
            bSelected= true;
            _act += AnimStart;
            _act += StartAction;
        }
        Start();
    }

    public void AnimStart()
    {
        _me.GetComponent<AnimationBind>().AnimationChange(_part.clips);
    }

    public bool IsEnd()
    {
        return isEnd;
    }

    protected abstract void Start();
    private void StartAction()
    {
        _pvp.StartCoroutine("UseingSKill");
    }
    protected abstract IEnumerator UseingSKill();
}
