using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISlotManager : MonoBehaviour
{
    RobotSettingAndSOList _robot;
    [SerializeField] private List<PartUIInfo> _partuiInfos = new();

    public UnitPart Prefabs;


    [SerializeField] TextMeshProUGUI ATK;
    [SerializeField] TextMeshProUGUI DEF;
    [SerializeField] TextMeshProUGUI HP;
    [SerializeField] TextMeshProUGUI SPEED;
    public Stat statues;

    static UISlotManager instance = null;
    public static UISlotManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.Log("���� ���� ����Ƽ");
            }
            return instance;
        }
    }
    private void Start()
    {
        instance = this;
        _robot = GameObject.Find("BaseRobot").GetComponent<RobotSettingAndSOList>();
    }
    private void OnEnable()
    {
        instance = this;
    }

    public List<PartTypeSelect> CategorySelected;

    public void PartSave()
    {
        Stat st = new();
        _partuiInfos.ForEach((v) =>
        {
            if (v.Part != null) st += v.Part.Statues;
        });
        statues = st;
    }
    
    void Update()
    {

        if(_robot._statues != null)
        {
            ATK.text = $"ATK : {statues.ATK + 100}";
            DEF.text = $"DEF : {statues.DEF +10 }";
            SPEED.text = $"SPEED : {statues.SPEED + 10}";
            HP.text = $"HP : {statues.HP + 10}";
            Debug.LogWarning("���߿� ���ľߵ�");
        }

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
