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
    protected float attackPower = 10.0f;        // 투사체 공격력
    protected float speed = 2.0f;               // 투사체 속도
    protected float lifeTime = 2.0f;            // 투사체 유지 시간

    protected float reduceSpeed = 0.003f;       // 감속 시키는 스피드
    protected float reduceAttack = 0.003f;      // 감속 시키는 공격력
    protected float holdTiem = 5.0f;            // 감속 시간 

    protected MonsterBase monster;                // 적


    protected virtual void Awake()
    {
        Destroy(this.gameObject, lifeTime);         // 생성되면 lifeTime 뒤에 삭제
    }

    protected void Update()
    {
        transform.Translate(speed * Time.deltaTime * transform.forward, Space.World);
    }

    //// 01.12 정민님 코드(인터페이스 사용)
    //protected virtual void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
    //    {
    //        IBattle target = other.gameObject.GetComponent<IBattle>();
    //        Attack(target);


    //        Destroy(this.gameObject); // 적과 충돌하면 발사체 삭제
    //    }
    //}

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
        {
            // 1. monster의 체력이 있는 컴포넌트를 가져오기
            monster = other.GetComponent<MonsterBase>();

            // 2. 발사체 삭제
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 체력 감소
    /// </summary>
    protected void Damage()
    {
        // 몬스터가 있으면
        if (monster != null)
        {
            // 몬스터 체력을 지역 변수로 선언
            float MonsterHp = monster.MonsterHp;
            
            if (MonsterHp > 0)
            {
                MonsterHp -= attackPower;
                if (MonsterHp < 0)
                {
                    MonsterHp = 0;
                }
            }
            Debug.Log($"몬스터의 현재 체력 : {MonsterHp}");
        }
    }

    /// <summary>
    /// 디버프 효과
    /// </summary>
    protected void Debuff()
    {
        // 몬스터가 있으면
        if(monster != null)
        {
            // 몬스터의 처음 이속, 공속을 저장
            float currentSpeed = monster.speed;
            float currentAttackSpeed = monster.attackSpeed;

            if (IsDebuffTime())  // 감속 유지 시간 동안 이속 공속 감속
            {
                // Monster의 이속과 공속 감소
                monster.speed -= reduceSpeed;
                monster.attackSpeed -= reduceAttack;
                
                Debug.Log($" 이속 저하. 현재 이도  : {monster.speed}");
                Debug.Log($" 공속 저하. 현재 공도: {monster.attackSpeed}");
            }
            else
            {
                // 시간이 끝나면 몬스터의 이속과 공속 원상복구
                monster.speed = currentSpeed;
                monster.attackSpeed = currentAttackSpeed;
            }
        }
    }


    /// <summary>
    /// 유지 시간 확인
    /// </summary>
    /// <returns>true면 공격이 지속되고, flase면 끝</returns>
    bool IsDebuffTime()
    {
        // 감속시간이 아니다
        bool holdTime = false;

        for(int i = 0; i < holdTiem; i++)     // 감속 시간
        {
            // 감속 유지
            holdTime = true;

            // 감속시간이 시간에 따라 줄어듬
            holdTiem -= Time.deltaTime;
        }

        return holdTime;
    }
}