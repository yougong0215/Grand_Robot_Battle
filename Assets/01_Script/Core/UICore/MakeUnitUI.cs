using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeUnitUI : MonoBehaviour
{

    //[SerializeField] GameObject Prefabs;
    [SerializeField] GameObject obj;
    Sprite spi;

    GameObject objt;

    private void Awake()
    {
        GetComponent<Image>().sprite = spi;
    }

    public void Make()
    {
        if (!Input.GetMouseButton(0))
        {

            objt = Instantiate(obj, transform);
            objt.transform.position = transform.position;
            UISlotManager.Instance.UnitUI = objt.GetComponent<UIDragAndDrop>();

        }
        
    }

    public void DestroyUI()
    {
        if (!Input.GetMouseButton(0))
        {
            DestroyImmediate(objt);
            UISlotManager.Instance.UnitUI = null;
        }
    }
}
