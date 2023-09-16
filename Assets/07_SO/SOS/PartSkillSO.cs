using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;


//[CreateAssetMenu(menuName = "SO/Skill")]
public abstract class PartSkillSO : ScriptableObject
{
    [Description("Last List is done Anim Z, if List is Empty")]
    [SerializeField] protected List<float> EventTime = new();

    [Header("If BuffHas")]
    [SerializeField] public int Bufftime;
    int _realTimeBuff = 0;
    
    [SerializeField] public Stat Buff = new Stat();
    protected Stat AddingBuff = new Stat();

    [Header("Debuff")]
    [SerializeField] public int DeBufftime;
    [SerializeField] public Stat DeBuff = new Stat();
    int _realTimeDebuff;
    protected Stat AddingDeBuff = new Stat();

    protected PVPUI _pvp;
    protected PartSO _part;
    protected RobotSettingAndSOList _me;
    protected RobotSettingAndSOList _enemy;

    protected AnimationBind _anim;

    public Action _act;

    bool bSelected = false;
    protected bool isEnd= false;

    Coroutine BuffCo;
    Coroutine DeBuffCo;
    protected PVP_GameResult _result;

    
    public virtual void Init(PVPUI pvp,RobotSettingAndSOList me, RobotSettingAndSOList Enemy, PartSO so, AnimationBind anim, PVP_GameResult _re = null)
    {
         _pvp = pvp;
        _me = me;
        _enemy = Enemy;
        _part = so;
        _anim = anim;
        isEnd= false;
        _result = _re;
        
        if(bSelected==false)
        {
            bSelected= true;
            _act += AnimStart;
            _act += StartAction;
        }
        Start();
        _realTimeBuff = Bufftime;
        _realTimeDebuff = DeBufftime;
    }

    public void AnimStart()
    {
        _me.GetComponent<AnimationBind>().AnimationChange(_part.clips);
    }

    public bool IsEnd()
    {
        return isEnd;
    }
    public void StopBuff()
    {
        _me._statues /= AddingBuff;

        if (BuffCo != null)
            _pvp.StopCoroutine(BuffCo);
    }

    public void TurnEnd()
    {
        _realTimeBuff--;
        _realTimeDebuff--;
    }

    public void AddBuffing()
    {
        AddingBuff *= Buff;
        
        _me._statues *= Buff;
        _me._statues.PercentDef = Buff.PercentDef;
    }
    public void AddDeBuff()
    {
        AddingDeBuff /= DeBuff;
        _enemy._statues /= DeBuff;
    }

    public void StopDeBuff()
    {
        _enemy._statues *= DeBuff;
        if (DeBuffCo != null)
            _pvp.StopCoroutine(DeBuffCo);
    }

    protected abstract void Start();
    private void StartAction()
    {
        BuffCo = _pvp.StartCoroutine(BuffBind());
        DeBuffCo = _pvp.StartCoroutine(DeBuffBind());
        _pvp.StartCoroutine(UseingSKill());
    }
    protected abstract IEnumerator UseingSKill();
    
    protected virtual IEnumerator BuffBind()
    {
        AddBuffing();
        while(_realTimeBuff > 0)
        {
            int t = _realTimeBuff;
            yield return t != _realTimeBuff;
            
        }
        StopBuff();
    }
    protected virtual IEnumerator DeBuffBind()
    {
        AddDeBuff();
        while (_realTimeDebuff > 0)
        {
            int t = _realTimeDebuff;
            yield return t != _realTimeDebuff;
        }
        StopDeBuff();
    }
}
