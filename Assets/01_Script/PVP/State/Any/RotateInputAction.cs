using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInputAction : AnyCommonAction
{
    Vector3 vec;
    [SerializeField] RotationRect _rect;

    
    RotationRect GetRect()
    {
        if(_rect == null)
        {
            _rect = FindManager.Instance.FindObject("PVPUI").GetComponent<RotationRect>();
        }
        return _rect;
    }


    Quaternion QuaternionAdd(Quaternion a, Quaternion b)
    {
        Quaternion h = new Quaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        return h;
    }
    protected override void FixedState()
    {
        vec += new Vector3(0, GetRect().GetInputDirection().x, 0);

        FSMMain.transform.rotation = QuaternionAdd(transform.rotation, Quaternion.Euler(vec));
    }

    protected override void UpdateState()
    {
    }
}
