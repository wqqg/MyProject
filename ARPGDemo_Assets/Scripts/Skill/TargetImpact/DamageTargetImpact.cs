using ARPGDemo.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// Ŀ��Ӱ���ࣺHP�ļ��١��˺���
    /// </summary>
    public class DamageTargetImpact:ITargetImpact
    {
        private int baseDamage = 0;
        /// <summary>
        /// Ӱ��Ŀ��ķ���
        /// </summary>
        /// <param name="deployer">����ʩ����</param>
        /// <param name="SkillData">�������ݶ���</param>
        /// <param name="goSelf">Ŀ�����</param>
        public void TargetImpact(SkillDeployer deployer, SkillData SkillData, GameObject goSelf)
        {
            //��ȡ����ӵ���߻����˺�
            if (SkillData.Onwer != null && SkillData.Onwer.gameObject != null)
            {
                baseDamage = SkillData.Onwer.GetComponent<CharacterStatus>().Damage;
            }
            //ִ���˺�
            deployer.StartCoroutine(RepeatDamage(deployer, SkillData));
        }
        //�����˺�
        private void OnceDamage(SkillData skill,GameObject goTarget)
        {
            //1.���ý�ɫOnDamage����
            var chStatus = goTarget.GetComponent<CharacterStatus>();
            var damageVal = baseDamage * skill.damage;
            chStatus.OnDamage((int)damageVal);
            //2.���ܻ���Ч���ص�Ŀ������
            if (skill.hitFxPrefab != null && chStatus.HitFxPos != null)
            {
                //1).����һ���ܻ���ЧԤ�Ƽ�����
                var hitGo = GameObjectPool.instance.CreateObject(skill.hitFxName,
                    skill.hitFxPrefab, chStatus.HitFxPos.position, chStatus.HitFxPos.rotation);
                //2).���ö�������ڵ���
                hitGo.transform.parent = chStatus.HitFxPos;
                //3).��Ч���������
                GameObjectPool.instance.CollectObject(hitGo, 0.2f);
            }
        }
        //�ظ��˺�
        private IEnumerator RepeatDamage(SkillDeployer deployer, SkillData skill)
        {
            float attackTime = 0;
            do
            {
                if (skill.attackTargets != null && skill.attackTargets.Length > 0)
                {
                    //�Զ��Ŀ��ִ���˺�
                    for (int i = 0; i < skill.attackTargets.Length; i++)
                    {
                        OnceDamage(skill, skill.attackTargets[i]);
                    }
                    //���һ��ʱ�䣬�ٴ�ִ���˺�
                    yield return new WaitForSeconds(skill.damageInterval);
                    attackTime += skill.damageInterval;//durationTime����0��damageIntervalҲ����Ϊ0
                    //����һ��֮��Ҫ����ѡȡĿ��
                    skill.attackTargets = deployer.ResetTargets();
                }
            } while (attackTime < skill.durationTime);//��ֹ��ѭ��
        }
    }
}
