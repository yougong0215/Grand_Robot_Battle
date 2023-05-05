using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlot : MonoBehaviour
{
    public UIDragAndDrop obj;


    public void Triggerd()
    {
        UISlotManager.Instance.SoltUI = this;
    }

    public void Out()
    {
        UISlotManager.Instance.SoltUI = null;
    }

}
