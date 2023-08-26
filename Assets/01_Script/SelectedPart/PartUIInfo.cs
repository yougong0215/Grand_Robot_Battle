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

    private void Awake()
    {
        _robot = GameObject.Find("BaseRobot").GetComponent<RobotSettingAndSOList>();
    }

    public void Seleted(ContentPartAdd pa, PartSO _partSO = null, string token = "")
    {
        this.token = token;
        GetComponent<Image>().color = Color.white;

        if(pat != null)
        {
            GameObject obj = transform.GetChild(0).gameObject;
            obj.GetComponent<UnitPart>().SetPartClick(false);

            for (int i = 0; i < pat._part.Count; i++)
            {
                _robot.DeEquip(pat, pat._part[i].enums);
            }
            GetComponent<Image>().color = Color.black;
        }

        pat = _partSO;

        _robot.SetingRealPart(pat);





    }



}
