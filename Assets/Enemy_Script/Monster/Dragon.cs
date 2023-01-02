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
    BoxCollider breathArea;

    SpriteRenderer breath;
    SpriteRenderer heal;

    DragonState state = DragonState.Attack;  // 현재 드래곤의 상태

    public float waitTime = 2.0f;   // 목적지에 도착했을 때 기다리는 시간
    float waitTimer = 0;            // 남아있는 기다려야 하는 시간 

    public float delayTime = 2.0f;   // 목적지에 도착했을 때 기다리는 시간
    float delayTimer = 0;            // 남아있는 기다려야 하는 시간 

    bool isDead = true;              // 죽으면 한번만 실행되게 하는 변수

    float hptotal = 3000.0f;        // HP 총 회복량
    float duration = 3.0f;           // 총 회복하는데 걸리는 시간

    //float testTime = 0.0f;

    bool isBreath = false;

    IBattle target;

    /// <summary>
    /// 드래곤 상태 enum
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
    /// 드래곤 상태를 바꿔주는 프로퍼티
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
                        stateUpdate = Update_Attack;    // 상태를 공격하는 함수로 변경
                        break;

                    case DragonState.Delay:
                        particle.Stop();
                        isBreath = true;
                        waitTimer = waitTime;           // waitTimer 시간초 충전
                        stateUpdate = Update_Delay;     // 상태를 기다리는 함수로 변경
                        break;

                    case DragonState.Breath:   
                        if (isBreath)
                        {
                            isBreath = false;
                            nav.Stopped(true);              // 네비게이션 멈추기
                            anim.SetTrigger("IsBreath"); // 브레스 애니메이션 실행
                            breath.color = Color.white;
                            //breath.SetActive(true);
                            breathArea.enabled = true;
                            delayTimer = delayTime;         // delayTimer 시간초 충전
                            stateUpdate = Update_Breath;    // 상태를 브레스 함수로 변경
                        }
                        break;

                    case DragonState.Heal:
                        nav.Stopped(false);
                        stateUpdate = Update_Heal;      // 상태를 회복하는 함수로 변경
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
    /// 딜레이 걸리는 시간 프로퍼티
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
    /// 딜레이 상태로 바꼈을 때 n초를 기다리고, 랜덤으로 상태를 변경시켜주는 프로퍼티
    /// </summary>
    public float WaitTimer
    {
        get => waitTimer;
        set
        {
            waitTimer = value;

            if (waitTimer < 0)
            {
                // 스킬 사용
                if (playerTarget != null)
                {
                    float random = UnityEngine.Random.Range(0.0f, 1.0f);
                    Debug.Log($"{random}");

                    if (random < 0.4f)
                    {
                        Debug.Log("기본 공격");
                        State = DragonState.Attack;                        
                    }
                    else if (random < 0.7f)
                    {
                        if (isBreath)
                        {
                            Debug.Log("브레스 스킬 사용");
                            State = DragonState.Breath;
                        }
                    }
                    else
                    {
                        Debug.Log("체력 회복");
                        heal.color = Color.white;
                        State = DragonState.Heal;
                    }
                }
                else
                {
                    //Debug.Log("대기 상태");                    
                }
            }
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<Enemy_Navigation>();
        particle = GetComponentInChildren<ParticleSystem>();

        //breath = GameObject.Find("Breath");        

        breathArea = transform.GetChild(4).GetComponent<BoxCollider>();

        breath = transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<SpriteRenderer>();
        heal = transform.GetChild(3).GetChild(2).GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        isBreath = true;
        breathArea.enabled = false;
        State = DragonState.Delay;
        SearchPlayer(); // 플레이어 찾기

        HP = MaxHP;
        //particle.Stop();

        onHealthChange += HP_Change;
        onDie += Update_Die;
    }

    private void Update()
    {
        Looktarget();   // 플레이어 바라보기
    }

    private void FixedUpdate()
    {
        stateUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        target = other.GetComponent<IBattle>();
        if (other.CompareTag("Player") && target != null)
        {
            looktargetOn = true;
            playerTarget = other.transform;         // playerTarget이 null이 아니게 되었다.
            //State = DragonState.Delay;
            State = DragonState.Attack;
            Debug.Log("플레이어를 공격한다.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTarget = null;                    // playerTarget이 null이 되었다.
            looktargetOn = false;
            target = null;
            //State = DragonState.Move;
            Debug.Log("플레이어가 도망갔다.");
        }
    }

    private void Update_Attack()    // 공격용
    {
        if (playerTarget != null)
        {
            Debug.Log("기본 공격 사용");
            anim.SetTrigger("Attack");      // 공격하는 애니메이션 실행
            Attack(target);
            State = DragonState.Delay;
        }
    }

    private void Update_Delay()     // n초간 기다리기
    {
        WaitTimer -= Time.deltaTime;
    }

    private void Update_Breath()    // 브레스 스킬
    {
        DelayTimer -= Time.deltaTime;

        if (delayTimer < 0.0f && !isBreath)
        {
            isBreath = true;
            nav.Stopped(false);
            anim.SetBool("IsMove", true);
            particle.Stop();
            //breath.SetActive(false);
            breathArea.enabled = false;
            breath.color = Color.clear;
            Debug.Log("브레스 사용 끝남"); 
            State = DragonState.Delay;
        }
    }

    /// <summary>
    /// 애니메이션 이벤트로 브레스 애니메이션이 실행되면 실행
    /// </summary>
    public void DragonBreath()
    {
        Debug.Log("브레스 사용");
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
        Debug.Log("체력 회복");
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            HP += Time.deltaTime * regenPerSec;
            heal.color = Color.clear;
            State = DragonState.Delay;
            yield return null;
        }
    }

    void HP_Change(float ratio)
    {
        //Debug.Log($"HP가 변경되었다. : {HP}");
    }

    private void Update_Die()       // 사망 처리
    {
        if (isDead)
        {
            Debug.Log("사망 처리");
            breath.color = Color.clear;
            heal.color = Color.clear;
            isDead = false;
            nav.Stopped(true);              // 네비게이션 멈추기
            anim.SetTrigger("IsDead");      // 죽는 애니메이션 실행
            Destroy(this.gameObject, 2.0f);
        }
    }
}
