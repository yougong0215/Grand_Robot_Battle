using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RobotSettingAndSOList : MonoBehaviour
{
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


    Stat _statues;

    public Stat Stat => _statues;



    void Setting(bool ReplaceMesh, PartSO input,GameObject bone,ref GameObject Equip, ref PartSO MYSO, GameObject mesh = null)
    {
        if (ReplaceMesh)
        {
            if(mesh != null)
                mesh.SetActive(false);
        }

        if (input == null)
        {
            if (mesh != null)
                mesh.SetActive(true);

            if (MYSO != null)
            {
                _statues.HP -= MYSO.Statues.HP;
                _statues.ATK -= MYSO.Statues.ATK;
                _statues.DEF -= MYSO.Statues.DEF;
                _statues.SPEED -= MYSO.Statues.SPEED;
            }

            if (Equip)
            {
                Destroy(Equip);
            }
            Equip = null;
            MYSO = null;

        }
        else
        {

            MYSO = input;

            _statues.HP += MYSO.Statues.HP;
            _statues.ATK += MYSO.Statues.ATK;
            _statues.DEF += MYSO.Statues.DEF;
            _statues.SPEED += MYSO.Statues.SPEED;

            Equip = Instantiate(input.PartAsset, bone.transform);
        }
    }
    


    
    public void EquipPart(PartEnum enums, PartSO so = null, bool ReplaceMesh = false)
    {
        switch (enums)
        {
            case PartEnum.None:
                break;
            case PartEnum.RightArm:

                Setting(ReplaceMesh, so,RightArmBone, ref RightEquip,ref Right, RightArmMesh);


                break;
            case PartEnum.LeftArm:

                Setting(ReplaceMesh, so,LeftArmBone, ref LeftEquip, ref Left, LeftArmMesh);
                break;

            case PartEnum.Legs:
                Setting(ReplaceMesh, so, LegBone, ref LegEquip, ref Leg, LegMesh);

                break;


            case PartEnum.Head:
                Setting(ReplaceMesh, so, HeadBone, ref HeadEquip, ref Head);


                break;
            case PartEnum.Body:
                Setting(ReplaceMesh, so, BodyBone, ref BodyEquip, ref Body);

                break;
        }

    }

    /// <summary>
    /// æ¿≥—æÓ∞°∏È º“»Ø«ÿ¡‡æﬂµ 
    /// </summary>
    public void VSSet()
    {
        gameObject.AddComponent<VSPlayer>();

        for(int i =0; i< 5; i++)
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
    public void SkillInput(PartSO ps = null, SkillScriptBase sk = null)
    {
        VSPlayer vs = GetComponent<VSPlayer>();
        if (ps == null)
        {
            if(sk == null)
            {
                vs.SkillAdd(new NoneAttack());
            }
            else
            {
                vs.SkillAdd(new NoneAttack()); // Head¿œ∂ß ¡§«ÿ¡‡æﬂµ 
            }
        }
        else
        {
            vs.SkillAdd(ps.Skill);
        }
    }
}