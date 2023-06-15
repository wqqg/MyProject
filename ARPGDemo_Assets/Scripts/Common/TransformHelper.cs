using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class TransformHelper : MonoBehaviour
{

    //�ҵ�������
    public static Transform FindChild(Transform parent,string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }
            else
            {
                Transform foundChild = FindChild(child, childName);
                if (foundChild != null)
                {
                    return foundChild;
                }
            }
        }
        return null;
    }
    
    /// <summary>
    /// ת��
    /// </summary>
    public static void LookAtTarget(Vector3 target,Transform transform,float rotationSpeed)
    {
        if (target != Vector3.zero)
        {
            Quaternion dir = Quaternion.LookRotation(target);
            transform.rotation = Quaternion.Lerp(transform.rotation, dir, rotationSpeed);
        }
    }
}
