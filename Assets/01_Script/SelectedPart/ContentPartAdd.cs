using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentPartAdd : MonoBehaviour
{
    RobotSettingAndSOList _robot;
    [SerializeField] public GameObject _contentObj;
    [SerializeField] public PartUIInfo _seletedObj;
    [SerializeField] List<PartSO> part;
    [SerializeField] PartBaseEnum enums;

    [SerializeField] Dictionary<string ,UnitPart> dic = new();


    public void SetSO(PartSO so, string token, bool b = false)
    {
        //part.Clear();
        part.Add(so);
        //if (_contentObj == null)
        //    _contentObj = transform.GetChild(0).Find("Content").gameObject;
        //if (_seletedObj == null)
        //    _seletedObj = transform.Find("Selected").gameObject.GetComponent<PartUIInfo>();

        //f (_contentObj.transform.childCount == 0 && _seletedObj.transform.childCount == 0)
        
            //prt = PartList.Instance.ReturnInfo(pt); ���� �ִ��� �������°�
            Debug.LogWarning("���� �ٲ���ߵ�");




             dic.Add(token ,Instantiate(UISlotManager.Instance.Prefabs, _contentObj.transform));


             dic[token].SettingSO(this, so, token);
                        //ui.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);


                    
                
            
        
        //gameObject.SetActive(false);

    }

    public void SetPart(string toekn) 
    {
        dic[toekn].SetPartClick();    
    }

}
