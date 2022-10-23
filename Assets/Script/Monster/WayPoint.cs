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
        // 모든 자식을 웨이포인트로 사용
        wayPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
    }

    public Transform MoveNext()
    {
        index++;                        // 1증가 시키고

        return wayPoints[index];
    }
}
