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
    [SerializeField] Image cont;
    public PartEnum PartType => _pe;
    
    [SerializeField] bool bSelected =false;

    private void Start()
    {
        _img = GetComponent<Image>();
        //Debug.Log(_pe.ToString());
        if(InitObj)
        {
            Selected();
        }
        else
        {
            _img.color = new Color(0.3f, 0.3f, 0.3f, 0.6f);
        }
        if(bSelected)
            Selected();
        
    }



    public void Click()
    {
        UISlotManager.Instance.PartSelected(_pe);
    }

    public void Selected()
    {
        _img.color = new Color(0.6f, 0.6f, 0.6f, 1);
        cont.color = new Color(1, 1, 1, 1f);
        if (cont.gameObject.TryGetComponent<PartUIInfo>(out PartUIInfo id) && id.GetIt())
        {
            cont.color = Color.black;
        }


        _child.SetActive(true);
    }

    public void NotSelected()
    {
        _img.color = new Color(0.3f, 0.3f, 0.3f, 0.6f);
        cont.color = new Color(1, 1, 1, 0.3f);

        if (cont.gameObject.TryGetComponent<PartUIInfo>(out PartUIInfo id) && id.GetIt())
        {
            cont.color = new Color(0.15f, 0.15f, 0.15f, 1);
        }

        _child.SetActive(false);
    }
}
