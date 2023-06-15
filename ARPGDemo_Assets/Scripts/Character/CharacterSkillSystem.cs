using ARPGDemo.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPGDemo.Character
{
    /// <summary>
    /// 角色系统和技能系统外观类：角色技能外观类
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
            //使用攻击事件 注册 动画组件>动画片段>调用方法OnAttack触发事件>施放技能
            GetComponentInChildren<AnimationEventBehaviour>().attackHandler += DeploySkill;
        }
        /// <summary>
        /// 使用指定编号的技能进行攻击
        /// </summary>
        /// <param name="skillId">技能编号</param>
        /// <param name="isBatter">连击</param>
        public void AttackUseSkill(int skillId, bool isBatter)
        {
            //如果是连续攻击，获取下一个技能编号
            if (isBatter && currentUseSkill != null)
                skillId = currentUseSkill.nextBatterld;
            //1.通过编号准备 出对应的技能数据对象
            currentUseSkill = skillMgr.PrepareSkill(skillId);
            if (currentUseSkill == null) return;
            //2.播放技能对应的攻击动画【技能施放由动画事件调用】
            chAnim.PlayAnimation(currentUseSkill.animationName);
            //3.找出受攻击的目标,若由目标，调用方法，若没有，打空气
            var selectedTarget = SelectTarget();
            if (selectedTarget == null) return;
            //4.显示选中的目标效果
            //上一个目标隐藏特效，当前目标显示特效
            ShowSelectedFx(false);
            currentAttackTarget = selectedTarget;
            ShowSelectedFx(true);
            //5.面向目标
            transform.LookAt(selectedTarget.transform);
        }
        public void DeploySkill()
        {
            if (currentUseSkill != null)
                skillMgr.DeploySkill(currentUseSkill);
        }
        private GameObject SelectTarget()
        {
            //1.有tag标记：通过tag找   不需要指定半径
            //  找出标记tag在attackTargetTags={"Enemy,Boss"}中的所有物体
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
            //2.过滤：比较距离(指定半径)所有的物体
            //  活着的物体中（）HP>0
            var enemys = listTargets.FindAll(go =>
            (Vector3.Distance(go.transform.position, this.transform.position) < currentUseSkill.attackDistance) &&
            (go.GetComponent<CharacterStatus>().HP > 0));
            if (enemys == null || enemys.Count == 0) return null;
            //3.攻击时，返回单个
            return ArrayHelper.Min(enemys.ToArray(), e =>
            Vector3.Distance(this.transform.position, e.transform.position));
        }
        private void ShowSelectedFx(bool isShow)
        {
            //定义特效
            Transform selectedFx = null;
            if (currentAttackTarget != null) 
            {
                selectedFx = TransformHelper.FindChild(currentAttackTarget.transform, "selected");
            }
            //控制物体的Renderer启用或禁用
            if (selectedFx != null)
            {
                selectedFx.GetComponent<MeshRenderer>().enabled = isShow;
            }
        }
        public void UseRandomSkill()
        {
            //从技能列表随机抽取一个可用的技能，可用{已经冷却完毕、消耗的SP<技能拥有者的SP}
            //1.找出所有可用技能集合
            var usableSkills = skillMgr.skills.FindAll(skill => skill.coolTime == 0 &&
            skill.costSP < skill.Onwer.GetComponent<CharacterStatus>().SP);
            //2.随机抽取集合中的一个技能对象(根据对象找编号)
            if (usableSkills != null && usableSkills.Count > 0)
            {
                var index = Random.Range(0, usableSkills.Count);
                var skillId = usableSkills[index].skillID;
                //调用AttackUseSkill(int skillId, bool isBatter)
                AttackUseSkill(skillId, false);
            }
        }
    }
}
