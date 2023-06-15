using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ʩ����
/// </summary>
namespace ARPGDemo.Skill
{
    public class MeleeSkillDeployer : SkillDeployer
    {
        public override void DeploySkill()
        {
            if (skillData == null) return;
            //ȷ��Ŀ��
            skillData.attackTargets = ResetTargets();
            //ִ������Ӱ��
            listSelfImpact.ForEach(p => p.SelfImpact(this, skillData, skillData.Onwer));
            //ִ��Ŀ��Ӱ��
            listTargetImpact.ForEach(p => p.TargetImpact(this, skillData, null));
            //���ռ���
            CollectSkill();
        }
    }
}
