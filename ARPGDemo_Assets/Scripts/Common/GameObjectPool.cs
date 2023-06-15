using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��Ϸ�����
/// �ռ任ȡʱ��ļ�����������߳��������
/// </summary>
public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    private GameObjectPool() { }
    //1 ������
    private Dictionary<string,List<GameObject>> cache = new Dictionary<string, List<GameObject>> ();
    //2 ����һ������ʹ�ö���:�����У��ӳ��з��أ�����û�У����ط�������ٷ���
    //�磺pool.CreateObject(�ӵ�,position,rotation);
    public GameObject CreateObject(string key,GameObject go,Vector3 position,Quaternion quaternion)
    {
        //1 ���ҳ������޿�����Ϸ����
        GameObject tempGo = FindUsable(key);
        //2 �����У��ӳ��з���
        if (tempGo != null)
        {
            tempGo.transform.position = position;
            tempGo.transform.rotation = quaternion;
            tempGo.SetActive(true);//��ʾ��ǰ�����Ϸ������ʹ����
        }
        else//3 ����û�У����ط�������ٷ���
        {
            tempGo = Instantiate(go, position, quaternion);
            //�������
            Add(key, tempGo);
        }
        //��Ϊ�������������
        tempGo.transform.parent = this.transform;
        return tempGo;
    }
    private GameObject FindUsable(string key)
    {
        if (cache.ContainsKey(key))
        {
            //���б����ҳ�δ�����Ϸ����
            return cache[key].Find(p => !p.activeSelf);
        }
        return null;
    }
    private void Add(string key,GameObject go)
    {
        //�ȼ������Ƿ�����Ҫ�ļ���û�У���Ҫ�ȴ���key�Ͷ�Ӧ���б�
        if (!cache.ContainsKey(key))
        {
            cache.Add(key, new List<GameObject>());
        }
        //����Ϸ������ӵ�����
        cache[key].Add(go);
    }
    //3 �ͷ���Դ���ӳ���ɾ������
    //3.1 �ͷŲ��֣���Key�ͷ�
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
    //3.2 �ͷ�ȫ��,ѭ������Clear(string key)
    public void ClearAll()
    {
        List<string> keys = new List<string>(cache.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            Clear(keys[i]);
        }
    }
    //4 ���ն���ʹ������󷵻س���[�ӻ�������ʧ]
    //4.1 ��ʱ���ն���
    public void CollectObject(GameObject go)
    {
        go.SetActive(false);//������ʧ
    }
    //4.2 ��ʱ���ն��󣬵ȴ�һ��ʱ�䣨Э�̣�
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
