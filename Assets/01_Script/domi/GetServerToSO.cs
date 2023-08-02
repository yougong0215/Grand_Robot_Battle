using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ServerSOPacket {
    public string code;
    public int level;
}
public class ServerPresetPacket {
    public string left;
    public string right;
    public string head;
    public string body;
    public string leg;
}

public class GetServerToSO : MonoBehaviour
{
    [SerializeField] bool isMakeScene = true;
    [SerializeField] PartSOList PartSOTable;
    Dictionary<string, PartSO> SOlist = new Dictionary<string, PartSO>();

    [Header("Left")]
    [SerializeField] ContentPartAdd Left;
    [Header("Right")]
    [SerializeField] ContentPartAdd Right;
    [Header("Body")]
    [SerializeField] ContentPartAdd Body;
    [Header("Head")]
    [SerializeField] ContentPartAdd Head;
    [Header("Leg")]
    [SerializeField] ContentPartAdd Leg;
    private void Awake() {

        for(int i =0; i < PartSOTable.sed.Count; ++i)
        {
            string a = PartSOTable.sed[i].ToString();
            a = a.Replace(" (PartSO)", "");
            SOlist.Add(a, PartSOTable.sed[i]);
            Debug.Log(a);
        }

        if (!isMakeScene)
            return;
         NetworkCore.EventListener["MakeRobot.ResultSO"] = ResultSO;
         NetworkCore.EventListener["MakeRobot.SendPreset"] = SetPreset;
    }


    public PartSO ReturnSO(string a)
    {
        return SOlist[a];
    }

    private void Start() {
        if (isMakeScene)
            NetworkCore.Send("MakeRobot.GetSO", null);
    }
    private void OnDestroy() {
        if (!isMakeScene)
            return;
        NetworkCore.EventListener.Remove("MakeRobot.ResultSO");
        NetworkCore.EventListener.Remove("MakeRobot.SendPreset");
    }

    void ResultSO(JsonData data) {
        var SO_List = JsonMapper.ToObject<Dictionary<string, ServerSOPacket>>(data.ToJson());
        print(SO_List.Count);

        foreach (var SOOOO in SO_List) 
        {
            print("-------- "+SOOOO.Key+" ---------");
            print(SOOOO.Value.code);
            print(SOOOO.Value.level);


            if(SOlist.ContainsKey($"{SOOOO.Value.code}"))
            {
                var SOS = SOlist[$"{SOOOO.Value.code}"];
                switch (SOS.PartBase)
                {
                    case PartBaseEnum.Left:
                        Left.SetSO(SOS, SOOOO.Key);
                        break;
                    case PartBaseEnum.Right:
                        Right.SetSO(SOS, SOOOO.Key);
                        break;
                    case PartBaseEnum.Head:
                        Head.SetSO(SOS, SOOOO.Key);
                        break;
                    case PartBaseEnum.Body:
                        Body.SetSO(SOS, SOOOO.Key);
                        break;
                    case PartBaseEnum.Leg:
                        Leg.SetSO(SOS, SOOOO.Key);
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
                print($"{SOOOO.Value.code}[{SOOOO.Key}] SO�� ã�� �� ����.");
            }
        }
    }
    
    void SetPreset(JsonData data) {
        if (!isMakeScene)
            return;

        var Preset = JsonMapper.ToObject<ServerPresetPacket>(data.ToJson());
        print("-------- 프리셋 -------");
        print(Preset.left);
        print(Preset.right);
        print(Preset.head);
        print(Preset.body);
        print(Preset.leg);
        
        if(Preset.left != "")
            Left.SetPart(Preset.left);
        if(Preset.body != "")
            Body.SetPart(Preset.body);
        if(Preset.right != "")
            Right.SetPart(Preset.right);
        if(Preset.leg != "")
            Leg.SetPart(Preset.leg);
        if(Preset.head != "")
            Head.SetPart(Preset.head);
                        
        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
        Head.gameObject.SetActive(false);
        Body.gameObject.SetActive(false);
        Leg.gameObject.SetActive(false);
    }
}
