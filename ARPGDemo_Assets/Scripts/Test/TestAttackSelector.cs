using ARPGDemo.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class TestAttackSelector : MonoBehaviour
{
    
    void Start()
    {
        SkillData skillData = new SkillData();
        skillData.attackDistance = 5;
        skillData.attackType = SkillAttackType.Group;
        skillData.attackAngle = 120;//只对扇形攻击模式有用
        IAttackSelector selector = new SectorAttackSelector(); //CircleAttackSelector();
        var allEnemy = selector.SelectTarget(skillData, this.transform);
        if (allEnemy != null)
        {
            foreach (var enemy in allEnemy)
            {
                enemy.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }
}
