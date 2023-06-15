using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class TestArrayHelper : MonoBehaviour
{
    
    void Start()
    {
        //FindHPMax();
        //FindHPGreaterThan20();
        FindDistanceMin();
    }
    private void FindHPMax()
    {
        //根据tag找到所有敌人
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        //找HP最大的
        var enemy = ArrayHelper.Max(gos, go => go.GetComponent<EnemyHealth>().HP);
        //变成黄色
        enemy.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
    private void FindHPGreaterThan20()
    {
        //根据tag找到所有敌人
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        //找HP大于20的
        var enemy = HelperFind.FindAll(gos, go => go.GetComponent<EnemyHealth>().HP > 20);
        //变成蓝色
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
    private void FindDistanceMin()
    {
        //根据tag找到所有敌人
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        //找到距离最近的
        var enemy = ArrayHelper.Min(gos, go => 
        Vector3.Distance(this.transform.position, go.transform.position));
        //变成红色
        enemy.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
