using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PartSOList")]
public class PartSOList : ScriptableObject
{
    [SerializeField] public List<PartSO> sed;
}
