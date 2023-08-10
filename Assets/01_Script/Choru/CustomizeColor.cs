using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomizeColor : MonoBehaviour
{
    public GameObject Robot;
    public Material Mat;

    [Space(10)]
    public Color color;

    private Material InstanceMat;
    private SkinnedMeshRenderer[] renderers;

    public void Setup()
    {
        InstanceMat = Instantiate(Mat);
        renderers = Robot.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var renderer in renderers)
        {
            int idx = renderer.materials.ToList().FindIndex(x => x.name.Contains(Mat.name));
            if (idx >= 0)
            {
                Material[] mats = renderer.sharedMaterials;
                mats[idx] = InstanceMat;
                renderer.sharedMaterials = mats;
            }
        }
    }

    public void ChangeColor(Color color)
    {
        InstanceMat.color = color;
    }

    void Start()
    {
        Setup();
    }

}
