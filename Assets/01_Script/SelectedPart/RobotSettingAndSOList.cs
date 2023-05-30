using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RobotSettingAndSOList : MonoBehaviour
{
    [SerializeField] bool AI = false;
    public Team team;

    [Header("Left Upper Arm")]
    [SerializeField] GameObject         A_L_UBone;
    [SerializeField] GameObject         A_L_UMesh;
    [SerializeField] GameObject         A_L_UEquip;
    [SerializeField] PartSO             A_L_USO;

    [Header("Left Middle Arm")]
    [SerializeField] GameObject         A_L_MBone;
    [SerializeField] GameObject         A_L_MEquip;
    [SerializeField] PartSO             A_L_MSO;

    [Header("Left Lower Arm")]
    [SerializeField] GameObject         A_L_LBone;
    [SerializeField] GameObject         A_L_LMesh;
    [SerializeField] GameObject         A_L_LEquip;
    [SerializeField] PartSO             A_L_LSO;

    [Header("Right Upper Arm")]
    [SerializeField] GameObject         A_R_UBone;
    [SerializeField] GameObject         A_R_UMesh;
    [SerializeField] GameObject         A_R_UEquip;
    [SerializeField] PartSO             A_R_USO;

    [Header("Right Middle Arm")]
    [SerializeField] GameObject         A_R_MBone;
    [SerializeField] GameObject         A_R_MEquip;
    [SerializeField] PartSO             A_R_MSO;

    [Header("Right Lower Arm")]
    [SerializeField] GameObject         A_R_LBone;
    [SerializeField] GameObject         A_R_LMesh;
    [SerializeField] GameObject         A_R_LEquip;
    [SerializeField] PartSO             A_R_LSO;

    [Header("Left Upper Leg")]
    [SerializeField] GameObject         L_L_UBone;
    [SerializeField] GameObject         L_L_UMesh;
    [SerializeField] GameObject         L_L_UEquip;
    [SerializeField] PartSO             L_L_USO;

    [Header("Left Middle Leg")]
    [SerializeField] GameObject         L_L_MBone;
    [SerializeField] GameObject         L_L_MEquip;
    [SerializeField] PartSO             L_L_MSO;

    [Header("Left Lower Leg")]
    [SerializeField] GameObject         L_L_LBone;
    [SerializeField] GameObject         L_L_LMesh;
    [SerializeField] GameObject         L_L_LEquip;
    [SerializeField] PartSO             L_L_LSO;


    [Header("Right Upper Leg")]
    [SerializeField] GameObject         L_R_UBone;
    [SerializeField] GameObject         L_R_UMesh;
    [SerializeField] GameObject         L_R_UEquip;
    [SerializeField] PartSO             L_R_USO;

    [Header("Right Middle Leg")]
    [SerializeField] GameObject         L_R_MBone;
    [SerializeField] GameObject         L_R_MEquip;
    [SerializeField] PartSO             L_R_MSO;

    [Header("Right Lower Leg")]
    [SerializeField] GameObject         L_R_LBone;
    [SerializeField] GameObject         L_R_LMesh;
    [SerializeField] GameObject         L_R_LEquip;
    [SerializeField] PartSO             L_R_LSO;




    [Header("Head")]
    [SerializeField] GameObject         HeadBone;
    [SerializeField] GameObject         HeadEquip;
    [SerializeField] PartSO             Head;

    [Header("Body")]
    [SerializeField] GameObject         B_UBone;
    [SerializeField] GameObject         B_UEquip;
    [SerializeField] PartSO             B_UBody;

    [SerializeField] GameObject         B_MBone;
    [SerializeField] GameObject         B_MEquip;
    [SerializeField] PartSO             B_MBody;

    [SerializeField] GameObject         B_LBone;
    [SerializeField] GameObject         B_LEquip;
    [SerializeField] PartSO             B_LBody;



    public Stat                         _statues = new Stat();


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


    private void Start()
    {
        if(team == Team.Blue)
        {
            if (A_L_USO)
            {
                EquipPart(PartEnum.ALU, A_L_USO, A_L_USO.RepalceMesh);
            }
            if (A_L_MSO)
            {
                EquipPart(PartEnum.ALM, A_L_MSO, A_L_MSO.RepalceMesh);
            }
            if (A_L_LSO)
            {
                EquipPart(PartEnum.ALL, A_L_LSO, A_L_LSO.RepalceMesh);
            }

            if (A_R_USO)
            {
                EquipPart(PartEnum.ARU, A_R_USO, A_R_USO.RepalceMesh);
            }
            if (A_R_MSO)
            {
                EquipPart(PartEnum.ARM, A_R_MSO, A_R_MSO.RepalceMesh);
            }
            if (A_R_LSO)
            {
                EquipPart(PartEnum.ARL, A_R_LSO, A_R_LSO.RepalceMesh);
            }

            if (L_L_USO)
            {
                EquipPart(PartEnum.LLU, L_L_USO, L_L_USO.RepalceMesh);
            }
            if (L_L_MSO)
            {
                EquipPart(PartEnum.LLM, L_L_MSO, L_L_MSO.RepalceMesh);
            }
            if (L_L_LSO)
            {
                EquipPart(PartEnum.LLL, L_L_LSO, L_L_LSO.RepalceMesh);
            }

            if (L_R_USO)
            {
                EquipPart(PartEnum.LRU, L_R_USO, L_R_USO.RepalceMesh);
            }
            if (L_R_MSO)
            {
                EquipPart(PartEnum.LRM, L_R_MSO, L_R_MSO.RepalceMesh);
            }
            if (L_R_LSO)
            {
                EquipPart(PartEnum.LRL, L_R_LSO, L_R_LSO.RepalceMesh);
            }

            

            if (Head)
            {
                EquipPart(PartEnum.Head, Head);
            }

            //if (Body)
            //{
            //    EquipPart(PartEnum., Body);
            //}
        }   
       
    }



    PartSO Setting(bool ReplaceMesh, PartSO input,GameObject bone, GameObject Equip, PartSO MYSO, GameObject mesh = null)
    {
        if (ReplaceMesh)
        {
            if(mesh != null)
                mesh.SetActive(false);
        }

        if (input == null)
        {
            if (Equip != null)
            {
                Destroy(Equip);
            }

            if (mesh != null)
                mesh.SetActive(true);

            if (MYSO != null)
            {
                _statues.HP -= MYSO.Statues.HP;
                _statues.ATK -= MYSO.Statues.ATK;
                _statues.DEF -= MYSO.Statues.DEF;
                _statues.SPEED -= MYSO.Statues.SPEED;
            }

            MYSO = null;

        }
        else
        {
            if(Equip != null)
            {
                Destroy(Equip);
            }

            MYSO = input;

            _statues.HP += MYSO.Statues.HP;
            _statues.ATK += MYSO.Statues.ATK;
            _statues.DEF += MYSO.Statues.DEF;
            _statues.SPEED += MYSO.Statues.SPEED;

            Equip = Instantiate(input.PartAsset, bone.transform);
        }


        return MYSO;
    }




    public void EquipPart(PartEnum enums, PartSO so = null, bool ReplaceMesh = false)
    {
        switch (enums)
        {
            case PartEnum.None:
                break;
            case PartEnum.ALU:
                A_L_USO = Setting(ReplaceMesh, so, A_L_UBone, A_L_UEquip, A_L_USO, A_L_UMesh);
                break;
            case PartEnum.ALM:
                A_L_MSO = Setting(ReplaceMesh, so, A_L_MBone, A_L_MEquip, A_L_MSO);
                break;
            case PartEnum.ALL:
                A_L_LSO = Setting(ReplaceMesh, so, A_L_LBone, A_L_LEquip, A_L_LSO, A_L_LMesh);
                break;
            case PartEnum.ARU:
                A_R_USO = Setting(ReplaceMesh, so, A_R_UBone, A_R_UEquip, A_R_USO, A_R_UMesh);
                break;
            case PartEnum.ARM:
                break;
            case PartEnum.ARL:
                break;
            case PartEnum.LLU:
                break;
            case PartEnum.LLM:
                break;
            case PartEnum.LLL:
                break;
            case PartEnum.LRU:
                break;
            case PartEnum.LRM:
                break;
            case PartEnum.LRL:
                break;
            case PartEnum.Head:
                break;
            case PartEnum.UpperBody:
                break;
            case PartEnum.MiddleBody:
                break;
            case PartEnum.LowerBody:
                break;
        }

    }

    /// <summary>
    /// æ¿≥—æÓ∞°∏È º“»Ø«ÿ¡‡æﬂµ 
    /// </summary>
    public void VSSet()
    {
        gameObject.AddComponent<VSPlayer>().team = team;


        gameObject.GetComponent<VSPlayer>().AI = AI;


        for (int i =0; i< 5; i++)
        {
            switch (i)
            {
                case 0:
                    SkillInput(Head);
                    break;
                case 1:
                    SkillInput(LeftUpperArmSO);
                    break;
                case 2:
                    SkillInput(RightUpperArmSO);
                    break;
                case 3:
                    SkillInput(Leg);
                    break;
                case 4:
                    SkillInput(B_Uody);
                    break;
            }
        }
    }
    public void SkillInput(PartSO ps = null)
    {
        VSPlayer vs = GetComponent<VSPlayer>();



        if (ps == null)
        {
            GameObject obj = new GameObject();
            obj.transform.parent = transform;
            obj.AddComponent<NoneAttack>();

            vs.SkillAdd(obj.GetComponent<NoneAttack>(), null);
        }
        else
        {
            if (ps.Skill == null)
            {
                GameObject obj = new GameObject();
                obj.transform.parent = transform;
                obj.AddComponent<NoneAttack>();
                vs.SkillAdd(obj.GetComponent<NoneAttack>(), ps);
            }
            else
            {
                SkillScriptBase a =  Instantiate(ps.Skill, transform);
                vs.SkillAdd(a, ps);
            }
        }
    }
}