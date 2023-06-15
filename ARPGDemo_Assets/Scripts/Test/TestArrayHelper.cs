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
        //����tag�ҵ����е���
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        //��HP����
        var enemy = ArrayHelper.Max(gos, go => go.GetComponent<EnemyHealth>().HP);
        //��ɻ�ɫ
        enemy.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
    private void FindHPGreaterThan20()
    {
        //����tag�ҵ����е���
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        //��HP����20��
        var enemy = HelperFind.FindAll(gos, go => go.GetComponent<EnemyHealth>().HP > 20);
        //�����ɫ
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
    private void FindDistanceMin()
    {
        //����tag�ҵ����е���
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        //�ҵ����������
        var enemy = ArrayHelper.Min(gos, go => 
        Vector3.Distance(this.transform.position, go.transform.position));
        //��ɺ�ɫ
        enemy.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
