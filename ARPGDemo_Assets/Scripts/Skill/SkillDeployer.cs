using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ʩ����
/// </summary>
namespace ARPGDemo.Skill
{
    abstract public class SkillDeployer : MonoBehaviour
    {
        //Ҫʩ�ŵļ���
        private SkillData m_skillData;
        public SkillData skillData
        {
            get { return m_skillData; }
            set //ʹ��һ������,��ʼ�������ֶ�
            {
                if (value == null) return;
                m_skillData = value;
                //����ʩ���������ù���,�Ӷ�ʵ�ֳ�ʼ���ֶ�
                attackSelector = DeployerConfigFactor.CreatAttackSelector(m_skillData);
                listSelfImpact = DeployerConfigFactor.CreatSelfImpact(m_skillData);
                listTargetImpact = DeployerConfigFactor.CreatTargetImpact(m_skillData);
            }
        }
        //�����㷨����DeployerConfigFactor���г�ʼ��
        protected IAttackSelector attackSelector;
        protected List<ISelfImpact> listSelfImpact = new List<ISelfImpact>();
        protected List<ITargetImpact> listTargetImpact = new List<ITargetImpact>();

        //ʩ�ż���,��ΪԶ�����ս,д�ɳ�����
        abstract public void DeploySkill();
        /// <summary>
        /// ����Ŀ��:����ѡ��Ŀ��
        /// </summary>
        public GameObject[] ResetTargets()
        {
            var targets = attackSelector.SelectTarget(m_skillData, transform);
            if (targets != null && targets.Length > 0) return targets;
            return null;
        }
        /// <summary>
        /// ���ܻ���
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
