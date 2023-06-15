using System;
using ARPGDemo.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;

/// <summary>
///
/// </summary>
namespace ARPGDemo.Skill
{
    public class CharacterSkillManager : MonoBehaviour
    {
        //字段。管理多个技能数据对象--容器List<SkillData>所有技能
        public List<SkillData> skills = new List<SkillData>();
        //方法：
        //1.初始化
        void Start()
        {
            //简单类型通过属性窗口直接赋值。初始化复杂类型，通过代码赋值。
            foreach (var skill in skills) 
            {
                //如果skill.perfabName（技能名字）字符串不为null或“”时执行
                if (!(string.IsNullOrEmpty(skill.perfabName))&&skill.skillPerfab==null)
                {
                    skill.skillPerfab = LoadPrefab(skill.perfabName);
                }
                if (!(string.IsNullOrEmpty(skill.hitFxName)) && skill.hitFxPrefab == null)
                {
                    skill.hitFxPrefab = LoadPrefab(skill.hitFxName);
                }
                //为技能指定拥有者
                skill.Onwer = this.gameObject;
            }
        }
        //根据预制件资源名称resName，动态加载预制件资源
        public GameObject LoadPrefab(string resName)
        {
            //动态加载预制件资源
            var prefabGo = Resources.Load<GameObject>(resName);

            //使用游戏对象池 初始化就准备好技能，防止第一次使用技能时出现卡顿现象
            //若不用对象池，不管卡顿，可以不用
            var tempGo = GameObjectPool.instance.CreateObject(resName, prefabGo, this.transform.position, transform.rotation);
            //上面代码生成物物体，因为是初始化，所以不需要出现在场景中,因此需要瞬间回收进池中
            GameObjectPool.instance.CollectObject(tempGo);

            return prefabGo;
        }
        //2.准备技能
        public SkillData PrepareSkill(int id)
        {
            //1.根据技能id找技能容器中是否有这个技能
            var skill = skills.Find(skill => skill.skillID == id);
            //2.若找到，同时技能已经冷却，且SP值足够，返回
            if (skill != null)
            {
                if (skill.coolRemain == 0&& skill.costSP <= skill.Onwer.GetComponent<CharacterStatus>().SP)
                {
                    return skill;
                }
            }
            return null;
        }
        //3.施放技能 调用施放器的释放的方法
        public void DeploySkill(SkillData skillData)
        {
            //1.创建技能预制件对象 对象池创建
            var tempGo = GameObjectPool.instance.CreateObject(skillData.perfabName, skillData.skillPerfab, this.transform.position, transform.rotation);
            //2.为技能预制件对象设置当前要使用的技能
            var deployer = tempGo.GetComponent<SkillDeployer>();
            //3.调用施放器的施放方法
            deployer.skillData = skillData;
            deployer.DeploySkill();
            //4.冷却计时
            StartCoroutine(CoolTimeDown(skillData));
            //技能对象需要回收 留给施放器完成

        }
        //4.技能冷却处理
        private IEnumerator CoolTimeDown(SkillData skillData)
        {
            //冷却开始时，通过冷却时间得到冷却剩余时间
            skillData.coolRemain = skillData.coolTime;
            //通过循环，进行冷却倒计时，直到coolRemain = 0
            while (skillData.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                skillData.coolRemain -= 1;
            }
            skillData.coolRemain = 0;//为了由于精度导致结果不为0的误差，保险起见手动为0
        }
        //5.获取技能冷却剩余时间
        public int GetSkillCoolRemain(int id)
        {
            return skills.Find(s => s.skillID == id).coolRemain;
        }
    }
}
