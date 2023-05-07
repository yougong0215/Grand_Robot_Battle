using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSSkillButton : MonoBehaviour
{
    public VSPlayer CurrentPlayer;

    public void SetSkillUI(GameObject obj = null)
    {
        if(obj != null)
        {
            Instantiate(obj, transform);
        }

    }
}
