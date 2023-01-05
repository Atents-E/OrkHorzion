using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 투사체의 생성을 조절
public class Direction_0000 : MonoBehaviour
{

    public float projectileSpeed = 0.3f;    // 투사체의 속도
    public float projectileCreate = 0.1f;   // 투사체의 생성시간

    Transform projectile;                   // 투사체(투사체의 현재 위치)
    Transform target = null;                // 투사체의 타겟(투사체가 도착 할 지점)


    private void Awake()
    {
        projectile = GetComponent<Transform>(); // 움직일 투사체를 받아옴
    }


    private void FixedUpdate()
    {
        if (target != null )                                   // target이 있고, 투사체도 생성 되었다면
        {
            Vector3 dir = target.position - projectile.position;                    // Enemy에게 가는 방향벡터
            dir.y = 0;

            projectile.position = dir * (projectileSpeed * Time.fixedDeltaTime);    // 투사체는 Enemy을 향해 이동
        }
    }

    //private void OnTriggerEnter(Collider other) // other이 트리거와 만나면
    //{   
    //    if (other.gameObject.CompareTag("Enemy"))          // other이 태그 Enemy라면
    //    {
    //        target = other.gameObject;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
    //    {
    //        // target은 other
    //        target = other.gameObject;

    //        // Enemey의 HP는 감소
    //        MonsterBase monster = GetComponent<MonsterBase>();
    //        monster.monsterHp -= attackPower;
    //    }
    //    Destroy(this.gameObject); // 발사체 삭제

    //}

    private void OnTriggerExit(Collider other)  // other이 트리거에서 나가면
    {
        if (other.CompareTag("Enemy"))          // other이 태그 Enemy라면
        {
            target = null;                      // target는 없다
        }
    }
 

}
