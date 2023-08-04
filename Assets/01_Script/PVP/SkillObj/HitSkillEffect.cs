using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSkillEffect : MonoBehaviour
{
    bool init = false;
    Transform ts;
    Quaternion qt;
    public void Init(Transform trans, Quaternion quter, bool rotEnemy = false)
    {

        ts = trans;
        qt = quter;
        if(rotEnemy == true)
        {
            qt.x = 0;
            qt.z = 0;

        }

        init = true;
    }

    private void Update()
    {
        if(init) 
        {
            if(ts != null ) 
            {
                transform.position = ts.position;
                transform.rotation = qt;
            }
        }
    }
}
