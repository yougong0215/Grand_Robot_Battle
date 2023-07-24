using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartUIInfo : MonoBehaviour
{
    RobotSettingAndSOList _robot;
    [SerializeField] PartSO pat;
    bool bfi;

    public PartSO Part => pat;

    public string token {get; private set;} = "";

    private void Awake()
    {
        _robot = GameObject.Find("BaseRobot").GetComponent<RobotSettingAndSOList>();
    }

    public void Seleted(ContentPartAdd pa, PartSO _partSO = null, string token = "")
    {
        this.token = token;

        PartBaseEnum ot = PartBaseEnum.Error;
        if(pat != null)
        {
            ot = pat.PartBase;
            _robot.InitSet();
            GameObject obj = transform.GetChild(0).gameObject;
            obj.GetComponent<UnitPart>().SetPartClick(false);

            for (int i = 0; i < pat._part.Count; i++)
            {
                _robot.DeEquip(pat, pat._part[i].enums);
            }
        }

        if(_partSO != null)
        {
            ot = _partSO.PartBase;
        }

        pat = _partSO;
        _robot.InitSet();

        // SimpleRobot.Instance.Setting(pat, ot);

        _robot.SetingRealPart(pat);





    }



}
