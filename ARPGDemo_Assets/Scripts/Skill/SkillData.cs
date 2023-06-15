using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
namespace ARPGDemo.Skill
{
    //���л���ǣ���������ø��࣬������unity���Դ��ڷ����������ֶ�
    [Serializable]
    public class SkillData
    {
        ///����ID
        public int skillID;
        ///��������
        public string name;
        ///��������
        public string description;
        ///��ȴʱ��
        public int coolTime;
        ///��ȴʣ��
        public int coolRemain;
        ///ħ������
        public int costSP;
        ///��������
        public float attackDistance;
        ///�����Ƕ�
        public float attackAngle;
        ///����Ŀ��tags
        public string[] attackTargetTags = { "Enemy","Boss"};
        //��Щ�������ͣ����ʺ������Դ��ڸ�ֵ��һ����start�и�ֵ��û��Ҫ�����Դ��ڳ��֣�
        //���ĳ�private�����֣�����Ҳ�޷����������з��ʡ�
        //ϣ�����������Դ��ڳ��֣������������п��Է��ʣ���[HideInInspector]��public
        [HideInInspector]
        ///����Ŀ���������
        public GameObject[] attackTargets;
        ///��������һ�����ܱ��
        public int nextBatterld;
        ///�˺�����
        public float damage;
        ///����ʱ��
        public float durationTime;
        ///�˺����
        public float damageInterval;
        [HideInInspector]
        ///��������
        public GameObject Onwer;
        ///����Ԥ�Ƽ�����
        public string perfabName;
        [HideInInspector]
        ///����Ԥ�Ƽ�����
        public GameObject skillPerfab;
        ///��������
        public string animationName;
        ///�ܻ���Ч����
        public string hitFxName;
        [HideInInspector]
        ///�ܻ���ЧԤ�Ƽ�
        public GameObject hitFxPrefab;
        ///�ȼ�
        public int level;
        ///�Ƿ񼤻�
        public bool activated;
        ///�������� ������Ⱥ��
        public SkillAttackType attackType;
        ///�˺�ģʽ Բ�Σ����Σ�����
        public DamageMode damageMode;
    }
}
