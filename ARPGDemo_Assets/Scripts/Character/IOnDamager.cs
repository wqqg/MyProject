using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace ARPGDemo.Character
{
    public interface IOnDamager
    {
        void OnDamage(int damageVal);
    }
}