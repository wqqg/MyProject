using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate bool FindHandler<T>(T t);
//public delegate TKey SelectHandler<T, TKey>(T t);
public static class HelperFind
{
    /// <summary>
    /// 查找某一个的方法Find
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责从某个类型中选取某个字段，返回这个字段的值
    /// </param>
    static public T Find<T>(T[]array,FindHandler<T> handler)
    {
        T temp = default(T);
        for (int i = 0; i < array.Length; i++)
        {
            if (handler(array[i]))
            {
                return array[i];
            }
        }
        return temp;
    }
    /// <summary>
    /// 查找所有的方法FindAll
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责从某个类型中选取某个字段，返回这个字段的值
    /// </param>
    static public T[] FindAll<T>(T[] array, FindHandler<T> handler)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < array.Length; i++)
        {
            if (handler(array[i]))
            {
                list.Add(array[i]);
            }
        }
        return list.ToArray();
    }
    /// <summary>
    /// 选择：选取数组中对象的某些成员形成一个独立的数组
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责从某个类型中选取某个字段，返回这个字段的值
    /// </param>
    //多个学生【id age tall name】   【1,2,3】
    //                              【“zs”，“ls”，“ww”】
    static public TKey[] Select<T,TKey>(T[] array, SelectHandler<T,TKey> handler)
    {
        TKey[] keys = new TKey[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            keys[i] = handler(array[i]);
        }
        return keys;
    }
}
