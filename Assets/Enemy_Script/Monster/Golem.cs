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
    public float testHpCount =5;
    bool isDead = true;
    bool isTargetOn = false;

    public Transform baseTarget;
    Animator anim;
    Enemy_Navigation nav;

    CapsuleCollider coll;

    GolemState state = GolemState.Attack;  // ���� ���� ����

    Transform rock_Spawner;

    protected enum GolemState
    {
        Run = 0,    // ��������Ʈ�� ���� �ɾ�� ����
        Attack,     // ���� ����
        Die         // ��� ����
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
                        isTargetOn = false;
                        nav.Stopped(false);         // �������� ���� �����δ�.
                        runWaitTimer = runWaitTime; // ��ٸ��� �ð� �ʱ�ȭ
                        stateUpdate = Update_Run;   // update���� ����� ��������Ʈ ���� (2�ʰ� ��ٸ�)
                        break;

                    case GolemState.Attack:
                        isTargetOn = false;
                        nav.Stopped(true);          // �������� �����
                        waitTimer = waitTime;       // ��ٸ��� �ð� �ʱ�ȭ
                        isTargetOn = true;
                        anim.SetTrigger("Attack");  // �����ϴ� �ִϸ��̼� ���
                        //Isthrow();
                        stateUpdate = Update_attack;  // update���� ����� ��������Ʈ ���� (2�ʰ� ��ٸ�)
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
        //anim.ResetTrigger("Attack");        // Ʈ���Ű� ���̴� ���� ����
        coll = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        //waitTimer = waitTime;       // ��ٸ��� �ð� �ʱ�ȭ
        State = GolemState.Run;             // �ʱ� ���� ���� (Idle)

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
            //Debug.Log($"{gameObject.name}�� HP�� {HP}�� ���� �Ǿ����ϴ�.");
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
