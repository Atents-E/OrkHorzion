using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public float projectileSpeed = 0.3f;      // 투사체의 속도
    public float projectileCreate = 0.1f;     // 투사체의 생성시간


    Transform projectile;                       // 투사체(투사체의 현재 위치)
    Transform target = null;                    // 투사체의 타겟(투사체가 도착 할 지점)

    GameObject projentile;

    private void Awake()
    {
        projectile = GetComponent<Transform>();     // 움직일 투사체를 받아옴
    }


    private void FixedUpdate()
    {
        if (target != null)              // target이 있다면
        {
            Vector3 dir = target.position - projectile.position;      // Player에게 가는 방향벡터
            dir.y = 0;

            projectile.position = dir * (projectileSpeed * Time.fixedDeltaTime);   // 투사체는 Playr를 향해 날라가라

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            target = other.transform;
            if (projectile != null)
            {
                //Destroy(projentile);                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            target = null;
        }
    }
 

}
