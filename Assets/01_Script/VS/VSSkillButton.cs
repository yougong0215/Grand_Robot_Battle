using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSSkillButton : MonoBehaviour
{
    public VSPlayer CurrentPlayer;
    [SerializeField] int number = 0;

    public void SetSkillUI(VSPlayer vs, Sprite obj = null)
    {
        CurrentPlayer = vs;
        if(obj != null)
        {
            GetComponent<Image>().sprite = obj;
        }

    }

    public void ReturnNumber()
    {
        CurrentPlayer.SetSkillNum(number);
        Debug.Log($"Select : {number}");
        CurrentPlayer.OpenSelectEnemy();
        CurrentPlayer = null;
    }

    public void ReturnEnemyNumber()
    {
        CurrentPlayer.SetEnemyNum(number);
    }
}
