using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 施放器的初始化工厂类 = 施放器的配置工厂类
    /// </summary>
    class DeployerConfigFactor
    {
        public static IAttackSelector CreatAttackSelector(SkillData skill)
        {
            IAttackSelector attackSelector = null;
            switch (skill.damageMode)
            {
                case DamageMode.Sector:
                    attackSelector = new SectorAttackSelector();
                    break;
                case DamageMode.Circle:
                    attackSelector = new CircleAttackSelector();
                    break;
            }

            return attackSelector;
        }
        /// <summary>
        /// 初始化自身影响
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public static List<ISelfImpact> CreatSelfImpact(SkillData skill)
        {
            List<ISelfImpact> list = new List<ISelfImpact>();
            list.Add(new CostSPSelfImpact());
            return list;
        }
        /// <summary>
        /// 初始化目标影响
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public static List<ITargetImpact> CreatTargetImpact(SkillData skill)
        {
            List<ITargetImpact> list = new List<ITargetImpact>();
            list.Add(new DamageTargetImpact());
            return list;
        }
    }
}
