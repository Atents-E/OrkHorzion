using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using System;

// ProjectileBase 기능

// 1. 생성되면 타겟 방향으로날아감 
// 2. 타겟과 만다면 특정 영향을 타겟에게 미침
    // 2.1. 타겟의 체력을 공격력만큼 깍는다.
    // 2.2. 타겟에게 디버프.
// 3. 생성되고 lifeTime 뒤에 자동 삭제
// 4. 타겟과 만나면 즉시 삭제된다

public class ProjectileBase : MonoBehaviour
{
    public float attackPower = 100.0f;        // 투사체 공격력
    public float speed = 2.0f;               // 투사체 속도
    public float lifeTime = 2.0f;            // 투사체 유지 시간

    public float reduceSpeed = 0.003f;       // 감속 시키는 스피드
    public float reduceAttack = 0.003f;      // 감속 시키는 공격력
    public float holdTime = 5.0f;            // 감속 시간 
    protected float currentAttackSpeed;

    protected EnemyBase enemy;                // 적
    DamageText enemyAttackSpeed;

    protected virtual void Awake()
    {

        Destroy(this.gameObject, lifeTime);         // 생성되면 lifeTime 뒤에 삭제
    }

    protected void Update()
    {
        transform.Translate(speed * Time.deltaTime * transform.forward, Space.World);
    }

    IBattle target;


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
        {
            // 1. monster의 체력이 있는 컴포넌트를 가져오기
            //monster = other.GetComponent<MonsterBase>();
            target = other.gameObject.GetComponent<IBattle>();

            target.TakeDamage(attackPower);


            //enemyAttackSpeed = enemy.GetComponent<DamageText>();
            //currentAttackSpeed = enemyAttackSpeed.moveSpeed;

            // 2. 발사체 삭제
            Destroy(this.gameObject);
        }
    }

    ///// <summary>
    ///// 체력 감소
    ///// </summary>
    //protected void Damage()
    //{
    //    // 몬스터가 있으면
    //    if (monster != null)
    //    {
    //        // 몬스터 체력을 지역 변수로 선언
    //        float MonsterHp = monster.MonsterHp;
            
    //        if (MonsterHp > 0)
    //        {
    //            MonsterHp -= attackPower;
    //            if (MonsterHp < 0)
    //            {
    //                MonsterHp = 0;
    //            }
    //        }
    //        Debug.Log($"몬스터의 현재 체력 : {MonsterHp}");
    //    }
    //}

    /// <summary>
    /// 디버프 효과
    /// </summary>
    protected void Debuff()
    {
        // 몬스터가 있으면
        if(enemy != null)
        {
            StartCoroutine(IsEdbuffTime());
        }
    }


    /// <summary>
    /// 유지 시간 확인
    /// </summary>
    IEnumerator IsEdbuffTime()
    {
        enemyAttackSpeed.moveSpeed -= reduceSpeed;
        yield return new WaitForSeconds(holdTime);

        enemyAttackSpeed.moveSpeed -= reduceSpeed;
        yield return null;
    }

}