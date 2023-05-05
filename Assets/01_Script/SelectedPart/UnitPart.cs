using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPart : MonoBehaviour
{
    [SerializeField] PartSO so;

    public PartSO PartSO => so;

    public void SettingSO(PartSO s)
    {
        so = s;
        if (so.Sprite != null)
            GetComponent<Image>().sprite = so.Sprite;
    }
}
