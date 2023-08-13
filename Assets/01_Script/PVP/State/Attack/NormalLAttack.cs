using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLAttack : CommonAction
{
    bool init = false;
    protected override void Init()
    {
        init = true;
        if(TryGetComponent<UseSkillCamera>(out UseSkillCamera sk))
        {
            sk.Init(FSMMain.transform.rotation);
        }
    }

    protected override void OnEndFunc()
    {
        //DestroyObj();
    }

    protected override void OnEventFunc()
    {
    }

    protected override void OnUpdateFunc()
    {
        if(init==false)
        {
            Init();
        }
    }

}
