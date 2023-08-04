using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeCircleTurn : MonoBehaviour
{
    [SerializeField] float _time = 0;
    [SerializeField] float _speed = 3f;
    [SerializeField] Transform ts;

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime * _speed;
        ts.localPosition = new Vector3(Mathf.Sin(_time),Mathf.Cos(_time)) * 0.52f;
    }

    public bool IsOK()
    {
        return true;
    }
}
