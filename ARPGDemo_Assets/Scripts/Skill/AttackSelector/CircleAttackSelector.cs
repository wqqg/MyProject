using ARPGDemo.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
namespace ARPGDemo.Skill
{
    /// <summary>
    /// 圆形攻击选择接口（算法）：选择 圆形区域中的敌人作为攻击目标
    /// </summary>
    public class CircleAttackSelector: IAttackSelector
    {
        /// <summary>
        /// 选择目标方法：选择圆形区域中的敌人作为要攻击的目标
        /// </summary>
        /// <param name="skillData">技能对象</param>
        /// <param name="transform">变换对象：选择时的参考点</param>
        /// <returns></returns>
        public GameObject[] SelectTarget(SkillData skillData, Transform skillTransform)
        {
            //1.通过射线找：物体没有tag标记；有tag标记：通过tag找
            //找出标记为 XX("Enemy,Boss") 所有的物体
            //需要指定半径，用射线找，tag标记无法指定半径
            var colliders = Physics.OverlapSphere(skillTransform.position, skillData.attackDistance);
            if (colliders == null || colliders.Length == 0) 
                return null;
            //2.找出标记tag在attackTargetTags={"Enemy,Boss"}中的所有物体
            //  活着的物体中（）HP>0
            var enemys = HelperFind.FindAll(colliders, c => 
            (Array.IndexOf(skillData.attackTargetTags,c.tag)>=0)&&
            (c.gameObject.GetComponent<CharacterStatus>().HP > 0));
            if(enemys == null||enemys.Length == 0) 
                return null;
            //3.根据技能攻击类型 确定攻击单个或多个
            switch (skillData.attackType)
            {
                case SkillAttackType.Single:
                    var collider = ArrayHelper.Min(enemys, e =>
                    Vector3.Distance(skillTransform.position, e.transform.position));
                    return new GameObject[] { collider.gameObject };
                case SkillAttackType.Group:
                    return HelperFind.Select(enemys, e => e.gameObject);
            }
            return null;
        }
    }
}
