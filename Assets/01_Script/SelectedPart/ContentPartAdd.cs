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



    public void SetSO(PartSO so, string token)
    {
        //part.Clear();
        part.Add(so);
        //if (_contentObj == null)
        //    _contentObj = transform.GetChild(0).Find("Content").gameObject;
        //if (_seletedObj == null)
        //    _seletedObj = transform.Find("Selected").gameObject.GetComponent<PartUIInfo>();

        //f (_contentObj.transform.childCount == 0 && _seletedObj.transform.childCount == 0)
        {
            //part = PartList.Instance.ReturnInfo(pt); ���� �ִ��� �������°�
            Debug.LogWarning("���� �ٲ���ߵ�");


                UnitPart ui;

                        ui = Instantiate(UISlotManager.Instance.Prefabs, _contentObj.transform);


                        ui.SettingSO(this, so, token);
                        //ui.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);



                    
                
            
        }
        //gameObject.SetActive(false);

    }

}
