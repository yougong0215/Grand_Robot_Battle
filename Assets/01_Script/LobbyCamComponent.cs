using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LobbyScene
{
    MainLobby,
    MakeRobotScene,
}

public class LobbyCamComponent : MonoBehaviour
{
    [SerializeField] LobbyScene _e;
    public LobbyScene CamEnum => _e;

    public LobbyCamaraManager lc;

    CinemachineVirtualCamera vir;

    private void Awake()
    {
        vir = GetComponent<CinemachineVirtualCamera>();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void Turning()
    {
        lc.CamTurning(_e);
    }


    public void OffCam()
    {
        vir.Priority = -10;
        for (int i =0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    public IEnumerator OnCam()
    {
        vir.Priority = 10;
        yield return new WaitForSeconds(1.9f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
