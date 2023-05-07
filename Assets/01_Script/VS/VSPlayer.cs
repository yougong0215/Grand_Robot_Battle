using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public enum Team
{
    Red,
    Blue
}

public enum Statues
{
    Custom,
    HP,
    ATK,
    SPEED,
    DEF,
    Barrier
}


public class VSPlayer : MonoBehaviour
{
    public Team team;
    RobotSettingAndSOList _info;

    [SerializeField] public int GameNumber = -1;
    [SerializeField] GameObject SelectedSkill;

    [SerializeField] bool AI;
    [SerializeField] List<SkillScriptBase> _skill;

    [SerializeField] int SelectedSkillPanel = -1;
    [SerializeField] int SelectedEnemyPanel = -1;

    public int GetNumber()
    {
        return GameNumber;
    }

    public void SetSkillNum(int a)
    {
        SelectedSkillPanel = a;
    }

    public int GetSkillNum()
    {
        return SelectedSkillPanel;
    }

    public void SetEnemyNum(int a)
    {
        SelectedEnemyPanel = a;
    }

    public int GetEnemyNum()
    {
        return SelectedEnemyPanel;
    }



    Stat _myStat;
    public Stat CurrentStat => _myStat;


    private void Start()
    {
        _info = GetComponent<RobotSettingAndSOList>();
        SelectedSkill = GameObject.Find("SelecteSkill");
    }

    public void BattleInit()
    {
        _myStat = _info.Stat;
    }

    public void SkillAdd(SkillScriptBase skill)
    {
        _skill.Add(skill);
    }

    public IEnumerator BuffTurn(Statues enums, int value, int turn, int currentTurn, Action action = null)
    {
        switch (enums)
        {
            case Statues.HP:
                _myStat.HP += value;
                break;
            case Statues.ATK:
                _myStat.ATK += value;
                break;
            case Statues.SPEED:
                _myStat.SPEED += value;
                break;
            case Statues.DEF:
                _myStat.DEF += value;
                break;
            case Statues.Barrier:
                _myStat.Barrier += value;
                break;
            case Statues.Custom:
                action?.Invoke();
                break;
        }
        yield return new WaitUntil(() => currentTurn < VSGameController.Instance.CurrentTurn);

        switch (enums)
        {
            case Statues.HP:
                //_myStat.HP = value;
                break;
            case Statues.ATK:
                _myStat.ATK -= value;
                break;
            case Statues.SPEED:
                _myStat.SPEED -= value;
                break;
            case Statues.DEF:
                _myStat.DEF -= value;
                break;
            case Statues.Barrier:
                _myStat.Barrier -= value;
                break;
            case Statues.Custom:
                action?.Invoke();
                break;
        }

        if(turn > 0)
        {
            StartCoroutine(BuffTurn(enums, value, turn - 1, currentTurn + 1, action));
        }



    }



    public void Turn()
    {
        if(AI)
        {
            SetSkillNum(UnityEngine.Random.Range(1, 5));
            SetEnemyNum(UnityEngine.Random.Range(0, VSGameController.Instance.TeamCount(team)));
        }
        else
        {
            // UI 보여줌
            OpenSelectSkill();
        }
    }

    public void OpenSelectSkill()
    {
        for(int i =0; i< SelectedSkill.transform.childCount; i++)
        {
            SelectedSkill.transform.GetChild(i).GetComponent<VSSkillButton>().CurrentPlayer = this;
            SelectedSkill.transform.GetChild(i).GetComponent<VSSkillButton>().SetSkillUI();
        }
    }

    public void OpenSelectEnemy()
    {

    }

    public void ClossAllUI()
    {

    }

    public IEnumerator DoSkill(VSPlayer vs)
    {
        // 0은 Head 이기 때문에 항상 +1 해줘야됨
        Stat stat = _skill[GetSkillNum()].Skill(ref vs); // 애니메이션도 보여줌
        yield return new WaitUntil(() => _skill[GetSkillNum()].SkillMotionEnd());
        _skill[GetSkillNum()].Set();
    }

    public void GetDamage(int value)
    {
        if(CurrentStat.Barrier > 0)
        {
            value -= CurrentStat.Barrier;
            if(value > 0)
            {
                CurrentStat.Barrier = 0;
            }
            else
            {
                return;
            }
        }

        CurrentStat.HP -= value;
    }

    

    public void OnDead(ref int red, ref int blue)
    {
        if (CurrentStat.HP <= 0)
        {

            VSGameController.Instance._players.Remove(this);
        }
        else
        {
            if (team == Team.Red)
            {
                red++;
            }
            else
            {
                blue++;
            }
        }
    }
}
