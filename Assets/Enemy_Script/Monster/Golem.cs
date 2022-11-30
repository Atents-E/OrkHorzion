using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Golem : EnemyBase
{
    public GameObject Rock;

    public float waitTime = 2.0f;       // 목적지에 도착했을 때 기다리는 시간
    float waitTimer = 0;                // 남아있는 기다려야 하는 시간 

    public float runWaitTime = 5.0f;   // 목적지에 도착했을 때 기다리는 시간
    float runWaitTimer = 0;            // 남아있는 기다려야 하는 시간 
    public float testHpCount =5;
    bool isDead = true;
    bool isTargetOn = false;

    public Transform baseTarget;
    Animator anim;
    Enemy_Navigation nav;

    CapsuleCollider coll;

    GolemState state = GolemState.Attack;  // 현재 골렘의 상태

    Transform rock_Spawner;

    protected enum GolemState
    {
        Run = 0,    // 웨이포인트를 향해 걸어가는 상태
        Attack,     // 공격 상태
        Die         // 사망 상태
    }

    /// <summary>
    /// 상태별 업데이터 함수를 가질 델리게이트
    /// </summary>
    Action stateUpdate;

    protected GolemState State
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;  // 새로운 상태로 변경
                
                switch (state)
                {
                    case GolemState.Run:
                        isTargetOn = false;
                        nav.Stopped(false);         // 목적지를 향해 움직인다.
                        runWaitTimer = runWaitTime; // 기다리는 시간 초기화
                        stateUpdate = Update_Run;   // update에서 실행될 델리게이트 변경 (2초간 기다림)
                        break;

                    case GolemState.Attack:
                        isTargetOn = false;
                        nav.Stopped(true);          // 움직임을 멈춘다
                        waitTimer = waitTime;       // 기다리는 시간 초기화
                        isTargetOn = true;
                        anim.SetTrigger("Attack");  // 공격하는 애니메이션 재생
                        //Isthrow();
                        stateUpdate = Update_attack;  // update에서 실행될 델리게이트 변경 (2초간 기다림)
                        break;

                    case GolemState.Die:
                        nav.Stopped(true);
                        anim.ResetTrigger("Attack");
                        anim.SetTrigger("Die");
                        coll.enabled = false;
                        nav.enabled = false;
                        nav.IsEnabled();
                        stateUpdate = Dead;
                        break;

                    default:
                        break;                        
                }
            }
        }
    }

    protected float WaitTimer
    {
        get => waitTimer;
        set
        {
            waitTimer = value;  
        }
    }

    protected float RunWaitTimer
    {
        get => runWaitTimer;
        set
        {
            runWaitTimer = value;
        }
    }


    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<Enemy_Navigation>();
        //anim.ResetTrigger("Attack");        // 트리거가 쌓이는 현상 방지
        coll = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        //waitTimer = waitTime;       // 기다리는 시간 초기화
        State = GolemState.Run;             // 초기 상태 설정 (Idle)

        monsterHp = monsterMaxHp;
        onHealthChange += HP_Change;
        onDie += Dead;
    }

    private void FixedUpdate()
    {
        HP -= testHpCount;

        stateUpdate();
        LookTarget();
    }

    void Update_Run()
    {
        RunWaitTimer -= Time.fixedDeltaTime;

        if (runWaitTimer < 0.0f)
        {
            State = GolemState.Attack;
        }
    }

    void Update_attack()
    {
        WaitTimer -= Time.fixedDeltaTime;

        if (waitTimer < 0.0f)
        { 
            anim.SetTrigger("NoAttack");
            State = GolemState.Run;
        }
    }

    void HP_Change(float ratio)
    {
        if (isDead)
        {
            //Debug.Log($"{gameObject.name}의 HP가 {HP}로 변경 되었습니다.");
        }

    }

    void Dead()
    {
        if (isDead)
        {
            isDead = false;
            State = GolemState.Die;
            Destroy(this.gameObject, 2.0f);
        }
    }

    void LookTarget()
    {
        if (isTargetOn)
        {
            Vector3 dir = (baseTarget.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), currentAngle * Time.deltaTime * 0.2f);
        }
    }

    public void RockSpawner()
    {
        rock_Spawner = transform.GetChild(3).transform;
        Instantiate(Rock, rock_Spawner.position, Quaternion.identity);
    }
}
