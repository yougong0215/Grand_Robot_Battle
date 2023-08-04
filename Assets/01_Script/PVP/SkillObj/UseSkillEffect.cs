using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class SkillEaseing
{
    public GameObject obj;
    public float nextTime = 0f;
    public bool normalize = false;
}
public class UseSkillEffect : MonoBehaviour
{
    public List<SkillEaseing> objs = new();

    [Header("Animation Time")]
    [SerializeField] float _endTime = 1f;
    float _camsTime = 0f;
    FSM _fsm;

    bool init = false;

    Coroutine co;

    public void Init(Quaternion rot)
    {
        transform.rotation = rot;

        co = StartCoroutine(camsCol());

        _camsTime = 0f;
        init = true;
    }

    IEnumerator camsCol()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].obj.TryGetComponent<ParticleSystem>(out ParticleSystem pt))
            {
                pt.Play();
            }
            if (objs[i].obj.TryGetComponent<VisualEffect>(out VisualEffect ptd))
            {
                ptd.Play();
            }

            yield return new WaitForSeconds(objs[i].nextTime);

        }

    }

    public void Hits()
    {
        if (co != null)
            StopCoroutine(co);
    }

    private void Update()
    {
        _camsTime += Time.deltaTime;
        if (_camsTime > _endTime && init)
        {
            init = false;
            if (co != null)
                StopCoroutine(co);


        }
    }
}
