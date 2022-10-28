using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Transform[] wayPoints;

    public Transform Current => wayPoints[0];

    private void Awake()
    {
        // 모든 자식을 웨이포인트로 사용
        wayPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
    }
}
