using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shot : MonoBehaviour
{
    /// <summary>
    /// 총알의 공격력
    /// </summary>
    public float attack = 10.0f;

    public float shotSpeed = 0.3f;      // 총알 속도
    public float shotCreate = 0.1f;     // 총알의 생성시간
    public Action onDie { get; set; }

    Transform target = null;            // 총알의 타겟(총알이 도착 할 지점)
    Transform shot;                     // 총알(총알의 현재 위치)

    private void Awake()
    {
        shot = transform.GetChild(1).transform.GetChild(0);     // 총알의 위치 받아옴
    }


    private void FixedUpdate()
    {
        if (target != null)              // target이 있다면
        {
            Vector3 dir = target.position - shot.position;      // Player에게 가는 방향벡터
            dir.y = 0;

            shot.position = dir * (shotSpeed * Time.fixedDeltaTime);   // 총알은 Playr를 향해 날라가라

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            target = other.transform;
            if (shot != null)
            {
                Destroy(shot);                      // Player와 만나면 shot은 사라진다(shot를 못 찾는거 같음
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            target = null;
        }
    }


    //public void Die()       // IDead 인터페이스 상속
    //{
    //}
}
