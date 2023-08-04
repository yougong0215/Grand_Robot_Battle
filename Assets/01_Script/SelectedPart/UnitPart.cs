using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitPart : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI ATK;
    [SerializeField] TextMeshProUGUI DEF;
    [SerializeField] TextMeshProUGUI HP;
    [SerializeField] TextMeshProUGUI SPEED;
    [SerializeField] Image img;
    [Header("SO")]

    [SerializeField] PartSO so;
    [Header("Equip")]
    [SerializeField] Image eq;
    [SerializeField] Image dq;

    public PartSO PartSO => so;

    string token;

    ContentPartAdd c;

    public void SetPartClick(bool f = true)
    {
        if(transform.parent.name == "Content")
        {

            if (f)
                c._seletedObj.Seleted(c, so, token);
            transform.SetParent(c._seletedObj.transform);
            dq.gameObject.SetActive(false);
            eq.gameObject.SetActive(true);
            
            transform.GetComponent<RectTransform>().position = c._seletedObj.GetComponent<RectTransform>().position;
            eq.GetComponent<RectTransform>().sizeDelta = c._seletedObj.GetComponent<RectTransform>().sizeDelta;
            
        }
        else
        {
            if (f)
                c._seletedObj.Seleted(c, null);
            transform.SetParent( c._contentObj.transform);

            dq.gameObject.SetActive(true);
            eq.gameObject.SetActive(false);

        }
    }

    public void SettingSO(ContentPartAdd c, PartSO s, string token)
    {
        so = s;
        this.token = token;
        this.c = c;

        if (so.Sprite != null)
        {
            img.sprite = so.Sprite;
            ATK.text = $"ATK : {so.Statues.ATK}";
            DEF.text = $"DEF : {so.Statues.DEF}";
            SPEED.text = $"SPEED : {so.Statues.SPEED}";
            HP.text = $"HP : {so.Statues.HP}";
        }

        eq.sprite = s.EquipImage;

        if(s.EquipPart == true)
        {
            dq.gameObject.SetActive(false);
            eq.gameObject.SetActive(true);
            eq.GetComponent<RectTransform>().sizeDelta = c._seletedObj.GetComponent<RectTransform>().sizeDelta;
        }
        else
        {
            dq.gameObject.SetActive(true);
            eq.gameObject.SetActive(false);
        }

    }

}
