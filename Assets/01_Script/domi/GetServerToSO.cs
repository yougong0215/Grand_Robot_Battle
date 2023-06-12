using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ServerSOPacket {
    public string code;
    public int level;
}

public class GetServerToSO : MonoBehaviour
{
    [SerializeField] PartSOList PartSOTable;
    Dictionary<string, PartSO> SOlist = new Dictionary<string, PartSO>();

    [Header("Left")]
    [SerializeField] ContentPartAdd Left;
    [SerializeField] List<PartSO> _left = new List<PartSO>();
    [Header("Right")]
    [SerializeField] ContentPartAdd Right;
    [SerializeField] List<PartSO> _right = new List<PartSO>();
    [Header("Body")]
    [SerializeField] ContentPartAdd Body;
    [SerializeField] List<PartSO> _body = new List<PartSO>();
    [Header("Head")]
    [SerializeField] ContentPartAdd Head;
    [SerializeField] List<PartSO> _head = new List<PartSO>();
    [Header("Leg")]
    [SerializeField] ContentPartAdd Leg;
    [SerializeField] List<PartSO> _legs = new List<PartSO>();
    private void Awake() {

        for(int i =0; i < PartSOTable.sed.Count; ++i)
        {
            SOlist.Add(PartSOTable.sed[i].ToString(), PartSOTable.sed[i]);
        }
        NetworkCore.EventListener["MakeRobot.ResultSO"] = ResultSO;
    }

    private void Start() {
        NetworkCore.Send("MakeRobot.GetSO", null);
    }
    private void OnDestroy() {
        NetworkCore.EventListener.Remove("MakeRobot.ResultSO");
    }

    void ResultSO(JsonData data) {
        var SO_List = JsonMapper.ToObject<Dictionary<string, ServerSOPacket>>(data.ToJson());
        print(SO_List.Count);

        foreach (var SOOOO in SO_List) {
            print("-------- "+SOOOO.Key+" ---------");
            print(SOOOO.Value.code);
            print(SOOOO.Value.level);

            if(SOlist.ContainsKey($"{SOOOO}"))
            {
                switch (SOlist[$"{SOOOO}"].PartBase)
                {
                    case PartBaseEnum.Left:
                        _left.Add(SOlist[$"{SOOOO}"]);
                        break;
                    case PartBaseEnum.Right:
                        _right.Add(SOlist[$"{SOOOO}"]);
                        break;
                    case PartBaseEnum.Head:
                        _head.Add(SOlist[$"{SOOOO}"]);
                        break;
                    case PartBaseEnum.Body:
                        _body.Add(SOlist[$"{SOOOO}"]);
                        break;
                    case PartBaseEnum.Leg:
                        _legs.Add(SOlist[$"{SOOOO}"]);
                        break;
                    case PartBaseEnum.Error:
                        print("error");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                print(SOOOO.Key + "ÀÌ ¾øÀ½");
            }
        }

        Left.SetSO(_left);
        Right.SetSO(_right);
        Body.SetSO(_body);
        Head.SetSO(_head);
        Leg.SetSO(_legs);
    }
}
