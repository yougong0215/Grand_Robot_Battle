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

    public PartSO PartSO => so;

    public void SettingSO(PartSO s)
    {
        so = s;
        if (so.Sprite != null)
        {
            img.sprite = so.Sprite;
            ATK.text = $"ATK : {so.Statues.ATK}";
            DEF.text = $"DEF : {so.Statues.DEF}";
            SPEED.text = $"SPEED : {so.Statues.SPEED}";
            HP.text = $"HP : {so.Statues.HP}";
        }
    }

}
