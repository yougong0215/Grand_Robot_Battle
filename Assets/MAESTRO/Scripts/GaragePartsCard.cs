using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Info
{
    public string token;
    public string name;
    public Sprite partsImage;
    public float atk;
    public float def;
    public float hp;
    public float speed;
    public string info;
    public string skillInfo;
    public string rate;
    public int level;
    public int maxLevel;
}

public class GaragePartsCard : MonoBehaviour
{
    Info infoData = new Info();
    public void InfoSet(string token, string name, Sprite partsImage, float atk, float def,
                        float hp, float speed, string info, string skillInfo, string rate, int level, int maxLevel)
    {
        infoData.token = token;
        infoData.name = name;
        infoData.partsImage = partsImage;
        infoData.atk = atk;
        infoData.def = def;
        infoData.hp = hp;
        infoData.speed = speed;
        infoData.info = info;
        infoData.skillInfo = skillInfo;
        infoData.rate = rate;
        infoData.level = level;
        infoData.maxLevel = level;
    }

    public void ClickThisCard()
    {
        GarageLoadSystem garageLoadSystem = GameObject.Find("LoadSystem").GetComponent<GarageLoadSystem>();
        garageLoadSystem.DataSet(infoData);
    }
}
