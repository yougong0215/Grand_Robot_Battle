using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPartAdd : MonoBehaviour
{

    //[SerializeField] GameObject _contentObj;
    //[SerializeField] PartUIInfo _seletedObj;
    //[SerializeField] List<PartSO> part;
    //[SerializeField] PartEnum pt;

    //bool bFirst = true;


    //private void Start()
    //{
    //    //if (_contentObj == null)
    //    //    _contentObj = transform.GetChild(0).Find("Content").gameObject;
    //    //if (_seletedObj == null)
    //    //    _seletedObj = transform.Find("Selected").gameObject.GetComponent<PartUIInfo>();

    //    if (_contentObj.transform.childCount == 0 && _seletedObj.transform.childCount == 0)
    //    {
    //        part = PartList.Instance.ReturnInfo(pt);

    //        if (part != null)
    //        {
    //            UnitPart ui;

    //            for (int i = 0; i < part.Count; i++)
    //            {
    //                if (part[i].EquipPart == true)
    //                {



    //                    ui = Instantiate(UISlotManager.Instance.Prefabs, _seletedObj.transform);

    //                    ui.SettingSO(part[i]);

    //                    _seletedObj.Seleted(ui.PartSO);
    //                    ui.transform.parent = _seletedObj.transform;
    //                    ui.transform.position = _seletedObj.transform.position;
    //                    ui.GetComponent<UIDragAndDrop>()._oldParent = _contentObj.transform;
    //                    ui.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);


    //                }
    //                else
    //                {


    //                    ui = Instantiate(UISlotManager.Instance.Prefabs, _contentObj.transform);


    //                    ui.SettingSO(part[i]);
    //                    ui.GetComponent<UIDragAndDrop>()._oldParent = _contentObj.transform;
    //                    ui.GetComponent<UIDragAndDrop>().ReturnParent();
    //                    ui.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);



    //                }
    //            }
    //        }
    //    }
    //    bFirst = false;
    //    gameObject.SetActive(false);

    //}

}
