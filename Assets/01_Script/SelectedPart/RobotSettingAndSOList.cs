using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RobotSettingAndSOList : MonoBehaviour
{
    [SerializeField] bool AI = false;
    public Team team;

    [Header("Left Arm")]
    [SerializeField] GameObject LeftArmBone;
    [SerializeField] GameObject LeftArmMesh;
    [SerializeField] GameObject LeftEquip;
    [SerializeField] PartSO Left;
    [Header("Right Arm")]
    [SerializeField] GameObject RightArmBone;
    [SerializeField] GameObject RightArmMesh;
    [SerializeField] GameObject RightEquip;
    [SerializeField] PartSO Right;

    [Header("Head")]
    [SerializeField] GameObject HeadBone;
    [SerializeField] GameObject HeadEquip;
    [SerializeField] PartSO Head;

    [Header("Body")]
    [SerializeField] GameObject BodyBone;
    [SerializeField] GameObject BodyEquip;
    [SerializeField] PartSO Body;

    [Header("Leg")]
    [SerializeField] GameObject LegBone;
    [SerializeField] GameObject LegMesh;
    [SerializeField] GameObject LegEquip;
    [SerializeField] PartSO Leg;


    public Stat _statues = new Stat();


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


    private void Start()
    {
        if(team == Team.Blue)
        {
            if (Left)
            {
                EquipPart(PartEnum.LeftArm, Left, Left.RepalceMesh);
            }

            if (Right)
            {
                EquipPart(PartEnum.RightArm, Right, Right.RepalceMesh);
            }

            if (Head)
            {
                EquipPart(PartEnum.Head, Head);
            }

            if (Body)
            {
                EquipPart(PartEnum.Body, Body);
            }

            if (Leg)
            {
                EquipPart(PartEnum.Legs, Leg);
            }
        }
       
    }



    PartSO Setting(bool ReplaceMesh, PartSO input,GameObject bone, ref GameObject Equip, PartSO MYSO, GameObject mesh = null)
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
            case PartEnum.RightArm:
                Right = Setting(ReplaceMesh, so, RightArmBone,ref RightEquip, Right, RightArmMesh);


                break;
            case PartEnum.LeftArm:
                Debug.Log(LeftArmMesh);
                Left = Setting(ReplaceMesh, so, LeftArmBone,ref LeftEquip, Left, LeftArmMesh);
                break;

            case PartEnum.Legs:


                Leg = Setting(ReplaceMesh, so, LegBone, ref  LegEquip, Leg, LegMesh);

                break;


            case PartEnum.Head:

                Head = Setting(ReplaceMesh, so, HeadBone,ref HeadEquip,Head);


                break;
            case PartEnum.Body:


                Body = Setting(ReplaceMesh, so, BodyBone,ref BodyEquip, Body);

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
                    SkillInput(Left);
                    break;
                case 2:
                    SkillInput(Right);
                    break;
                case 3:
                    SkillInput(Leg);
                    break;
                case 4:
                    SkillInput(Body);
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