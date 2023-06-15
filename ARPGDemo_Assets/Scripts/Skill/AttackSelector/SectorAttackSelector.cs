using ARPGDemo.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
///
/// </summary>
namespace ARPGDemo.Skill
{
    /// <summary>
    /// 扇形攻击选择接口（算法）：选择 扇形区域中的敌人作为攻击目标
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        /// <summary>
        /// 选择目标方法：选择扇形区域中的敌人作为要攻击的目标
        /// </summary>
        /// <param name="skillData">技能对象</param>
        /// <param name="transform">变换对象：选择时的参考点</param>
        /// <returns></returns>
        public GameObject[] SelectTarget(SkillData skillData, Transform skillTransform)
        {
            //1.有tag标记：通过tag找   不需要指定半径
            //  找出标记tag在attackTargetTags={"Enemy,Boss"}中的所有物体
            List<GameObject> listTargets = new List<GameObject>();
            for (int i = 0; i < skillData.attackTargetTags.Length; i++)
            {
                var targets = GameObject.FindGameObjectsWithTag(skillData.attackTargetTags[i]);
                if (targets != null && targets.Length > 0)
                {
                    listTargets.AddRange(targets);
                }
            }
            if(listTargets.Count == 0) return null;
            //2.过滤：比较距离(指定半径)所有的物体
            //  活着的物体中（）HP>0
            var enemys = listTargets.FindAll(go =>
            (Vector3.Distance(go.transform.position,skillTransform.position)<skillData.attackDistance) && 
            (go.GetComponent<CharacterStatus>().HP>0) && 
            (Vector3.Angle(skillTransform.forward,go.transform.position-skillTransform.position)<=skillData.attackAngle*0.5f));
            if(enemys==null||enemys.Count==0) return null;
            //3.根据技能攻击类型 确定攻击单个或多个
            switch (skillData.attackType)
            {
                case SkillAttackType.Single:
                    var go = ArrayHelper.Min(enemys.ToArray(), e =>
                    Vector3.Distance(skillTransform.position, e.transform.position));
                    return new GameObject[] { go };
                case SkillAttackType.Group:
                    return enemys.ToArray();
            }
            return null;
        }
    }
}
