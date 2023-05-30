using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public struct partstd
{
    [SerializeField] public List<PartSO> SOList;

}


public class PartList : MonoBehaviour
{
    public partstd l;
    [SerializeField] public List<PartSO> LeftArm;
    [SerializeField] public List<PartSO> RightArm;
    [SerializeField] public List<PartSO> Head;
    [SerializeField] public List<PartSO> Body;
    [SerializeField] public List<PartSO> Leg;
    static PartList m_Ins;
    public static PartList Instance
    {
        get
        {
            if(m_Ins == null)
            {
                Debug.LogError("Err");
            }
            return m_Ins;
        }
    }

    private void Awake()
    {
        //Destroy(PartList.Instance);
        m_Ins = this;
        JsonSave<partstd>.Save(l, "SOLIST");

        l = JsonSave<partstd>.Load(l, "SOLIST");
        
        for(int i =0; i < l.SOList.Count; i++)
        {
            switch (l.SOList[i].Type)
            {
                case PartEnum.None:
                    Debug.LogError($"{l.SOList[i]} : ÆÄÃ÷ enum ¹ÌÁöÁ¤");
                    break;
                case PartEnum.RightUpperArm:
                    RightArm.Add(l.SOList[i]);
                    break;
                case PartEnum.LeftUpperArm:
                    LeftArm.Add(l.SOList[i]);
                    break;
                case PartEnum.Legs:
                    Leg.Add(l.SOList[i]);
                    break;
                case PartEnum.Head:
                    Head.Add(l.SOList[i]);
                    break;
                case PartEnum.Body:
                    Body.Add(l.SOList[i]);
                    break;
            }
        }
    }

    public List<PartSO> ReturnInfo(PartEnum pt)
    {
        switch (pt)
        {
            case PartEnum.None:
                Debug.LogError($"{pt} : ÆÄÃ÷ enum ¹ÌÁöÁ¤");
                break;
            case PartEnum.RightUpperArm:
                return RightArm;

            case PartEnum.LeftUpperArm:
                return LeftArm;

            case PartEnum.Legs:
                return Leg;

            case PartEnum.Head:
                return Head;

            case PartEnum.Body:
                return Body;
        }
        return null;


    }
}