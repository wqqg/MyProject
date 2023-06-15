using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
namespace ARPGDemo.Character
{
    /// <summary>
    /// 动画事件行为
    /// </summary>
    public class AnimationEventBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 动画组件
        /// </summary>
        private Animator anim;
        private void Start()
        {
            anim = GetComponent<Animator>();

        }
        /// <summary>
        /// 撤销动画播放
        /// </summary>
        public void OnCancelAnim(string animName)
        {
            anim.SetBool(animName, false);
        }
        //定义委托：数据类型嵌套
        public delegate void AttackHandler();
        //1.定义事件：使用事件设计模式：：步骤 定义事件名称=声明委托对象
        public event AttackHandler attackHandler;
        //3.触发事件
        /// <summary>
        /// 攻击时使用
        /// </summary>
        public void OnAttack()
        {
            if (attackHandler != null)
            {
                attackHandler();
            }
        }
    } 
}