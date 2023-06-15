using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ARPGDemo.Skill
{
    /// <summary>
    /// ����Ӱ��ӿ�(�㷨)
    /// </summary>
    public interface ISelfImpact
    {
        /// <summary>
        /// Ӱ������Ĳ���
        /// </summary>
        /// <param name="deployer">����ʩ����</param>
        /// <param name="SkillData">�������ݶ���</param>
        /// <param name="goSelf">�������Ѷ���</param>
        void SelfImpact(SkillDeployer deployer, SkillData skillData,GameObject goSelf);
    }
}
