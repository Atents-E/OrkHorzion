using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cannon_Projectile 기능

// 1. 적의 체력을 저하시킨다

public class Cannon_Projectile : ProjectileBase
{
    float newAttackPower = 10.0f;   // 투사체 공격력
    float newSpeed = 2.0f;          // 투사체 속도
    float newLifeTime = 2.0f;       // 투사체 유지 시간

    protected override void Awake() 
    {
        attackPower = newAttackPower;
        speed = newSpeed;
        lifeTime = newLifeTime;

        base.Awake();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
        {
            monster = other.GetComponent<MonsterBase>();

            Damage();   // 적의 체력을 저하시키는 함수 실행

            Destroy(this.gameObject);
        }
    }
}
