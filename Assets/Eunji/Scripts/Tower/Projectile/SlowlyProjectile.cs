using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlowlyProjectile : ProjectileBase
{
    public float reduceSpeed = 0.003f;    // 감속 시키는 스피드
    public float reduceAttack = 0.003f;    // 감속 시키는 공격력
    public float reduceTiem = 5.0f;	    // 감속 시키는 시간 

    bool isReduceTime = false;

    protected void OnCollisionEnter(Collision collision) // 충돌이 일어나면
    {
        if (collision.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
        {
            // target은 몬스터
            target = collision.gameObject;

            // 발사체 삭제
            Destroy(this.gameObject);

            MonsterBase monster = GetComponent<MonsterBase>();
 
            for(int i = 0; i < reduceTiem; i++)     // 감속시간 동안
            {
                // Monster의 이속과 공속 감소
                reduceSpeed -= Time.deltaTime;
                isReduceTime = false;
            }

            isReduceTime = true;
            monster.speed -= reduceSpeed;
            monster.attackSpeed -= reduceAttack;
        }
    }

}
