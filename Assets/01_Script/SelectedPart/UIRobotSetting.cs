using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRobotSetting : MonoBehaviour
{
    [Header("Left Arm")]
    [SerializeField] GameObject LeftArmBone;
    [SerializeField] GameObject LeftArmMesh;
    [SerializeField] GameObject LeftEquip;
    [Header("Right Arm")]
    [SerializeField] GameObject RightArmBone;
    [SerializeField] GameObject RightArmMesh;
    [SerializeField] GameObject RightEquip;

    [Header("Head")]
    [SerializeField] GameObject HeadBone;
    [SerializeField] GameObject HeadEquip;

    [Header("Body")]
    [SerializeField] GameObject BodyPos;
    [SerializeField] GameObject BodyEquip;

    [Header("Leg")]
    [SerializeField] GameObject LegPos;
    [SerializeField] GameObject LegMesh;
    [SerializeField] GameObject LegEquip;

    public void EquipPart(PartEnum enums, PartSO so = null, bool ReplaceMesh = false)
    {
        switch (enums)
        {
            case PartEnum.None:
                break;
            case PartEnum.RightArm:
                
                if(ReplaceMesh)
                {
                    RightArmMesh.SetActive(false);
                }

                if(so == null)
                {
                    RightArmMesh.SetActive(true);



                    if (RightEquip)
                    {
                        Destroy(RightEquip);
                    }
                    RightEquip = null;

                }
                else
                {
                    RightEquip = Instantiate(so.PartAsset, RightArmBone.transform);
                }


                break;
            case PartEnum.LeftArm:

                if (ReplaceMesh)
                {
                    LeftArmMesh.SetActive(false);
                }

                if (so == null)
                {
                    LeftArmMesh.SetActive(true);


                    if (LeftEquip)
                    {
                        Destroy(LeftEquip);
                    }
                    LeftEquip = null;
                }
                else
                {
                    LeftEquip = Instantiate(so.PartAsset, LeftArmBone.transform);
                }

                break;

            case PartEnum.Legs:

                if (ReplaceMesh)
                {
                    LegMesh.SetActive(false);
                }

                if (so == null)
                {
                    LegMesh.SetActive(true);

                    if(LegEquip)
                    {
                        Destroy(LegEquip);
                    }
                    LegEquip = null;
                }
                else
                {
                    LegEquip = Instantiate(so.PartAsset, LegPos.transform);
                }
                break;


            case PartEnum.Head:
                if (so == null)
                {
                    if (HeadEquip)
                    {
                        Destroy(HeadEquip);
                    }
                    HeadEquip = null;
                }
                else
                {
                    HeadEquip = Instantiate(so.PartAsset, HeadBone.transform);
                }


                break;
            case PartEnum.Body:
                if (so == null)
                {
                    if (BodyEquip)
                    {
                        Destroy(BodyEquip);
                    }
                    BodyEquip = null;
                }
                else
                {
                    BodyEquip = Instantiate(so.PartAsset, BodyPos.transform);
                }

                break;
        }

    }
}