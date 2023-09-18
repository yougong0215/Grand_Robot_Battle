using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : ObjectManager
{
    public static EffectManager Instance;
    [SerializeField] private List<GameObject> _fxList = new List<GameObject>();

    public override void SetInstance()
    {
        Instance = this;
    }

    public void CreateFX(Transform trans, FXType fxtype, float term)
    {
        StartCoroutine(CreateTemrm(trans, fxtype, term));
    }

    IEnumerator CreateTemrm(Transform trans, FXType fx, float term)
    {
        yield return new WaitForSeconds(term);
        GameObject makeFx = Instantiate(_fxList[(int)fx]);
        makeFx.transform.position = new Vector3(trans.position.x, 10, trans.position.z);
    }
}
