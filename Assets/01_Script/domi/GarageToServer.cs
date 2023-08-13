using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
using TMPro;

class GarageItemInfo {
    public string token;
    public string code;
    public int level;
    public int maxLevel;
    public int attack;
    public int shield;
    public int speed;
    public int health;
    public int grade;
}

public class GarageToServer : MonoBehaviour
{
    [SerializeField] Transform _content;
    [SerializeField] GameObject boxPrefab;
    [SerializeField] GetServerToSO _ServerSO;

    private void Awake() {
        NetworkCore.EventListener["garage.resultItems"] = SetItems;
    }
    private void OnDestroy() {
        NetworkCore.EventListener.Remove("garage.resultItems");
    }

    private void Start() {
        NetworkCore.Send("garage.getItems", null);
    }

    enum Grade {
        노멀,
        유니크,
        마스터피스
    }

    void SetItems(JsonData data) {
        var items = JsonMapper.ToObject<GarageItemInfo[]>(data.ToJson());
        
        foreach (var item in items)
        {
            GameObject box = Instantiate(boxPrefab, _content);
            box.name = item.token;

            GaragePartsCard card = box.GetComponent<GaragePartsCard>();
            PartSO so = _ServerSO.ReturnSO(item.code);

            box.transform.Find("PartsImageBack").Find("PartsImage").GetComponent<Image>().sprite = so.Sprite;
            box.transform.Find("PartsImageBack").Find("PartsImage").Find("NamePanel").Find("NameText").GetComponent<TextMeshProUGUI>().text = so.name;

            Color _color;
            switch (item.grade)
            {
                case 0:
                    _color = Color.green;
                    break;
                case 1:
                    _color = Color.magenta;
                    break;
                case 2:
                    _color = Color.red;
                    break;
                default:
                    _color = Color.cyan;
                    break;
            }
            
            box.transform.Find("PartsImageBack").Find("PartsImage").Find("NamePanel").Find("PartsTypeImage").GetComponent<Image>().color = _color;

            card.InfoSet(item.token, so.names, so.SkillImage, item.attack, item.shield, item.health, item.speed, so.Explain, so.SkillExplain, ((Grade)item.grade).ToString(), item.level, item.maxLevel);
        }
    }

    
}
