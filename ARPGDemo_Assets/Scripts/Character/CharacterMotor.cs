using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace ARPGDemo.Character
{
    /// <summary>
    /// 角色马达
    /// </summary>
    public class CharacterMotor:MonoBehaviour
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        public float moveSpeed = 5;
        /// <summary>
        /// 转向速度
        /// </summary>
        public float rotationSpeed = 10;
        /// <summary>
        /// 动画系统
        /// </summary>
        public CharacterAnimation chAnim;
        /// <summary>
        /// 角色控制器
        /// </summary>
        public CharacterController chController;
        private void Start()
        {
            chAnim = GetComponent<CharacterAnimation>();
            chController = GetComponent<CharacterController>();
        }
        /// <summary>
        /// 移动
        /// </summary>
        public void Move(float x,float z)
        {
            if (x != 0 || z != 0)
            {
                //1.转向 的方向
                TransformHelper.LookAtTarget(new Vector3(x, 0, z), transform, rotationSpeed);
                //2.向目标运动：核心调用内置组件移动的方法（角色控制器的运动方法)
                //-1表示，模拟重力：保证角色贴在地面上，不会飘起来
                Vector3 motion = new Vector3(transform.forward.x, -1, transform.forward.z);
                chController.Move(motion * Time.deltaTime * moveSpeed);
                //3.播放运动动画
                chAnim.PlayAnimation("run");
            }
            else
            {
                chAnim.PlayAnimation("idle");
            }
        }
    }
}