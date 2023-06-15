using ARPGDemo.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 目标影响类：HP的减少【伤害】
    /// </summary>
    public class DamageTargetImpact:ITargetImpact
    {
        private int baseDamage = 0;
        /// <summary>
        /// 影响目标的方法
        /// </summary>
        /// <param name="deployer">技能施放器</param>
        /// <param name="SkillData">技能数据对象</param>
        /// <param name="goSelf">目标对象</param>
        public void TargetImpact(SkillDeployer deployer, SkillData SkillData, GameObject goSelf)
        {
            //获取技能拥有者基础伤害
            if (SkillData.Onwer != null && SkillData.Onwer.gameObject != null)
            {
                baseDamage = SkillData.Onwer.GetComponent<CharacterStatus>().Damage;
            }
            //执行伤害
            deployer.StartCoroutine(RepeatDamage(deployer, SkillData));
        }
        //单次伤害
        private void OnceDamage(SkillData skill,GameObject goTarget)
        {
            //1.调用角色OnDamage方法
            var chStatus = goTarget.GetComponent<CharacterStatus>();
            var damageVal = baseDamage * skill.damage;
            chStatus.OnDamage((int)damageVal);
            //2.将受击特效挂载到目标身上
            if (skill.hitFxPrefab != null && chStatus.HitFxPos != null)
            {
                //1).创建一个受击特效预制件对象
                var hitGo = GameObjectPool.instance.CreateObject(skill.hitFxName,
                    skill.hitFxPrefab, chStatus.HitFxPos.position, chStatus.HitFxPos.rotation);
                //2).将该对象挂载在点上
                hitGo.transform.parent = chStatus.HitFxPos;
                //3).特效播放完回收
                GameObjectPool.instance.CollectObject(hitGo, 0.2f);
            }
        }
        //重复伤害
        private IEnumerator RepeatDamage(SkillDeployer deployer, SkillData skill)
        {
            float attackTime = 0;
            do
            {
                if (skill.attackTargets != null && skill.attackTargets.Length > 0)
                {
                    //对多个目标执行伤害
                    for (int i = 0; i < skill.attackTargets.Length; i++)
                    {
                        OnceDamage(skill, skill.attackTargets[i]);
                    }
                    //间隔一个时间，再次执行伤害
                    yield return new WaitForSeconds(skill.damageInterval);
                    attackTime += skill.damageInterval;//durationTime不是0，damageInterval也不能为0
                    //攻击一次之后，要重新选取目标
                    skill.attackTargets = deployer.ResetTargets();
                }
            } while (attackTime < skill.durationTime);//防止死循环
        }
    }
}
