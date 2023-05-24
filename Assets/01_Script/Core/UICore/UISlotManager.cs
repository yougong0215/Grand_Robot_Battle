using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UISlotManager : MonoBehaviour
{
    RobotSettingAndSOList _robot;

    public UnitPart Prefabs;


    [SerializeField] TextMeshProUGUI ATK;
    [SerializeField] TextMeshProUGUI DEF;
    [SerializeField] TextMeshProUGUI HP;
    [SerializeField] TextMeshProUGUI SPEED;


    static UISlotManager instance = null;
    public static UISlotManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.Log("ㅈㄴ 느린 유니티");
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        _robot = GameObject.Find("MyRobot").GetComponent<RobotSettingAndSOList>();
    }
    private void OnEnable()
    {
        instance = this;
    }

    public UIDragAndDrop UnitUI;
    public UISlot SoltUI;

    public List<PartTypeSelect> CategorySelected;

    void Update()
    {
        ATK.text = $"ATK : { _robot._statues.ATK}";
            DEF.text = $"DEF : { _robot._statues.DEF}";
        SPEED.text = $"SPEED : { _robot._statues.SPEED}";
        HP.text = $"HP : {_robot._statues.HP}";
    }


    public void NullUIS()
    {
        UnitUI = null;
        SoltUI = null;
    }

    public void PartSelected(PartEnum parts = PartEnum.None)
    {
        for(int i =0; i < CategorySelected.Count; i++)
        {
            if (CategorySelected[i].PartType == parts)
            {
                CategorySelected[i].Selected();
            }
            else
            {
                CategorySelected[i].NotSelected();
            }
        }
    }

}
