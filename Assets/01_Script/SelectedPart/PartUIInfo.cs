using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartUIInfo : MonoBehaviour
{
    RobotSettingAndSOList _robot;

    private void Awake()
    {
        _robot = GameObject.Find("MyRobot").GetComponent<RobotSettingAndSOList>();
    }

    public void Seleted(PartSO _partSO = null, PartEnum part = PartEnum.None)
    {
        if(_partSO !=null)
            _robot.EquipPart(_partSO.Type, _partSO, _partSO.RepalceMesh);
        else
        {
            _robot.EquipPart(part);
        }


    }
}
