using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Transform[] wayPoints;

    public Transform Current => wayPoints[0];

    private void Awake()
    {
        // ��� �ڽ��� ��������Ʈ�� ���
        wayPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
    }
}
