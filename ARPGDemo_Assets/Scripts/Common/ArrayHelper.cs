using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//从T类中取一个属性【Age,Tall...】 返回这个属性的值
public delegate TKey SelectHandler<T,TKey>(T t);
public static class ArrayHelper
{
    /// <summary>
    /// 升序排序
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责从某个类型中选取某个字段，返回这个字段的值
    /// </param>
    static public void OrderBy1<T>(T[] array) where T : IComparable<T>
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[i].CompareTo(array[j]) > 0)
                {
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
    }

    /// <summary>
    /// 降序排序
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责从某个类型中选取某个字段，返回这个字段的值
    /// </param>
    static public void OrderBy2<T>(T[] array) where T : IComparable<T>
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[i].CompareTo(array[j]) < 0)
                {
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
    }

    /// <summary>
    /// 返回最大值
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责从某个类型中选取某个字段，返回这个字段的值
    /// </param>
    static public T Max<T, TKey>(T[] array, SelectHandler<T, TKey> handler) where TKey : IComparable<TKey>
    {
        T temp = default(T);
        temp = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (handler(temp).CompareTo(handler(array[i])) < 0)
            {
                temp = array[i];
            }
        }
        return temp;
    }

    /// <summary>
    /// 返回最小值
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责从某个类型中选取某个字段，返回这个字段的值
    /// </param>
    static public T Min<T, TKey>(T[] array, SelectHandler<T, TKey> handler) where TKey : IComparable<TKey>
    {
        T temp = default(T);
        temp = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (handler(temp).CompareTo(handler(array[i])) > 0)
            {
                temp = array[i];
            }
        }
        return temp;
    }

    static public void OrderBy3<T>(T[] array, IComparer<T> Compare) where T : IComparable<T>
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (Compare.Compare(array[i], array[j]) > 0)
                {
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
    }

    static public void OrderBy4<T,TKey>(T[] array, SelectHandler<T,TKey> handler)
        where TKey : IComparable<TKey>
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (handler(array[i]).CompareTo(handler(array[j]))>0)
                {
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
    }
}
