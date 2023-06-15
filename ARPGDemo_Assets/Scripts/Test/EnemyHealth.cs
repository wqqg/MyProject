using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public int HP;
    public int SP;
    void Awake()
    {
        HP = Random.Range(0, 50);
    }

    void Update()
    {
        
    }
}
