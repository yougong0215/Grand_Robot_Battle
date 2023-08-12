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
    public int attack;
    public int shield;
    public int speed;
    public int health;
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

    void SetItems(JsonData data) {
        var items = JsonMapper.ToObject<GarageItemInfo[]>(data.ToJson());
        
        foreach (var item in items)
        {
            GameObject box = Instantiate(boxPrefab, _content);
            box.name = item.token;

            GaragePartsCard card = box.GetComponent<GaragePartsCard>();
            PartSO so = _ServerSO.ReturnSO(item.code);

            box.transform.Find("PartsImageBack").Find("PartsImage").GetComponent<Image>().sprite = so.EquipImage;
            box.transform.Find("PartsImageBack").Find("PartsImage").Find("NamePanel").Find("NameText").GetComponent<TextMeshProUGUI>().text = so.name;

            card.InfoSet(item.token, so.names, so.SkillImage, item.attack, item.shield, item.health, item.speed, so.Explain, so.SkillExplain, "empty" /* 등급은 아직 서버에 구현이 되어있지 않음 */, item.level);
        }
    }
}
