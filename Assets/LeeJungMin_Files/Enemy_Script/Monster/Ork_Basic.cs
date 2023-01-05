using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ork_Basic : EnemyBase
{
    Animator anim;
    Enemy_Navigation nav;
    CapsuleCollider ccoll;

    EnemySpawner spawn;

    bool isDead = true;
    bool isAlive = true;

    float testTime = 0.0f;

    IBattle target;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();        // 애니메이터
        nav = GetComponent<Enemy_Navigation>();
        ccoll = GetComponent<CapsuleCollider>();

        spawn = FindObjectOfType<EnemySpawner>();
    }

    protected virtual void Start()
    {
        SearchPlayer();         // 플레이어 찾기

        MaxHP += spawn.WaveCount * 500.0f;
        HP = MaxHP;

        onHealthChange += HP_Change;   
        onDie += Dead;
    }

    
    protected virtual void Update()
    {
        //HP -= testHpCount;    // 프레임마다 testHpCount만큼 HP를 줄인다
        testTime += Time.deltaTime;
               

        Looktarget();           // 플레이어쪽을 쳐다본다.
    }


    private void OnTriggerEnter(Collider other)     // 트리거 안에 들어왔을 때
    {
        target = other.GetComponent<IBattle>();
        if (other.CompareTag("Player") && isAlive)
        {
            //Debug.Log("들어옴");
            looktargetOn = true;
            playerTarget = other.transform;         // playerTarget이 null이 아니게 되었다.
            anim.SetBool("Attack", true);
            //Attack(target);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && target != null && isAlive)
        {
            if (target != null && testTime > 1)
            {
                //Debug.Log("플레이어 공격");
                Attack(target);
                testTime = 0.0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)      // 트리거를 빠져 나갔을 때
    {        
        if (other.CompareTag("Player"))
        {
            target = null;
            //Debug.Log("나감");
            playerTarget = null;                    // playerTarget이 null이 되었다.
            looktargetOn = false;
            anim.SetBool("Attack", false);
        }
    }

    protected virtual void Dead()
    {
        if (isDead)
        {
            isAlive = false;
            isDead = false;
            nav.Stopped(true);
            anim.SetBool("Attack", false);
            anim.SetTrigger("Die");
            ccoll.enabled = false;
            nav.enabled = false;
            nav.IsEnabled();
            Destroy(this.gameObject, 2.0f);
            //Debug.Log("오크 죽음");
        }
    }

    void HP_Change(float ratio)
    {
        if (isDead)
        {            
            //Debug.Log($"{gameObject.name}의 HP가 {HP}로 변경 되었습니다.");
        }
    }

    
}
