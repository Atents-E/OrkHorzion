using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Crossbow_Projectile 기능

// 1. 적의 체력을 저하시킨다

public class Crossbow_Projectile : ProjectileBase
{
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
