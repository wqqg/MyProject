using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
namespace ARPGDemo.Skill
{
    //序列化标记，若子类调用该类，可以在unity属性窗口访问这个类的字段
    [Serializable]
    public class SkillData
    {
        ///技能ID
        public int skillID;
        ///技能名称
        public string name;
        ///技能描述
        public string description;
        ///冷却时间
        public int coolTime;
        ///冷却剩余
        public int coolRemain;
        ///魔法消耗
        public int costSP;
        ///攻击距离
        public float attackDistance;
        ///攻击角度
        public float attackAngle;
        ///攻击目标tags
        public string[] attackTargetTags = { "Enemy","Boss"};
        //有些复杂类型，不适合在属性窗口赋值（一般在start中赋值，没必要在属性窗口出现）
        //若改成private不出现，但是也无法在其他类中访问。
        //希望：不在属性窗口出现，但在其他类中可以访问，加[HideInInspector]、public
        [HideInInspector]
        ///攻击目标对象数组
        public GameObject[] attackTargets;
        ///连击的下一个技能编号
        public int nextBatterld;
        ///伤害比例
        public float damage;
        ///持续时间
        public float durationTime;
        ///伤害间隔
        public float damageInterval;
        [HideInInspector]
        ///技能所属
        public GameObject Onwer;
        ///技能预制件名称
        public string perfabName;
        [HideInInspector]
        ///技能预制件对象
        public GameObject skillPerfab;
        ///动画名称
        public string animationName;
        ///受击特效名称
        public string hitFxName;
        [HideInInspector]
        ///受击特效预制件
        public GameObject hitFxPrefab;
        ///等级
        public int level;
        ///是否激活
        public bool activated;
        ///攻击类型 单攻，群攻
        public SkillAttackType attackType;
        ///伤害模式 圆形，扇形，矩形
        public DamageMode damageMode;
    }
}
