using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPSceneTouchManager : MonoBehaviour
{
    Touch[] tch = new Touch[3];
    bool isJoyStick = false;
    JoyStick joy;
    RotationRect rec;
    void Start()
    {
        joy = GetComponent<JoyStick>();
        rec = GetComponent<RotationRect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log(Input.touchCount);
            for (int i = 0; i < Input.touchCount; i++)
            {
                tch[i] = Input.GetTouch(i);
                if (joy.TouchJoyStick(tch[i]) && isJoyStick == false)
                {
                    isJoyStick = false;
                    joy.UpdatePointer(tch[i].fingerId);
                }
                else
                {

                    rec.UpdatePointer(tch[i].fingerId);
                }
                

            }

        }

        isJoyStick = false;
    }
}
