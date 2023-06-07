using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRobot : Singleton<SimpleRobot>
{
    RobotSettingAndSOList _robot;
    [SerializeField] PartSO Left;
    [SerializeField] PartSO Right;
    [SerializeField] PartSO Head;
    [SerializeField] PartSO Body;
    [SerializeField] PartSO Leg;

    public void Setting(PartSO so, PartBaseEnum b)
    {
        switch (b)
        {
            case PartBaseEnum.Left:
                Left = so;
                break;
            case PartBaseEnum.Right:
                Right = so;
                break;
            case PartBaseEnum.Head:
                Head = so;
                break;
            case PartBaseEnum.Body:
                Body = so;
                break;
            case PartBaseEnum.Leg:
                Leg = so;
                break;
        }
    }
     public void FindAndSet()
    {
        _robot = GameObject.Find("MyRobot").GetComponent<RobotSettingAndSOList>();
        Set(Left);
        Set(Right);
        Set(Head);
        Set(Body);
        Set(Leg);
    }

    private void Set(PartSO _partSO)
    {
        if (_partSO != null)
        {
            _robot.SetingRealPart(_partSO.PartBase, _partSO);
            for (int i = 0; i < _partSO._part.Count; i++)
            {
                _robot.EquipPart(_partSO._part[i].enums, _partSO);
            }
        }
    }
    
}