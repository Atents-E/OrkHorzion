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

    public Transform baseTarget;
    Animator anim;
    Enemy_Navigation nav;

    GolemState state = GolemState.attack;  // 현재 골렘의 상태

    protected enum GolemState
    {
        Run = 0,    // 웨이포인트를 향해 걸어가는 상태
        attack      // 공격 상태
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
                        nav.Stopped(false);         // 목적지를 향해 움직인다.
                        
                        runWaitTimer = runWaitTime; // 기다리는 시간 초기화
                        stateUpdate = Update_Run;   // update에서 실행될 델리게이트 변경 (2초간 기다림)
                        break;

                    case GolemState.attack:
                        nav.Stopped(true);          // 움직임을 멈춘다
                        waitTimer = waitTime;       // 기다리는 시간 초기화

                        transform.LookAt(baseTarget);   // 수정이 필요한 코드 
                        
                        anim.SetTrigger("Attack");  // 공격하는 애니메이션 재생
                        //Isthrow();
                        stateUpdate = Update_attack;  // update에서 실행될 델리게이트 변경 (2초간 기다림)
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
    }

    private void Start()
    {
        //waitTimer = waitTime;       // 기다리는 시간 초기화
        State = GolemState.Run;             // 초기 상태 설정 (Idle)
        
    }

    private void FixedUpdate()
    {
        stateUpdate();
    }

    void Update_Run()
    {
        RunWaitTimer -= Time.fixedDeltaTime;

        if (runWaitTimer < 0.0f)
        {
            State = GolemState.attack;
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

    //void Isthrow()
    //{
    //    Instantiate(Rock, transform.position, Quaternion.identity);
    //}
}
