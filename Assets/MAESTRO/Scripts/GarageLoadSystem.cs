using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageLoadSystem : MonoBehaviour
{
    GarageUI _gui;
    [SerializeField] private GameObject _partsScroll;
    [SerializeField] private GameObject _enforcePage;
    private void Awake()
    {
        _gui = GameObject.Find("UI").GetComponent<GarageUI>();
    }

    private void Start()
    {
        _partsScroll.SetActive(true);
        _enforcePage.SetActive(false);
    }

    public void DataSet(Info partsInfo)
    {
        _gui.ChangeMode();
        _partsScroll.SetActive(false);
        _enforcePage.SetActive(true);
    }
}
