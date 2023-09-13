using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Unity.VisualScripting;

class PVP_PlayerInfo {
    public bool my;
    public int health;
    public int attack;
    public int shield;
    public int speed;
    public string left;
    public string right;
    public string head;
    public string body;
    public string leg;
}

public class domiPVPServer : MonoBehaviour
{
    [SerializeField] GameObject MyRobot;
    [SerializeField] GameObject EnemyRobot;
    GetServerToSO SO_Server;
    PVPUI _pvpUI;
    
    private void Awake() {

 
        
        _pvpUI = FindAnyObjectByType<PVPUI>();
        SO_Server = GetComponent<GetServerToSO>();
        NetworkCore.EventListener["ingame.playerInit"] = playerInit;
        NetworkCore.EventListener["ingame.AIinit"] = AI_Init;
    }
    private void OnDestroy() {
        NetworkCore.EventListener.Remove("ingame.playerInit");
        NetworkCore.EventListener.Remove("ingame.AIinit");
    }

    void playerInit(JsonData data) {
        float playerMaxHP = 0;
        float not_playerMaxHP = 0;
        foreach (JsonData player in data)
        {
            ServerPVPRobotInput serverInput;
            if ((bool)player["my"])
                serverInput = MyRobot.AddComponent<ServerPVPRobotInput>();
            else
                serverInput = EnemyRobot.AddComponent<ServerPVPRobotInput>();

            // serverInput.Name = (string)player["name"];
            print(player["my"]);
            print(player["name"]);
            _pvpUI.SetNameText((bool)player["my"], (string)player["name"]);

            if (player["left"] != null)
                serverInput.Left = SO_Server.ReturnSO((string)player["left"]);
            if (player["right"] != null)
                serverInput.Right = SO_Server.ReturnSO((string)player["right"]);
            if (player["head"] != null)
                serverInput.Head = SO_Server.ReturnSO((string)player["head"]);
            if (player["body"] != null)
                serverInput.Body = SO_Server.ReturnSO((string)player["body"]);
            if (player["leg"] != null)
                serverInput.Leg = SO_Server.ReturnSO((string)player["leg"]);

            serverInput.stat = new();
            serverInput.stat.HP = (int)player["health"];
            serverInput.stat.SPEED = (int)player["speed"];
            serverInput.stat.ATK = (int)player["attack"];
            serverInput.stat.DEF = (int)player["shield"];

            // 최대 체력 설정
            if ((bool)player["my"])
                playerMaxHP = (int)player["health"];
            else
                not_playerMaxHP = (int)player["health"];

            if ((bool)player["my"]) // 스킬 버튼 설정
                _pvpUI.SetSkillButton(new PartSO[] {
                    serverInput.Left, serverInput.Right, serverInput.Head, serverInput.Body, serverInput.Leg
                }, new int[] {
                    (int)player["cools"]["left"], (int)player["cools"]["right"], (int)player["cools"]["head"], (int)player["cools"]["body"], (int)player["cools"]["leg"]
                });
            
            StartCoroutine(serverInput.FindAndSet());
        }

        _pvpUI.SetMaxHP(playerMaxHP, not_playerMaxHP);
    }

    void AI_Init(JsonData data) {
        var serverInput = MyRobot.AddComponent<ServerPVPRobotInput>();

        int myMaxHealth = 0;

        try {
            if (data["left"] != null) {
                serverInput.Left = SO_Server.ReturnSO((string)data["left"]["id"]);
                myMaxHealth += (int)data["left"]["health"];
            }
        } catch {};
        try {
            if (data["right"] != null) {
                serverInput.Right = SO_Server.ReturnSO((string)data["right"]["id"]);
                myMaxHealth += (int)data["right"]["health"];
            }
        } catch {};
        try {
            if (data["head"] != null) {
                serverInput.Head = SO_Server.ReturnSO((string)data["head"]["id"]);
                myMaxHealth += (int)data["head"]["health"];
            }
        } catch {};
        try {
            if (data["body"] != null) {
                serverInput.Body = SO_Server.ReturnSO((string)data["body"]["id"]);
                myMaxHealth += (int)data["body"]["health"];
            }
        } catch {};
        try {
            if (data["leg"] != null) {
                serverInput.Leg = SO_Server.ReturnSO((string)data["leg"]["id"]);
                myMaxHealth += (int)data["leg"]["health"];
            }
        } catch {};

        StartCoroutine(serverInput.FindAndSet());

        print(data["name"]); // 이름
        print(myMaxHealth); // 최대 체력

        // 이어서...
    }
}
