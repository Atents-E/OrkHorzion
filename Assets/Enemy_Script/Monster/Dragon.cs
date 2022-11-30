using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : EnemyBase
{
    Animator anim;
    Enemy_Navigation nav;

    DragonState state = DragonState.Attack;  // 현재 드래곤의 상태

    public float waitTime = 3.0f;   // 목적지에 도착했을 때 기다리는 시간
    float waitTimer = 0;            // 남아있는 기다려야 하는 시간 

    public float delayTime = 3.0f;   // 목적지에 도착했을 때 기다리는 시간
    float delayTimer = 0;            // 남아있는 기다려야 하는 시간 

    bool isDead = true;              // 죽으면 한번만 실행되게 하는 변수

    float hptotal = 10000.0f;        // HP 총 회복량
    float duration = 1.0f;           // 총 회복하는데 걸리는 시간

    /// <summary>
    /// 드래곤 상태 enum
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
                        nav.Stopped(true);              // 네비게이션 멈추기
                        anim.SetBool("IsMove", false);  // 움직이는 애니메이션 멈추기
                        anim.SetTrigger("Attack");      // 공격하는 애니메이션 실행
                        stateUpdate = Update_Attack;    // 상태를 공격하는 함수로 변경
                        break;

                    case DragonState.Delay:
                        nav.Stopped(false);             // 네비게이션 움직이기
                        anim.SetBool("IsMove", true);   // 움직이는 애니메이션 실행
                        anim.SetBool("IsBreath", false);// 브레스 애니메이션 끄기
                        waitTimer = waitTime;           // waitTimer 시간초 충전
                        stateUpdate = Update_Delay;     // 상태를 기다리는 함수로 변경
                        break;

                    case DragonState.Breath:            
                        nav.Stopped(true);              // 네비게이션 멈추기
                        anim.SetBool("IsBreath", true); // 브레스 애니메이션 실행
                        delayTimer = delayTime;         // delayTimer 시간초 충전
                        stateUpdate = Update_Breath;    // 상태를 브레스 함수로 변경
                        break;

                    case DragonState.Heal:              
                        stateUpdate = Update_Heal;      // 상태를 회복하는 함수로 변경
                        break;

                    case DragonState.Die:
                        nav.Stopped(true);              // 네비게이션 멈추기
                        anim.SetTrigger("IsDead");      // 죽는 애니메이션 실행
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
                float random = UnityEngine.Random.Range(0.0f, 1.0f);
                Debug.Log($"{random}");

                // 스킬 사용
                if (random < 0.3f)
                {
                    Debug.Log("브레스 스킬 사용");
                    State = DragonState.Breath;
                }
                else if (random < 0.6f)
                {
                    State = DragonState.Attack;
                    Debug.Log("기본 공격 사용");
                }
                else
                {
                    Debug.Log("스킬 미정");
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

    private void Update_Attack()    // 공격용
    {
        State = DragonState.Delay;
    }

    private void Update_Delay()     // n초간 기다리기
    {
        WaitTimer -= Time.deltaTime;
    }

    private void Update_Breath()    // 브레스 스킬
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
            Debug.Log("브레스 사용 끝남");
            State = DragonState.Delay;
        }
        //anim.SetBool("IsBreath", false);
        //State = DragonState.Delay;
    }

    public void DragonBreath()
    {
        Debug.Log("브레스 사용 ");
    }


    private void Update_Heal()
    {
        StartCoroutine(HealingRegeneration());
    }

    IEnumerator HealingRegeneration()
    {
        float regenPerSec = hptotal / duration;
        float timeElapsed = 0.0f;
        Debug.Log("힐이다");
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
        //Debug.Log($"HP가 변경되었다. : {HP}");
    }

    private void Update_Die()       // 죽을 때
    {
        if (isDead)
        {
            isDead = false;
            State = DragonState.Die;
            Destroy(this.gameObject, 2.0f);
        }
    }
}
