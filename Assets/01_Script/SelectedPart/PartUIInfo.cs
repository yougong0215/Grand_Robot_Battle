using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartUIInfo : MonoBehaviour
{
    RobotSettingAndSOList _robot;
    [SerializeField] GameObject TextPanel;
    [SerializeField] TextMeshProUGUI ATK;
    [SerializeField] TextMeshProUGUI DEF;
    [SerializeField] TextMeshProUGUI HP;
    [SerializeField] TextMeshProUGUI SPEED;
    [SerializeField] TextMeshProUGUI TMPPanel;
    PartSO pat;

    private void Awake()
    {
        _robot = GameObject.Find("MyRobot").GetComponent<RobotSettingAndSOList>();
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

    private void Update()
    {
        if (pat != null)
        {
            TextPanel.SetActive(true);
            TMPPanel.text = pat.Explain;
            ATK.text = $"ATK : {pat.Statues.ATK}";
            DEF.text = $"DEF : {pat.Statues.DEF}";
            SPEED.text = $"SPEED : {pat.Statues.SPEED}";
            HP.text = $"HP : {pat.Statues.HP}";
        }

    }

}
