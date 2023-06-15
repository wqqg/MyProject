using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能施放器
/// </summary>
namespace ARPGDemo.Skill
{
    abstract public class SkillDeployer : MonoBehaviour
    {
        //要施放的技能
        private SkillData m_skillData;
        public SkillData skillData
        {
            get { return m_skillData; }
            set //使用一个属性,初始化所有字段
            {
                if (value == null) return;
                m_skillData = value;
                //调用施放器的配置工厂,从而实现初始化字段
                attackSelector = DeployerConfigFactor.CreatAttackSelector(m_skillData);
                listSelfImpact = DeployerConfigFactor.CreatSelfImpact(m_skillData);
                listTargetImpact = DeployerConfigFactor.CreatTargetImpact(m_skillData);
            }
        }
        //攻击算法：在DeployerConfigFactor类中初始化
        protected IAttackSelector attackSelector;
        protected List<ISelfImpact> listSelfImpact = new List<ISelfImpact>();
        protected List<ITargetImpact> listTargetImpact = new List<ITargetImpact>();

        //施放技能,分为远程与近战,写成抽象类
        abstract public void DeploySkill();
        /// <summary>
        /// 重置目标:重新选择目标
        /// </summary>
        public GameObject[] ResetTargets()
        {
            var targets = attackSelector.SelectTarget(m_skillData, transform);
            if (targets != null && targets.Length > 0) return targets;
            return null;
        }
        /// <summary>
        /// 技能回收
        /// </summary>
        public void CollectSkill() 
        {
            if (m_skillData.durationTime > 0)
            {
                GameObjectPool.instance.CollectObject(this.gameObject, m_skillData.durationTime);
            }
            else
            {
                GameObjectPool.instance.CollectObject(this.gameObject, 0.2f);
            }
        }
    }
}
