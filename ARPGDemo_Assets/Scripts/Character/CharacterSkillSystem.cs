using ARPGDemo.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPGDemo.Character
{
    /// <summary>
    /// ��ɫϵͳ�ͼ���ϵͳ����ࣺ��ɫ���������
    /// </summary>
    public class CharacterSkillSystem : MonoBehaviour
    {
        private CharacterAnimation chAnim;
        private GameObject currentAttackTarget;
        private SkillData currentUseSkill;
        private CharacterSkillManager skillMgr;
        private void Start()
        {
            chAnim = GetComponent<CharacterAnimation>();
            skillMgr = GetComponent<CharacterSkillManager>();
            //ʹ�ù����¼� ע�� �������>����Ƭ��>���÷���OnAttack�����¼�>ʩ�ż���
            GetComponentInChildren<AnimationEventBehaviour>().attackHandler += DeploySkill;
        }
        /// <summary>
        /// ʹ��ָ����ŵļ��ܽ��й���
        /// </summary>
        /// <param name="skillId">���ܱ��</param>
        /// <param name="isBatter">����</param>
        public void AttackUseSkill(int skillId, bool isBatter)
        {
            //�����������������ȡ��һ�����ܱ��
            if (isBatter && currentUseSkill != null)
                skillId = currentUseSkill.nextBatterld;
            //1.ͨ�����׼�� ����Ӧ�ļ������ݶ���
            currentUseSkill = skillMgr.PrepareSkill(skillId);
            if (currentUseSkill == null) return;
            //2.���ż��ܶ�Ӧ�Ĺ�������������ʩ���ɶ����¼����á�
            chAnim.PlayAnimation(currentUseSkill.animationName);
            //3.�ҳ��ܹ�����Ŀ��,����Ŀ�꣬���÷�������û�У������
            var selectedTarget = SelectTarget();
            if (selectedTarget == null) return;
            //4.��ʾѡ�е�Ŀ��Ч��
            //��һ��Ŀ��������Ч����ǰĿ����ʾ��Ч
            ShowSelectedFx(false);
            currentAttackTarget = selectedTarget;
            ShowSelectedFx(true);
            //5.����Ŀ��
            transform.LookAt(selectedTarget.transform);
        }
        public void DeploySkill()
        {
            if (currentUseSkill != null)
                skillMgr.DeploySkill(currentUseSkill);
        }
        private GameObject SelectTarget()
        {
            //1.��tag��ǣ�ͨ��tag��   ����Ҫָ���뾶
            //  �ҳ����tag��attackTargetTags={"Enemy,Boss"}�е���������
            List<GameObject> listTargets = new List<GameObject>();
            for (int i = 0; i < currentUseSkill.attackTargetTags.Length; i++)
            {
                var targets = GameObject.FindGameObjectsWithTag(currentUseSkill.attackTargetTags[i]);
                if (targets != null && targets.Length > 0)
                {
                    listTargets.AddRange(targets);
                }
            }
            if (listTargets.Count == 0) return null;
            //2.���ˣ��ȽϾ���(ָ���뾶)���е�����
            //  ���ŵ������У���HP>0
            var enemys = listTargets.FindAll(go =>
            (Vector3.Distance(go.transform.position, this.transform.position) < currentUseSkill.attackDistance) &&
            (go.GetComponent<CharacterStatus>().HP > 0));
            if (enemys == null || enemys.Count == 0) return null;
            //3.����ʱ�����ص���
            return ArrayHelper.Min(enemys.ToArray(), e =>
            Vector3.Distance(this.transform.position, e.transform.position));
        }
        private void ShowSelectedFx(bool isShow)
        {
            //������Ч
            Transform selectedFx = null;
            if (currentAttackTarget != null) 
            {
                selectedFx = TransformHelper.FindChild(currentAttackTarget.transform, "selected");
            }
            //���������Renderer���û����
            if (selectedFx != null)
            {
                selectedFx.GetComponent<MeshRenderer>().enabled = isShow;
            }
        }
        public void UseRandomSkill()
        {
            //�Ӽ����б������ȡһ�����õļ��ܣ�����{�Ѿ���ȴ��ϡ����ĵ�SP<����ӵ���ߵ�SP}
            //1.�ҳ����п��ü��ܼ���
            var usableSkills = skillMgr.skills.FindAll(skill => skill.coolTime == 0 &&
            skill.costSP < skill.Onwer.GetComponent<CharacterStatus>().SP);
            //2.�����ȡ�����е�һ�����ܶ���(���ݶ����ұ��)
            if (usableSkills != null && usableSkills.Count > 0)
            {
                var index = Random.Range(0, usableSkills.Count);
                var skillId = usableSkills[index].skillID;
                //����AttackUseSkill(int skillId, bool isBatter)
                AttackUseSkill(skillId, false);
            }
        }
    }
}
