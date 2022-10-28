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

    public float waitTime = 2.0f;       // �������� �������� �� ��ٸ��� �ð�
    float waitTimer = 0;                // �����ִ� ��ٷ��� �ϴ� �ð� 

    public float runWaitTime = 5.0f;   // �������� �������� �� ��ٸ��� �ð�
    float runWaitTimer = 0;            // �����ִ� ��ٷ��� �ϴ� �ð� 

    public Transform baseTarget;
    Animator anim;
    Enemy_Navigation nav;

    GolemState state = GolemState.attack;  // ���� ���� ����

    protected enum GolemState
    {
        Run = 0,    // ��������Ʈ�� ���� �ɾ�� ����
        attack      // ���� ����
    }

    /// <summary>
    /// ���º� �������� �Լ��� ���� ��������Ʈ
    /// </summary>
    Action stateUpdate;

    protected GolemState State
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;  // ���ο� ���·� ����

                switch (state)
                {
                    case GolemState.Run:
                        nav.Stopped(false);         // �������� ���� �����δ�.
                        
                        runWaitTimer = runWaitTime; // ��ٸ��� �ð� �ʱ�ȭ
                        stateUpdate = Update_Run;   // update���� ����� ��������Ʈ ���� (2�ʰ� ��ٸ�)
                        break;

                    case GolemState.attack:
                        nav.Stopped(true);          // �������� �����
                        waitTimer = waitTime;       // ��ٸ��� �ð� �ʱ�ȭ

                        transform.LookAt(baseTarget);   // ������ �ʿ��� �ڵ� 
                        
                        anim.SetTrigger("Attack");  // �����ϴ� �ִϸ��̼� ���
                        //Isthrow();
                        stateUpdate = Update_attack;  // update���� ����� ��������Ʈ ���� (2�ʰ� ��ٸ�)
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
        //anim.ResetTrigger("Attack");        // Ʈ���Ű� ���̴� ���� ����
    }

    private void Start()
    {
        //waitTimer = waitTime;       // ��ٸ��� �ð� �ʱ�ȭ
        State = GolemState.Run;             // �ʱ� ���� ���� (Idle)
        
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
