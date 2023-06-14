using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRobotSavePart : MonoBehaviour
{
    Dictionary<string,string > tokens = new();
    public void onClick()
    {
        int count = transform.childCount;

        for(int i =0; i < count; i++)
        {
            var pi = transform.GetChild(i).GetComponent<PartUIInfo>();

            if (pi.Part)
                tokens.Add(pi.Part.PartBase.ToString() ,pi.token);
        }

        NetworkCore.Send("MakeRobot.SetSetting", tokens);
    }
}
