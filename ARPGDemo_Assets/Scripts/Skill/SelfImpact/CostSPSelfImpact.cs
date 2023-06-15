using ARPGDemo.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 自身影响类：SP减少了
    /// </summary>
    public class CostSPSelfImpact:ISelfImpact
    {
        /// <summary>
        /// 影响自身的方法
        /// </summary>
        /// <param name="deployer">技能施放器</param>
        /// <param name="SkillData">技能数据对象</param>
        /// <param name="goSelf">自身或队友对象</param>
        public void SelfImpact(SkillDeployer deployer, SkillData skillData, GameObject goSelf)
        {
            //找到技能拥有者的SP = 找到技能拥有者的SP - skillData.costSP
            if (skillData.Onwer == null) return;
            var chstatus = skillData.Onwer.GetComponent<CharacterStatus>();
            chstatus.SP -= skillData.costSP;
        }
    }
}
