using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<ObjectManager> _managerList = new List<ObjectManager>();

    private void Awake()
    {
        for(int i = 0; i < _managerList.Count; i++)
        {
            _managerList[i].SetInstance();
        }
    }
}
