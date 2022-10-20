using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ork_Basic : EnemyBase
{
    Animator anim;

    public Transform target;             // �׺�Ž� Ÿ��
    protected NavMeshAgent agent;        // �׺�Ž�

    protected Transform moveTarget;                 // ���� �̵��� ��ǥ ������ Ʈ���� ��
    protected WayPoint wayPoint;

    Action stateUpdate;


    EnemyStateOrk state = EnemyStateOrk.wait;
    protected enum EnemyStateOrk
    {
        wait = 0,   // ��� ����
        Run,        // ���� ����
        Attack      // ���� ����
    }

    /// <summary>
    /// �̵��� �������� ��Ÿ���� ������Ƽ
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
                        //agent.SetDestination(wayPoint.Current.position);
                        agent.isStopped = false;
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

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)  // ��� ����� �Ϸ��� ���� ������������ �����Ǵ� �Ÿ����� �̵����� �ʾҴ�.
        {
            MoveTarget = wayPoint.MoveNext();
        }
    }

    protected void Awake()
    {
        anim = GetComponent<Animator>();        // �ִϸ�����
        agent = GetComponent<NavMeshAgent>();   // �׺�Ž�

        wayPoint = FindObjectOfType<WayPoint>();
    }
    protected void Start()
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

        SearchPlayer();
    }

    protected override void Update()
    {
        base.Update();
        Looktarget();
    }
    private void FixedUpdate()
    {
        stateUpdate();
    }

    void NextMove()
    {
        wayPoint.MoveNext();

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)  // ��� ����� �Ϸ��� ���� ������������ �����Ǵ� �Ÿ����� �̵����� �ʾҴ�.
        {
            MoveTarget = wayPoint.MoveNext();
        }
    }

    private void OnTriggerEnter(Collider other)     // Ʈ���� �ȿ� ������ ��
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("����");
            looktargetOn = true;
            playerTarget = other.transform;         // playerTarget�� null�� �ƴϰ� �Ǿ���.
            anim.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit(Collider other)      // Ʈ���Ÿ� ���� ������ ��
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("����");
            playerTarget = null;                    // playerTarget�� null�� �Ǿ���.
            looktargetOn = false;
            anim.SetBool("Attack", false);
        }
    }

}
