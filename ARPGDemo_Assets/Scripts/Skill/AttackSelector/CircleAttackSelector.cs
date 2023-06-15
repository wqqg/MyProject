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
    /// Բ�ι���ѡ��ӿڣ��㷨����ѡ�� Բ�������еĵ�����Ϊ����Ŀ��
    /// </summary>
    public class CircleAttackSelector: IAttackSelector
    {
        /// <summary>
        /// ѡ��Ŀ�귽����ѡ��Բ�������еĵ�����ΪҪ������Ŀ��
        /// </summary>
        /// <param name="skillData">���ܶ���</param>
        /// <param name="transform">�任����ѡ��ʱ�Ĳο���</param>
        /// <returns></returns>
        public GameObject[] SelectTarget(SkillData skillData, Transform skillTransform)
        {
            //1.ͨ�������ң�����û��tag��ǣ���tag��ǣ�ͨ��tag��
            //�ҳ����Ϊ XX("Enemy,Boss") ���е�����
            //��Ҫָ���뾶���������ң�tag����޷�ָ���뾶
            var colliders = Physics.OverlapSphere(skillTransform.position, skillData.attackDistance);
            if (colliders == null || colliders.Length == 0) 
                return null;
            //2.�ҳ����tag��attackTargetTags={"Enemy,Boss"}�е���������
            //  ���ŵ������У���HP>0
            var enemys = HelperFind.FindAll(colliders, c => 
            (Array.IndexOf(skillData.attackTargetTags,c.tag)>=0)&&
            (c.gameObject.GetComponent<CharacterStatus>().HP > 0));
            if(enemys == null||enemys.Length == 0) 
                return null;
            //3.���ݼ��ܹ������� ȷ��������������
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
