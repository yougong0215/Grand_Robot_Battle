using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSelecter : MonoBehaviour
{
    private BlockMarkSpawner _bs;
    [SerializeField] private List<Transform> _maps = new List<Transform>();
    private Transform _mapParent;
    private string _mapMarkCharData = "ABCDEFGH";
    private int[] _mapMarkIntData = { 1, 2, 3, 4, 5, 6, 7, 8 };

    private void Awake()
    {
        _bs = (BlockMarkSpawner)GameObject.Find("BlockMarkSpawner").GetComponent("BlockMarkSpawner");
    }

    private void Start()
    {
        _mapParent = GameObject.Find("Chess Battlefield").transform;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
                _maps.Add(_mapParent.Find($"{_mapMarkCharData[i]}{_mapMarkIntData[j]}"));
        }
    }

    public void DetectRange()
    {
        for (int i = 0; i < _maps.Count; i++)
        {
            _bs.MarkSpawn(transform, _maps[i], false, _maps[i].name);
        }
    }
}
