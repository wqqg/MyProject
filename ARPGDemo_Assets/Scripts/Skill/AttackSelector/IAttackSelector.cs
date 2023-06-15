using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// ����ѡ��ӿڣ��㷨����ѡ�� ʲô�����еĵ�����Ϊ����Ŀ�꣬���磺Բ�Ρ�����������
    /// </summary>
    public interface IAttackSelector
    {
        /// <summary>
        /// ѡ��Ŀ�귽����ѡ����Щ������ΪҪ������Ŀ��
        /// </summary>
        /// <param name="skillData">���ܶ���</param>
        /// <param name="transform">�任����ѡ��ʱ�Ĳο���</param>
        /// <returns></returns>
        GameObject[] SelectTarget(SkillData skillData, Transform skillTransform);
    }
}
