using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dragon : EnemyBase
{
    Animator anim;
    Enemy_Navigation nav;
    ParticleSystem particle;
    GameObject breath;
    BoxCollider breathArea;

    DragonState state = DragonState.Attack;  // ���� �巡���� ����

    public float waitTime = 2.0f;   // �������� �������� �� ��ٸ��� �ð�
    float waitTimer = 0;            // �����ִ� ��ٷ��� �ϴ� �ð� 

    public float delayTime = 2.0f;   // �������� �������� �� ��ٸ��� �ð�
    float delayTimer = 0;            // �����ִ� ��ٷ��� �ϴ� �ð� 

    bool isDead = true;              // ������ �ѹ��� ����ǰ� �ϴ� ����

    float hptotal = 3000.0f;        // HP �� ȸ����
    float duration = 3.0f;           // �� ȸ���ϴµ� �ɸ��� �ð�

    bool isBreath = false;

    /// <summary>
    /// �巡�� ���� enum
    /// </summary>
    protected enum DragonState
    {
        Attack = 0,
        Delay,
        Breath,
        Heal,
        Move,
        Die,
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
                        anim.SetTrigger("Attack");      // �����ϴ� �ִϸ��̼� ����
                        stateUpdate = Update_Attack;    // ���¸� �����ϴ� �Լ��� ����
                        break;

                    case DragonState.Delay:
                        particle.Stop();
                        isBreath = true;
                        waitTimer = waitTime;           // waitTimer �ð��� ����
                        stateUpdate = Update_Delay;     // ���¸� ��ٸ��� �Լ��� ����
                        break;

                    case DragonState.Breath:   
                        if (isBreath)
                        {
                            isBreath = false;
                            nav.Stopped(true);              // �׺���̼� ���߱�
                            anim.SetTrigger("IsBreath"); // �극�� �ִϸ��̼� ����
                            breath.SetActive(true);
                            breathArea.enabled = true;
                            delayTimer = delayTime;         // delayTimer �ð��� ����
                            stateUpdate = Update_Breath;    // ���¸� �극�� �Լ��� ����
                        }
                        break;

                    case DragonState.Heal:
                        nav.Stopped(false);
                        stateUpdate = Update_Heal;      // ���¸� ȸ���ϴ� �Լ��� ����
                        break;

                    case DragonState.Move:
                        nav.Stopped(false);
                        anim.SetTrigger("IsMove");
                        break;

                    case DragonState.Die:                        
                        stateUpdate = Update_Die;
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
                // ��ų ���
                if (playerTarget != null)
                {
                    float random = UnityEngine.Random.Range(0.0f, 1.0f);
                    Debug.Log($"{random}");

                    if (random < 0.4f)
                    {
                        Debug.Log("�⺻ ����");
                        State = DragonState.Attack;                        
                    }
                    else if (random < 0.7f)
                    {
                        if (isBreath)
                        {
                            Debug.Log("�극�� ��ų ���");
                            State = DragonState.Breath;
                        }
                    }
                    else
                    {
                        Debug.Log("ü�� ȸ��");
                        State = DragonState.Heal;
                    }
                }
                else
                {
                    //Debug.Log("��� ����");                    
                }
            }
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<Enemy_Navigation>();
        particle = GetComponentInChildren<ParticleSystem>();

        breath = GameObject.Find("Breath");
        breathArea = transform.GetChild(4).GetComponent<BoxCollider>();
    }

    private void Start()
    {
        isBreath = true;
        breathArea.enabled = false;
        State = DragonState.Delay;
        SearchPlayer(); // �÷��̾� ã��

        HP = MaxHP;
        //particle.Stop();

        onHealthChange += HP_Change;
        onDie += Update_Die;
    }

    private void Update()
    {
        Looktarget();   // �÷��̾� �ٶ󺸱�
    }

    private void FixedUpdate()
    {
        HP -= Time.fixedDeltaTime * 500.0f;
        stateUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            looktargetOn = true;
            playerTarget = other.transform;         // playerTarget�� null�� �ƴϰ� �Ǿ���.
            //State = DragonState.Delay;
            State = DragonState.Attack;
            Debug.Log("�÷��̾ �����Ѵ�.");
        }
    }    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTarget = null;                    // playerTarget�� null�� �Ǿ���.
            looktargetOn = false;
            //State = DragonState.Move;
            Debug.Log("�÷��̾ ��������.");
        }
    }

    private void Update_Attack()    // ���ݿ�
    {
        if (playerTarget != null)
        {
            Debug.Log("�⺻ ���� ���");
            State = DragonState.Delay;
        }
    }

    private void Update_Delay()     // n�ʰ� ��ٸ���
    {
        WaitTimer -= Time.deltaTime;
    }

    private void Update_Breath()    // �극�� ��ų
    {
        DelayTimer -= Time.deltaTime;

        if (delayTimer < 0.0f && !isBreath)
        {
            isBreath = true;
            nav.Stopped(false);
            anim.SetBool("IsMove", true);
            particle.Stop();
            breath.SetActive(false);
            breathArea.enabled = false;
            Debug.Log("�극�� ��� ����"); 
            State = DragonState.Delay;
        }
    }

    /// <summary>
    /// �ִϸ��̼� �̺�Ʈ�� �극�� �ִϸ��̼��� ����Ǹ� ����
    /// </summary>
    public void DragonBreath()
    {
        Debug.Log("�극�� ���");
        particle.Play();

        delayTimer = delayTime;
        if (delayTimer < 0)
        {
            particle.Stop();
        }
    }

    private void Update_Heal()
    {
        StartCoroutine(HealingRegeneration());
    }

    IEnumerator HealingRegeneration()
    {
        float regenPerSec = hptotal / duration;
        float timeElapsed = 0.0f;
        Debug.Log("ü�� ȸ��");
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
            Debug.Log("��� ó��");
            isDead = false;
            nav.Stopped(true);              // �׺���̼� ���߱�
            anim.SetTrigger("IsDead");      // �״� �ִϸ��̼� ����
            Destroy(this.gameObject, 2.0f);
        }
    }
}