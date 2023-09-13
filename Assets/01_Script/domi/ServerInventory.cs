using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

struct PuzzelItem {
    public string id;
    public int amount;
}

public class ServerInventory : MonoBehaviour
{
    InventoryUI _inven;
    GetServerToSO _SO;
    private void Awake() {
        _inven = GetComponent<InventoryUI>();
        _SO = GetComponent<GetServerToSO>();
        NetworkCore.EventListener["puzzel.resultList"] = ResultPuzzels;
        NetworkCore.EventListener["puzzel.errorwindow"] = ErrorWindow;
    }

    private void Start() {
        NetworkCore.Send("puzzel.getList", null);
    }

    private void OnDestroy() {
        NetworkCore.EventListener.Remove("puzzel.resultList");
        NetworkCore.EventListener.Remove("puzzel.errorwindow");
    }

    void ResultPuzzels(JsonData data) {
        var puzzels = JsonMapper.ToObject<PuzzelItem[]>(data.ToJson());

        _inven.ClearList();
        foreach (var puzzel in puzzels) {
            _inven.AddPuzzleItem(puzzel.id, puzzel.amount);
        }
    }

    void ErrorWindow(JsonData data) {
        var domi = ((string)data).Split(":");
        if (domi[0] == "domi.eventsuccess") {
            var SO_data = _SO.ReturnSO(domi[1]);
            if (SO_data != null) {
                _inven.ActiveErrorPanel(true, $"{SO_data.names} {((RatingType)int.Parse(domi[2])).ToString()} 등급으로 교환 하였습니다.");
                NetworkCore.Send("puzzel.getList", null);
                return;
            }
        }

        _inven.ActiveErrorPanel(true, (string)data);
    }
}
