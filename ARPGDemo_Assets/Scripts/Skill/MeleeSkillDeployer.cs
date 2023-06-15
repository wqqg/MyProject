using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能施放器
/// </summary>
namespace ARPGDemo.Skill
{
    public class MeleeSkillDeployer : SkillDeployer
    {
        public override void DeploySkill()
        {
            if (skillData == null) return;
            //确定目标
            skillData.attackTargets = ResetTargets();
            //执行自身影响
            listSelfImpact.ForEach(p => p.SelfImpact(this, skillData, skillData.Onwer));
            //执行目标影响
            listTargetImpact.ForEach(p => p.TargetImpact(this, skillData, null));
            //回收技能
            CollectSkill();
        }
    }
}
