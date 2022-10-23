using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    Transform[] wayPoints;
    int index = 0;

    public Transform Current => wayPoints[index];

    private void Awake()
    {
        // ��� �ڽ��� ��������Ʈ�� ���
        wayPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
    }

    public Transform MoveNext()
    {
        index++;                        // 1���� ��Ű��

        return wayPoints[index];
    }
}
