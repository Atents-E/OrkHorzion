using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Poison_Projectile 기능

// 1. 디버프 효과

public class Poison_Projectile : ProjectileBase
{
    float newSpeed = 2.0f;              // 투사체 속도
    float newLifeTime = 2.0f;           // 투사체 유지 시간
    float newReduceSpeed = 0.003f;      // 감속 시키는 스피드
    float newReduceAttack = 0.003f;     // 감속 시키는 공격력
    float newHoldTiem = 5.0f;           // 감속 시간 

    protected override void Awake()
    {
        speed = newSpeed;
        lifeTime = newLifeTime;
        reduceSpeed = newReduceSpeed;
        reduceAttack = newReduceAttack;
        holdTiem = newHoldTiem;

        base.Awake();
    }

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
