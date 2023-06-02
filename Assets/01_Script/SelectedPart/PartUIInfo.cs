using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartUIInfo : MonoBehaviour
{
    RobotSettingAndSOList _robot;
    PartSO pat;

    private void Awake()
    {
        _robot = GameObject.Find("BaseRobot").GetComponent<RobotSettingAndSOList>();
    }

    public void Seleted(PartSO _partSO = null, PartEnum part = PartEnum.None)
    {
        pat = _partSO;
        if (_partSO != null)
        {
            for(int i =0; i < _partSO._part.Count; i++)
            {
                _robot.EquipPart(_partSO._part[i].enums, _partSO);
            }


        }


    }


    

}
