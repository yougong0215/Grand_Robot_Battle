using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSSkillButton : MonoBehaviour
{
    public VSPlayer CurrentPlayer;
    [SerializeField] int number = 0;

    public void SetSkillUI(GameObject obj = null)
    {
        if(obj != null)
        {
            Instantiate(obj, transform);
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
