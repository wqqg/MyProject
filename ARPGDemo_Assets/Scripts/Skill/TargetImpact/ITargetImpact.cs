using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 目标影响接口（算法）
    /// </summary>
    public interface ITargetImpact
    {
        /// <summary>
        /// 影响目标的方法
        /// </summary>
        /// <param name="deployer">技能施放器</param>
        /// <param name="SkillData">技能数据对象</param>
        /// <param name="goSelf">目标对象</param>
        void TargetImpact(SkillDeployer deployer, SkillData SkillData, GameObject goSelf);
    }
}
