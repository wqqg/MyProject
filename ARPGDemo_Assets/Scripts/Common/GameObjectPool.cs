using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 游戏对象池
/// 空间换取时间的技术，可以提高程序的性能
/// </summary>
public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    private GameObjectPool() { }
    //1 创建池
    private Dictionary<string,List<GameObject>> cache = new Dictionary<string, List<GameObject>> ();
    //2 创建一个对象并使用对象:池中有，从池中返回；池中没有，加载放入池中再返回
    //如：pool.CreateObject(子弹,position,rotation);
    public GameObject CreateObject(string key,GameObject go,Vector3 position,Quaternion quaternion)
    {
        //1 查找池中有无可用游戏对象
        GameObject tempGo = FindUsable(key);
        //2 池中有，从池中返回
        if (tempGo != null)
        {
            tempGo.transform.position = position;
            tempGo.transform.rotation = quaternion;
            tempGo.SetActive(true);//表示当前这个游戏对象在使用中
        }
        else//3 池中没有，加载放入池中再返回
        {
            tempGo = Instantiate(go, position, quaternion);
            //放入池中
            Add(key, tempGo);
        }
        //作为池物体的子物体
        tempGo.transform.parent = this.transform;
        return tempGo;
    }
    private GameObject FindUsable(string key)
    {
        if (cache.ContainsKey(key))
        {
            //从列表中找出未活动的游戏物体
            return cache[key].Find(p => !p.activeSelf);
        }
        return null;
    }
    private void Add(string key,GameObject go)
    {
        //先检查池中是否有需要的键，没有，需要先创建key和对应的列表
        if (!cache.ContainsKey(key))
        {
            cache.Add(key, new List<GameObject>());
        }
        //把游戏对象添加到池中
        cache[key].Add(go);
    }
    //3 释放资源：从池中删除对象
    //3.1 释放部分：按Key释放
    public void Clear(string key)
    {
        if (cache.ContainsKey(key))
        {
            for (int i = 0; i < cache[key].Count; i++)
            {
                Destroy(cache[key][i]);
            }
            cache.Remove(key);
        }
    }
    //3.2 释放全部,循环调用Clear(string key)
    public void ClearAll()
    {
        List<string> keys = new List<string>(cache.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            Clear(keys[i]);
        }
    }
    //4 回收对象：使用完对象返回池中[从画面中消失]
    //4.1 即时回收对象
    public void CollectObject(GameObject go)
    {
        go.SetActive(false);//画面消失
    }
    //4.2 延时回收对象，等待一定时间（协程）
    public void CollectObject(GameObject go, float delay)
    {
        StartCoroutine(CollectDelay(go, delay));
    }
    private IEnumerator CollectDelay(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        CollectObject(go);
    }

}
