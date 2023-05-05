using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UISlotManager : MonoBehaviour
{


    public UnitPart Prefabs;


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
    }
    private void OnEnable()
    {
        instance = this;
    }

    public UIDragAndDrop UnitUI;
    public UISlot SoltUI;

    public List<PartTypeSelect> CategorySelected;


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
