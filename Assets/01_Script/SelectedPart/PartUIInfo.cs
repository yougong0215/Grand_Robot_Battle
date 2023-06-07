using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartUIInfo : MonoBehaviour
{
    RobotSettingAndSOList _robot;
    [SerializeField]PartSO pat;
    bool bfi;
    private void Awake()
    {
        _robot = GameObject.Find("BaseRobot").GetComponent<RobotSettingAndSOList>();
    }

    public void Seleted(ContentPartAdd pa, PartSO _partSO = null)
    {
        if(pat != null)
        {
            _robot.InitSet();
            GameObject obj = transform.GetChild(0).gameObject;
            obj.GetComponent<UnitPart>().SetPartClick(false);

            for (int i = 0; i < pat._part.Count; i++)
            {
                _robot.DeEquip(pat, pat._part[i].enums);
            }
        }



        pat = _partSO;
        _robot.InitSet();

        if(_partSO != null)
        for (int i = 0; i < _partSO._part.Count; i++)
        {
            _robot.EquipPart(_partSO._part[i].enums, _partSO);
        }





    }
    private void Update()
    {

    }



}
