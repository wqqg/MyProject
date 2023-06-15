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
    /// ���ι���ѡ��ӿڣ��㷨����ѡ�� ���������еĵ�����Ϊ����Ŀ��
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        /// <summary>
        /// ѡ��Ŀ�귽����ѡ�����������еĵ�����ΪҪ������Ŀ��
        /// </summary>
        /// <param name="skillData">���ܶ���</param>
        /// <param name="transform">�任����ѡ��ʱ�Ĳο���</param>
        /// <returns></returns>
        public GameObject[] SelectTarget(SkillData skillData, Transform skillTransform)
        {
            //1.��tag��ǣ�ͨ��tag��   ����Ҫָ���뾶
            //  �ҳ����tag��attackTargetTags={"Enemy,Boss"}�е���������
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
            //2.���ˣ��ȽϾ���(ָ���뾶)���е�����
            //  ���ŵ������У���HP>0
            var enemys = listTargets.FindAll(go =>
            (Vector3.Distance(go.transform.position,skillTransform.position)<skillData.attackDistance) && 
            (go.GetComponent<CharacterStatus>().HP>0) && 
            (Vector3.Angle(skillTransform.forward,go.transform.position-skillTransform.position)<=skillData.attackAngle*0.5f));
            if(enemys==null||enemys.Count==0) return null;
            //3.���ݼ��ܹ������� ȷ��������������
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
