using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 攻击选择接口（算法）：选择 什么区域中的敌人作为攻击目标，例如：圆形、扇形区域中
    /// </summary>
    public interface IAttackSelector
    {
        /// <summary>
        /// 选择目标方法：选择哪些敌人作为要攻击的目标
        /// </summary>
        /// <param name="skillData">技能对象</param>
        /// <param name="transform">变换对象：选择时的参考点</param>
        /// <returns></returns>
        GameObject[] SelectTarget(SkillData skillData, Transform skillTransform);
    }
}
