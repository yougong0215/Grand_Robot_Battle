using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

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

    private void Awake() {
        SO_Server = GetComponent<GetServerToSO>();
        NetworkCore.EventListener["ingame.playerInit"] = playerInit;
    }
    private void OnDestroy() {
        NetworkCore.EventListener.Remove("ingame.playerInit");
    }

    void playerInit(JsonData data) {
        foreach (JsonData player in data)
        {
            ServerPVPRobotInput serverInput;
            if ((bool)player["my"])
                serverInput = MyRobot.AddComponent<ServerPVPRobotInput>();
            else
                serverInput = EnemyRobot.AddComponent<ServerPVPRobotInput>();

            serverInput.Name = (string)player["name"];

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

            StartCoroutine(serverInput.FindAndSet());
        }
    }
}
