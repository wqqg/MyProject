using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// Ŀ��Ӱ��ӿڣ��㷨��
    /// </summary>
    public interface ITargetImpact
    {
        /// <summary>
        /// Ӱ��Ŀ��ķ���
        /// </summary>
        /// <param name="deployer">����ʩ����</param>
        /// <param name="SkillData">�������ݶ���</param>
        /// <param name="goSelf">Ŀ�����</param>
        void TargetImpact(SkillDeployer deployer, SkillData SkillData, GameObject goSelf);
    }
}
