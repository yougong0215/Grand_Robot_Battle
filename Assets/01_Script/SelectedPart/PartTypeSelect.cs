using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartTypeSelect : MonoBehaviour
{
    [SerializeField] Image _img;
    [SerializeField] GameObject _child;
    [SerializeField] PartEnum _pe;
    [SerializeField] bool InitObj = false;
    public PartEnum PartType => _pe;

    private void Start()
    {
        _img = GetComponent<Image>();
        Debug.Log(_pe.ToString());
        if(InitObj)
        {
            Selected();
        }
        else
        {
            _img.color = new Color(0.3f, 0.3f, 0.3f, 0.6f);
        }
    }



    public void Click()
    {
        UISlotManager.Instance.PartSelected(_pe);
    }

    public void Selected()
    {
        _img.color = new Color(0.6f, 0.6f, 0.6f, 1);

        _child.SetActive(true);
    }

    public void NotSelected()
    {
        _img.color = new Color(0.3f, 0.3f, 0.3f, 0.6f);

        _child.SetActive(false);
    }
}
