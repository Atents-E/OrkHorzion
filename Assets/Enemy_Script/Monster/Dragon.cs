using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : EnemyBase
{
    Animator anim;
    Enemy_Navigation nav;

    DragonState state = DragonState.Attack;  // ���� �巡���� ����

    public float waitTime = 3.0f;   // �������� �������� �� ��ٸ��� �ð�
    float waitTimer = 0;            // �����ִ� ��ٷ��� �ϴ� �ð� 

    public float delayTime = 3.0f;   // �������� �������� �� ��ٸ��� �ð�
    float delayTimer = 0;            // �����ִ� ��ٷ��� �ϴ� �ð� 

    bool isDead = true;              // ������ �ѹ��� ����ǰ� �ϴ� ����

    float hptotal = 10000.0f;        // HP �� ȸ����
    float duration = 1.0f;           // �� ȸ���ϴµ� �ɸ��� �ð�

    /// <summary>
    /// �巡�� ���� enum
    /// </summary>
    protected enum DragonState
    {
        Attack = 0,
        Delay,
        Breath,
        Heal,
        Die
    }

    Action stateUpdate;

    /// <summary>
    /// �巡�� ���¸� �ٲ��ִ� ������Ƽ
    /// </summary>
    protected DragonState State
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;

                switch (state)
                {

                    case DragonState.Attack:
                        nav.Stopped(true);              // �׺���̼� ���߱�
                        anim.SetBool("IsMove", false);  // �����̴� �ִϸ��̼� ���߱�
                        anim.SetTrigger("Attack");      // �����ϴ� �ִϸ��̼� ����
                        stateUpdate = Update_Attack;    // ���¸� �����ϴ� �Լ��� ����
                        break;

                    case DragonState.Delay:
                        nav.Stopped(false);             // �׺���̼� �����̱�
                        anim.SetBool("IsMove", true);   // �����̴� �ִϸ��̼� ����
                        anim.SetBool("IsBreath", false);// �극�� �ִϸ��̼� ����
                        waitTimer = waitTime;           // waitTimer �ð��� ����
                        stateUpdate = Update_Delay;     // ���¸� ��ٸ��� �Լ��� ����
                        break;

                    case DragonState.Breath:            
                        nav.Stopped(true);              // �׺���̼� ���߱�
                        anim.SetBool("IsBreath", true); // �극�� �ִϸ��̼� ����
                        delayTimer = delayTime;         // delayTimer �ð��� ����
                        stateUpdate = Update_Breath;    // ���¸� �극�� �Լ��� ����
                        break;

                    case DragonState.Heal:              
                        stateUpdate = Update_Heal;      // ���¸� ȸ���ϴ� �Լ��� ����
                        break;

                    case DragonState.Die:
                        nav.Stopped(true);              // �׺���̼� ���߱�
                        anim.SetTrigger("IsDead");      // �״� �ִϸ��̼� ����
                        break;
                }
            }
        }
    }

    /// <summary>
    /// ������ �ɸ��� �ð� ������Ƽ
    /// </summary>
    public float DelayTimer
    {
        get => delayTimer;
        set
        {
            delayTimer = value;
        }
    }

    /// <summary>
    /// ������ ���·� �ٲ��� �� n�ʸ� ��ٸ���, �������� ���¸� ��������ִ� ������Ƽ
    /// </summary>
    public float WaitTimer
    {
        get => waitTimer;
        set
        {
            waitTimer = value;

            if (waitTimer < 0)
            {
                float random = UnityEngine.Random.Range(0.0f, 1.0f);
                Debug.Log($"{random}");

                // ��ų ���
                if (random < 0.3f)
                {
                    Debug.Log("�극�� ��ų ���");
                    State = DragonState.Breath;
                }
                else if (random < 0.6f)
                {
                    State = DragonState.Attack;
                    Debug.Log("�⺻ ���� ���");
                }
                else
                {
                    Debug.Log("��ų ����");
                    State = DragonState.Heal;
                }
            }
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<Enemy_Navigation>();
    }

    private void Start()
    {
        State = DragonState.Delay;

        HP = MaxHP;

        onHealthChange += HP_Change;
        onDie += Update_Die;
    }

    private void FixedUpdate()
    {
        HP -= Time.fixedDeltaTime * 100.0f;
        stateUpdate();
    }

    private void Update_Attack()    // ���ݿ�
    {
        State = DragonState.Delay;
    }

    private void Update_Delay()     // n�ʰ� ��ٸ���
    {
        WaitTimer -= Time.deltaTime;
    }

    private void Update_Breath()    // �극�� ��ų
    {
        DelayTimer -= Time.deltaTime;
        bool dbreath = true;
        if (delayTimer > 0.1)
        {
            if (dbreath)
            {
                dbreath = false;
                
            }
        }
        else
        {
            Debug.Log("�극�� ��� ����");
            State = DragonState.Delay;
        }
        //anim.SetBool("IsBreath", false);
        //State = DragonState.Delay;
    }

    public void DragonBreath()
    {
        Debug.Log("�극�� ��� ");
    }


    private void Update_Heal()
    {
        StartCoroutine(HealingRegeneration());
    }

    IEnumerator HealingRegeneration()
    {
        float regenPerSec = hptotal / duration;
        float timeElapsed = 0.0f;
        Debug.Log("���̴�");
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            HP += Time.deltaTime * regenPerSec;
            State = DragonState.Delay;
            yield return null;
        }
    }


    void HP_Change(float ratio)
    {
        //Debug.Log($"HP�� ����Ǿ���. : {HP}");
    }

    private void Update_Die()       // ���� ��
    {
        if (isDead)
        {
            isDead = false;
            State = DragonState.Die;
            Destroy(this.gameObject, 2.0f);
        }
    }
}
