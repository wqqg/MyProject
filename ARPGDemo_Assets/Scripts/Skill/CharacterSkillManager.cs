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
        //�ֶΡ��������������ݶ���--����List<SkillData>���м���
        public List<SkillData> skills = new List<SkillData>();
        //������
        //1.��ʼ��
        void Start()
        {
            //������ͨ�����Դ���ֱ�Ӹ�ֵ����ʼ���������ͣ�ͨ�����븳ֵ��
            foreach (var skill in skills) 
            {
                //���skill.perfabName���������֣��ַ�����Ϊnull�򡰡�ʱִ��
                if (!(string.IsNullOrEmpty(skill.perfabName))&&skill.skillPerfab==null)
                {
                    skill.skillPerfab = LoadPrefab(skill.perfabName);
                }
                if (!(string.IsNullOrEmpty(skill.hitFxName)) && skill.hitFxPrefab == null)
                {
                    skill.hitFxPrefab = LoadPrefab(skill.hitFxName);
                }
                //Ϊ����ָ��ӵ����
                skill.Onwer = this.gameObject;
            }
        }
        //����Ԥ�Ƽ���Դ����resName����̬����Ԥ�Ƽ���Դ
        public GameObject LoadPrefab(string resName)
        {
            //��̬����Ԥ�Ƽ���Դ
            var prefabGo = Resources.Load<GameObject>(resName);

            //ʹ����Ϸ����� ��ʼ����׼���ü��ܣ���ֹ��һ��ʹ�ü���ʱ���ֿ�������
            //�����ö���أ����ܿ��٣����Բ���
            var tempGo = GameObjectPool.instance.CreateObject(resName, prefabGo, this.transform.position, transform.rotation);
            //����������������壬��Ϊ�ǳ�ʼ�������Բ���Ҫ�����ڳ�����,�����Ҫ˲����ս�����
            GameObjectPool.instance.CollectObject(tempGo);

            return prefabGo;
        }
        //2.׼������
        public SkillData PrepareSkill(int id)
        {
            //1.���ݼ���id�Ҽ����������Ƿ����������
            var skill = skills.Find(skill => skill.skillID == id);
            //2.���ҵ���ͬʱ�����Ѿ���ȴ����SPֵ�㹻������
            if (skill != null)
            {
                if (skill.coolRemain == 0&& skill.costSP <= skill.Onwer.GetComponent<CharacterStatus>().SP)
                {
                    return skill;
                }
            }
            return null;
        }
        //3.ʩ�ż��� ����ʩ�������ͷŵķ���
        public void DeploySkill(SkillData skillData)
        {
            //1.��������Ԥ�Ƽ����� ����ش���
            var tempGo = GameObjectPool.instance.CreateObject(skillData.perfabName, skillData.skillPerfab, this.transform.position, transform.rotation);
            //2.Ϊ����Ԥ�Ƽ��������õ�ǰҪʹ�õļ���
            var deployer = tempGo.GetComponent<SkillDeployer>();
            //3.����ʩ������ʩ�ŷ���
            deployer.skillData = skillData;
            deployer.DeploySkill();
            //4.��ȴ��ʱ
            StartCoroutine(CoolTimeDown(skillData));
            //���ܶ�����Ҫ���� ����ʩ�������

        }
        //4.������ȴ����
        private IEnumerator CoolTimeDown(SkillData skillData)
        {
            //��ȴ��ʼʱ��ͨ����ȴʱ��õ���ȴʣ��ʱ��
            skillData.coolRemain = skillData.coolTime;
            //ͨ��ѭ����������ȴ����ʱ��ֱ��coolRemain = 0
            while (skillData.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                skillData.coolRemain -= 1;
            }
            skillData.coolRemain = 0;//Ϊ�����ھ��ȵ��½����Ϊ0������������ֶ�Ϊ0
        }
        //5.��ȡ������ȴʣ��ʱ��
        public int GetSkillCoolRemain(int id)
        {
            return skills.Find(s => s.skillID == id).coolRemain;
        }
    }
}
