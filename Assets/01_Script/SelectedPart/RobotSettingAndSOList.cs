using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class RobotSettingAndSOList : MonoBehaviour
{
    Dictionary<PartBaseEnum, PartSO> partsDic = new();
    AnimationBind _bd;

    public AnimationBind AnimBind => _bd;



    #region public code
    [Header("Left Upper Arm")]
    [SerializeField] GameObject A_L_UBone;
    [SerializeField] SkinnedMeshRenderer A_L_UMesh;
    [SerializeField] GameObject A_L_UEquip;
    [SerializeField] PartSO A_L_USO;

    [Header("Left Middle Arm")]
    [SerializeField] GameObject A_L_MBone;
    [SerializeField] SkinnedMeshRenderer A_L_MMesh;
    [SerializeField] GameObject A_L_MEquip;
    [SerializeField] PartSO A_L_MSO;

    [Header("Left Lower Arm")]
    [SerializeField] GameObject A_L_LBone;
    [SerializeField] SkinnedMeshRenderer A_L_LMesh;
    [SerializeField] GameObject A_L_LEquip;
    [SerializeField] PartSO A_L_LSO;

    [Header("Right Upper Arm")]
    [SerializeField] GameObject A_R_UBone;
    [SerializeField] SkinnedMeshRenderer A_R_UMesh;
    [SerializeField] GameObject A_R_UEquip;
    [SerializeField] PartSO A_R_USO;

    [Header("Right Middle Arm")]
    [SerializeField] GameObject A_R_MBone;
    [SerializeField] SkinnedMeshRenderer A_R_MMesh;
    [SerializeField] GameObject A_R_MEquip;
    [SerializeField] PartSO A_R_MSO;

    [Header("Right Lower Arm")]
    [SerializeField] GameObject A_R_LBone;
    [SerializeField] SkinnedMeshRenderer A_R_LMesh;
    [SerializeField] GameObject A_R_LEquip;
    [SerializeField] PartSO A_R_LSO;

    [Header("Left Upper Leg")]
    [SerializeField] GameObject L_L_UBone;
    [SerializeField] SkinnedMeshRenderer L_L_UMesh;
    [SerializeField] GameObject L_L_UEquip;
    [SerializeField] PartSO L_L_USO;

    [Header("Left Middle Leg")]
    [SerializeField] GameObject L_L_MBone;
    [SerializeField] SkinnedMeshRenderer L_L_MMesh;
    [SerializeField] GameObject L_L_MEquip;
    [SerializeField] PartSO L_L_MSO;

    [Header("Left Lower Leg")]
    [SerializeField] GameObject L_L_LBone;
    [SerializeField] SkinnedMeshRenderer L_L_LMesh;
    [SerializeField] GameObject L_L_LEquip;
    [SerializeField] PartSO L_L_LSO;


    [Header("Right Upper Leg")]
    [SerializeField] GameObject L_R_UBone;
    [SerializeField] SkinnedMeshRenderer L_R_UMesh;
    [SerializeField] GameObject L_R_UEquip;
    [SerializeField] PartSO L_R_USO;

    [Header("Right Middle Leg")]
    [SerializeField] GameObject L_R_MBone;
    [SerializeField] SkinnedMeshRenderer L_R_MMesh;
    [SerializeField] GameObject L_R_MEquip;
    [SerializeField] PartSO L_R_MSO;

    [Header("Right Lower Leg")]
    [SerializeField] GameObject L_R_LBone;
    [SerializeField]SkinnedMeshRenderer L_R_LMesh;
    [SerializeField] GameObject L_R_LEquip;
    [SerializeField] PartSO L_R_LSO;




    [Header("Head")]
    [SerializeField] GameObject HeadBone;
    [SerializeField] SkinnedMeshRenderer HeadMesh;
    [SerializeField] GameObject HeadEquip;
    [SerializeField] PartSO HeadSO;

    [Header("Body")]
    [SerializeField] GameObject B_UBone;
    [SerializeField] SkinnedMeshRenderer B_UMesh;
    [SerializeField] GameObject B_UEquip;
    
    [SerializeField] PartSO B_UBodySO;
    #endregion


    public Stat _statues;
    public float MaxHP;

    public void SetStatues(Stat st)
    {
       _statues= st;
        MaxHP = _statues.HP;
    }


    private IEnumerator Start()
    {
        yield return StartCoroutine(gameObject.AddComponent<ServerPVPRobotInput>().FindAndSet());

        // 서버 로딩 완료 보내주기
    }
    private void Awake()
    {
        _bd = GetComponent<AnimationBind>();
    }

    //폐기
    /*
    private void Init()
    {

            if (A_L_USO)
            {
                EquipPart(PartEnum.ALU, A_L_USO);
            }
            if (A_L_MSO)
            {
                EquipPart(PartEnum.ALM, A_L_MSO);
            }
            if (A_L_LSO)
            {
                EquipPart(PartEnum.ALL, A_L_LSO);
            }

            if (A_R_USO)
            {
                EquipPart(PartEnum.ARU, A_R_USO);
            }
            if (A_R_MSO)
            {
                EquipPart(PartEnum.ARM, A_R_MSO);
            }
            if (A_R_LSO)
            {
                EquipPart(PartEnum.ARL, A_R_LSO);
            }

            if (L_L_USO)
            {
                EquipPart(PartEnum.LLU, L_L_USO);
            }
            if (L_L_MSO)
            {
                EquipPart(PartEnum.LLM, L_L_MSO);
            }
            if (L_L_LSO)
            {
                EquipPart(PartEnum.LLL, L_L_LSO);
            }

            if (L_R_USO)
            {
                EquipPart(PartEnum.LRU, L_R_USO);
            }
            if (L_R_MSO)
            {
                EquipPart(PartEnum.LRM, L_R_MSO);
            }
            if (L_R_LSO)
            {
                EquipPart(PartEnum.LRL, L_R_LSO);
            }

            if (B_UBodySO)
            {
                EquipPart(PartEnum.UpperBody, B_UBodySO);
            }
            //if (B_MBodySO)
            //{
            //    EquipPart(PartEnum.MiddleBody, B_MBodySO);
            //}
            //if (B_LBodySO)
            //{
            //    EquipPart(PartEnum.LowerBody, B_LBodySO);
            //}

            if (HeadSO)
            {
                EquipPart(PartEnum.Head, HeadSO);
            }

            //if (Body)
            //{
            //    EquipPart(PartEnum., Body);
            //}
        

    }
    */


    public void DeEquip(PartSO so, PartEnum enums)
    {

        switch (enums)
        {
            case PartEnum.None:
                break;
            case PartEnum.ALU:
                Setting(so, A_L_UBone, ref A_L_UEquip, ref A_L_USO, enums, A_L_UMesh, true);
                break;
            case PartEnum.ALM:
                Setting(so, A_L_MBone, ref A_L_MEquip, ref A_L_MSO, enums, A_L_MMesh, true);
                break;
            case PartEnum.ALL:
                Setting(so, A_L_LBone, ref A_L_LEquip, ref A_L_LSO, enums, A_L_LMesh, true);
                break;
            case PartEnum.ARU:
                Setting(so, A_R_UBone, ref A_R_UEquip, ref A_R_USO, enums, A_R_UMesh, true);
                break;
            case PartEnum.ARM:
                Setting(so, A_R_MBone, ref A_R_MEquip, ref A_R_MSO, enums, A_R_MMesh, true);
                break;
            case PartEnum.ARL:
                Setting(so, A_R_LBone, ref A_R_LEquip, ref A_R_LSO, enums, A_R_LMesh, true);
                break;
            case PartEnum.LLU:
                Setting(so, L_L_UBone, ref L_L_LEquip, ref L_L_USO, enums, L_L_UMesh, true);
                break;
            case PartEnum.LLM:
                Setting(so, L_L_MBone, ref L_L_MEquip, ref L_L_MSO, enums, L_L_MMesh, true);
                break;
            case PartEnum.LLL:
                Setting(so, L_L_LBone, ref L_L_LEquip, ref L_L_LSO, enums, L_L_LMesh, true);
                break;
            case PartEnum.LRU:
                Setting(so, L_R_UBone, ref L_R_UEquip, ref L_R_USO, enums, L_R_UMesh, true);
                break;
            case PartEnum.LRM:
                Setting(so, L_R_MBone, ref L_R_MEquip, ref L_R_MSO, enums, L_R_MMesh, true);
                break;
            case PartEnum.LRL:
                Setting(so, L_R_LBone, ref L_R_LEquip, ref L_R_LSO, enums, L_R_LMesh, true);
                break;
            case PartEnum.Head:
                Setting(so, HeadBone, ref HeadEquip, ref HeadSO, enums, HeadMesh, true);
                break;
            case PartEnum.UpperBody:
                Setting(so, B_UBone, ref B_UEquip, ref B_UBodySO, enums, B_UMesh, true);
                break;
                //case PartEnum.MiddleBody:
                //    Setting(so, B_MBone, ref B_MEquip, ref B_MBodySO, so._part[i].enums, null ,true);
                //    break;
                //case PartEnum.LowerBody:
                //    Setting(so, B_LBone, ref B_LEquip, ref B_LBodySO, so._part[i].enums, null, true);
                //    break;
        }

    }




    void Setting(PartSO input, GameObject bone, ref GameObject Equip, ref PartSO MYSO, PartEnum enums, SkinnedMeshRenderer mesh = null, bool Deq = false)
    {
        if (Deq == false)
        {
            if (input == null)

            {
                if (Equip != null)
                {
                    Destroy(Equip);
                    Equip = null;
                }

                if (mesh != null)
                {
                    mesh.enabled = true;

                }

                //if (MYSO != null && t==0)
                //{
                //    _statues.HP -= MYSO.Statues.HP;
                //    _statues.ATK -= MYSO.Statues.ATK;
                //    _statues.DEF -= MYSO.Statues.DEF;
                //    _statues.SPEED -= MYSO.Statues.SPEED;
                //}

                MYSO = null;
            }
            else
            {
                if (input.ReplaceMesh)
                {
                    if (mesh != null)
                        mesh.enabled = false;
                }

                if (Equip != null)
                {
                    Destroy(Equip);
                    Equip = null;
                }

                //if (MYSO != null && t == 0)
                //{
                //    _statues.HP -= MYSO.Statues.HP;
                //    _statues.ATK -= MYSO.Statues.ATK;
                //    _statues.DEF -= MYSO.Statues.DEF;
                //    _statues.SPEED -= MYSO.Statues.SPEED;
                //}

                MYSO = input;

                //if (input != null && t == 0)
                //{
                //    _statues.HP += MYSO.Statues.HP;
                //    _statues.ATK += MYSO.Statues.ATK;
                //    _statues.DEF += MYSO.Statues.DEF;
                //    _statues.SPEED += MYSO.Statues.SPEED;
                //}

                GameObject objed = null;
                for (int i = 0; i < input._part.Count; i++)
                {
                    if (input._part[i].enums == enums)
                    {
                        objed = input._part[i].part;
                        break;
                    }
                }

                if (objed != null)
                    Equip = Instantiate(objed, bone.transform);
                else
                    Debug.LogError("파츠 없음");
            }
        }
        else
        {
            if (mesh != null)
            {
                mesh.enabled = true;
            }
            if (Equip != null)
            {
                Destroy(Equip);
                Equip = null;
            }
            //if (MYSO != null && t == 0)
            //{
            //    _statues.HP -= MYSO.Statues.HP;
            //    _statues.ATK -= MYSO.Statues.ATK;
            //    _statues.DEF -= MYSO.Statues.DEF;
            //    _statues.SPEED -= MYSO.Statues.SPEED;
            //}

            MYSO = null;
        }
        //t++;

    }


    public bool SetingRealPart(PartSO p = null)
    {
        
        if (p != null)
        {
            partsDic[p.PartBase] = p;



            for (int i = 0; i < partsDic[p.PartBase]._part.Count; i++)
            {
                EquipPart(partsDic[p.PartBase]._part[i].enums, partsDic[p.PartBase]);
            }
        }

        return true;

    }

    public PartSO ReturnParts(PartBaseEnum e)
    {
        try
        {

            return partsDic[e];
        }
        catch
        {

            return null;
        }
    }


    public void EquipPart(PartEnum enums, PartSO so = null)
    {
        
        switch (enums)
        {
            case PartEnum.None:
                break;
            case PartEnum.ALU:
                Setting(so, A_L_UBone, ref A_L_UEquip, ref A_L_USO, enums, A_L_UMesh);
                break;
            case PartEnum.ALM:
                Setting(so, A_L_MBone, ref A_L_MEquip, ref A_L_MSO, enums, A_L_MMesh);
                break;
            case PartEnum.ALL:
                Setting(so, A_L_LBone, ref A_L_LEquip, ref A_L_LSO, enums, A_L_LMesh);
                break;
            case PartEnum.ARU:
                Setting(so, A_R_UBone, ref A_R_UEquip, ref A_R_USO, enums, A_R_UMesh);
                break;
            case PartEnum.ARM:
                Setting(so, A_R_MBone, ref A_R_MEquip, ref A_R_MSO, enums, A_R_MMesh);
                break;
            case PartEnum.ARL:
                Setting(so, A_R_LBone, ref A_R_LEquip, ref A_R_LSO, enums, A_R_LMesh);
                break;
            case PartEnum.LLU:
                Setting(so, L_L_UBone, ref L_L_LEquip, ref L_L_USO, enums, L_L_UMesh);
                break;
            case PartEnum.LLM:
                Setting(so, L_L_MBone, ref L_L_MEquip, ref L_L_MSO, enums, L_L_MMesh);
                break;
            case PartEnum.LLL:
                Setting(so, L_L_LBone, ref L_L_LEquip, ref L_L_LSO, enums, L_L_LMesh);
                break;
            case PartEnum.LRU:
                Setting(so, L_R_UBone, ref L_R_UEquip, ref L_R_USO, enums, L_R_UMesh);
                break;
            case PartEnum.LRM:
                Setting(so, L_R_MBone, ref L_R_MEquip, ref L_R_MSO, enums, L_R_MMesh);
                break;
            case PartEnum.LRL:
                Setting(so, L_R_LBone, ref L_R_LEquip, ref L_R_LSO, enums, L_R_LMesh);
                break;
            case PartEnum.Head:
                Setting(so, HeadBone, ref HeadEquip, ref HeadSO, enums, HeadMesh);
                break;
            case PartEnum.UpperBody:
                Setting(so, B_UBone, ref B_UEquip, ref B_UBodySO, enums, B_UMesh);
                break;
                //case PartEnum.MiddleBody:
                //    Setting(so, B_MBone, ref B_MEquip, ref B_MBodySO, enums);
                //    break;
                //case PartEnum.LowerBody:
                //    Setting(so, B_LBone, ref B_LEquip, ref B_LBodySO, enums);
                //    break;
        }

    }
}