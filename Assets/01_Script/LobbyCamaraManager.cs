using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Linq;

public class LobbyCamaraManager : MonoBehaviour
{
    [SerializeField] List<LobbyCamComponent> ListCam;
    [SerializeField] GameObject canvas;

    private void Awake()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        for(int i =0; i < ListCam.Count;i++)
        {
            ListCam[i].lc = this;
        }
        yield return new WaitForSeconds(0.2f);
        CamTurning(LobbyScene.MainLobby);
        yield return new WaitForSeconds(2f);
        canvas.SetActive(false);
    }


    public void CamTurning(LobbyScene scene)
    {
        for (int i = 0; i < ListCam.Count; i++)
        {
            if (ListCam[i].CamEnum == scene)
                StartCoroutine(ListCam[i].OnCam());
            else
                ListCam[i].OffCam();
        }
    }

}
