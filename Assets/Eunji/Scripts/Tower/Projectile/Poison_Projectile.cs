using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Poison_Projectile 기능

// 1. 디버프 효과

public class Poison_Projectile : ProjectileBase
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
        {
            monster = other.GetComponent<MonsterBase>();

            Debuff();   // 디퍼프 함수 실행

            Destroy(this.gameObject);
        }
    }
}
