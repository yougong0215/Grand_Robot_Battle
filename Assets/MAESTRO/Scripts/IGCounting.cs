using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IGCounting : MonoBehaviour
{
    int setCount = 1;
    [SerializeField] private TextMeshProUGUI _countTxt;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void AddCounting()
    {
        setCount++;
    }

    public void DegreeCounting()
    {
        if(setCount - 1 < 1 != true)
        {
            setCount--;
        }
    }
}
