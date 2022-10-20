using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ork_Basic : EnemyBase
{
    Animator anim;

    public Transform target;             // 네비매시 타겟
    protected NavMeshAgent agent;        // 네비매시

    protected Transform moveTarget;                 // 적이 이동할 목표 지점의 트랜스 폼
    protected WayPoint wayPoint;

    Action stateUpdate;


    EnemyStateOrk state = EnemyStateOrk.wait;
    protected enum EnemyStateOrk
    {
        wait = 0,   // 대기 상태
        Run,        // 순찰 상태
        Attack      // 추적 상태
    }

    /// <summary>
    /// 이동할 목적지를 나타내는 프로퍼티
    /// </summary>
    protected Transform MoveTarget
    {
        get => moveTarget;
        set
        {
            moveTarget = value;
        }
    }

    protected EnemyStateOrk State
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;
                switch(state)
                {
                    case EnemyStateOrk.wait:
                        agent.isStopped = true;
                        
                        break;

                    case EnemyStateOrk.Run:
                        agent.SetDestination(wayPoint.Current.position);
                        stateUpdate = NextMoving;
                        break;

                    case EnemyStateOrk.Attack:
                        
                        break;

                    default:
                        break;
                }
            }
        }
    }

    void NextMoving()
    {
        agent.SetDestination(wayPoint.Current.position);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)  // 경로 계산이 완료됬고 아직 도착지점으로 인정되는 거리까지 이동하지 않았다.
        {
            MoveTarget = wayPoint.MoveNext();
        }
    }

    protected override void Awake()
    {
        anim = GetComponent<Animator>();        // 애니메이터
        agent = GetComponent<NavMeshAgent>();   // 네비매시

        wayPoint = FindObjectOfType<WayPoint>();
    }
    protected override void Start()
    {
        //if (wayPoint != null)
        //{
        //    MoveTarget = wayPoint.Current;
        //}
        //else
        //{
        //    MoveTarget = transform;
        //}

        State = EnemyStateOrk.Run;
    }

    protected override void Update()
    {
        
    }

    private void FixedUpdate()
    {
        stateUpdate();
    }

    void NextMove()
    {
        wayPoint.MoveNext();

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)  // 경로 계산이 완료됬고 아직 도착지점으로 인정되는 거리까지 이동하지 않았다.
        {
            MoveTarget = wayPoint.MoveNext();
        }
    }

    private void OnTriggerEnter(Collider other)     // 트리거 안에 들어왔을 때
    {
        if (other.CompareTag("Player"))
        {
            State = EnemyStateOrk.wait;
            Debug.Log("들어옴");
            playerTarget = other.transform;         // playerTarget이 null이 아니게 되었다.
        }
    }

    private void OnTriggerExit(Collider other)      // 트리거를 빠져 나갔을 때
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("나감");
            playerTarget = null;                    // playerTarget이 null이 되었다.

        }
    }

}
