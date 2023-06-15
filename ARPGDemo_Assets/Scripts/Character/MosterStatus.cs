using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace ARPGDemo.Character
{
    /// <summary>
    /// 怪物状态
    /// </summary>
    public class MosterStatus : CharacterStatus, IOnDamager
    {
        /// <summary>
        /// 贡献经验
        /// </summary>
        public int GiveExp;
        public override void Dead()
        {
            print("小怪死");
        }
        public override void OnDamage(int damageVal)
        {
            base.OnDamage(damageVal);
            //print("受到伤害");
        }
    }
}