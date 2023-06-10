using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Info
{
    public string name;
    public Image partsImage;
    public float atk;
    public float def;
    public float hp;
    public float speed;
    public string info;
    public string skillInfo;
    public string rate;
}

public class GaragePartsCard : MonoBehaviour
{
    Info infoData = new Info();
    public void InfoSet(string name, Image partsImage, float atk, float def,
                        float hp, float speed, string info, string skillInfo, string rate)
    {
        infoData.name = name;
        infoData.partsImage = partsImage;
        infoData.atk = atk;
        infoData.def = def;
        infoData.hp = hp;
        infoData.speed = speed;
        infoData.info = info;
        infoData.skillInfo = skillInfo;
        infoData.rate = rate;
    }

    public void ClickThisCard()
    {
        GarageLoadSystem garageLoadSystem = GameObject.Find("LoadSystem").GetComponent<GarageLoadSystem>();
        garageLoadSystem.DataSet(infoData);
    }
}
