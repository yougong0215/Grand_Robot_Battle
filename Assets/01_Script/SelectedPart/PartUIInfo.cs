using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PartUIInfo : MonoBehaviour
{
    RobotSettingAndSOList _robot;
    [SerializeField] PartSO pat;
    bool bfi;

    public PartSO Part => pat;

    public string token {get; private set;} = "";
    UnitPart p;


    private void Awake()
    {
        _robot = GameObject.Find("BaseRobot").GetComponent<RobotSettingAndSOList>();
    }

    public bool GetIt()
    {
        return pat != null;
    }

    public void Seleted(UnitPart pa, PartSO _partSO = null, string token = "")
    {
        if(token != "")
            this.token = token;

        foreach (var a in transform.GetComponentsInChildren<UnitPart>())
        {
            if(a != pa)
                a.SetPartClick((false));
        }

        p = pa;

        if (pat != null)
        {

            //obj.GetComponent<UnitPart>().SetPartClick(false);

            for (int i = 0; i < pat._part.Count; i++)
            {
                _robot.DeEquip(pat, pat._part[i].enums);
            }

        }

        pat = _partSO;

        if (pat == null)
            GetComponent<Image>().color = Color.white;
        else
            GetComponent<Image>().color = Color.black;

        _robot.SetingRealPart(pat);





    }



}
