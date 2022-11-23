using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ork_King : Ork_Basic
{
    ParticleSystem particle;
    SpriteRenderer anger;
    SpriteRenderer heal;

    float oldDamage;

    float waitTime = 3.0f;
    float waitTimer = 0.0f;

    float angerTime = 6.0f;
    float angerduration = 0.0f;

    float hptotal = 20000.0f;
    float duration = 3.0f;

    bool isAnger;

    protected enum kingState
    {
        Anger = 0,      // 분노 상태
        Heal,           // 체력 회복
        Delay           // 기다리는 시간
    }

    kingState state;

    Action stateUpdata;

    protected kingState State
    {
        get => state;
        set
        {
            if (state != value || State == value)
            {
                state = value;

                switch (state)
                {
                    case kingState.Anger:
                        angerduration = angerTime;
                        isAnger = true;
                        oldDamage = attackPower;
                        stateUpdata = Update_Anger;
                        break;
                    case kingState.Heal:
                        waitTimer = waitTime;
                        heal.color = Color.white;
                        stateUpdata = Update_Heal;
                        break;
                    case kingState.Delay:
                        waitTimer = waitTime;
                        stateUpdata = Update_Delay;
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

            if (waitTimer < 0)
            {        
                float random = UnityEngine.Random.Range(0.0f, 1.0f);
                
                if (random < 0.49)
                {
                    State = kingState.Anger;
                    Debug.Log("분노(스킬) 입니다.");
                }
                else
                {
                    State = kingState.Heal;
                    Debug.Log("힐(스킬) 입니다.");
                }
            }
        }
    }

    protected float Angerduration
    {
        get => angerduration;
        set
        {
            angerduration = value;
        }
    }



    protected override void Awake()
    {
        base.Awake();

        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        anger = transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<SpriteRenderer>();
        heal = transform.GetChild(3).GetChild(2).GetChild(1).GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        waitTimer = waitTime;
        angerduration = angerTime;
        HP -= 30000.0f;

        State = kingState.Delay;
        //Debug.Log($"랜덤값 : {Rand}");
    }

    private void FixedUpdate()
    {
        stateUpdata();
    }

    private void Update_Anger()
    {
        Angerduration -= Time.fixedDeltaTime;

        if (isAnger)
        {
            isAnger = false;
            anger.color = Color.white;

            particle.Play();
            Debug.Log("파티클 실행");

            attackPower *= 2;
            Debug.Log("데미지가 2배가 되었다.");
        }
        if (angerduration < 0.1)
        {
            particle.Stop();
            Debug.Log("파티클 종료");

            anger.color = Color.clear;

            attackPower = oldDamage;
            Debug.Log("데미지가 원래대로 되었다.");

            State = kingState.Delay;
        }
    }

    private void Update_Heal()
    {
        // HP가 서서히 회복되게     
        StartCoroutine(HealingRegeneration());
        if (HP > MaxHP)
        {
            Debug.Log("HP가 MaxHP를 넘었습니다.");
            StopCoroutine(HealingRegeneration());
        }
        Debug.Log("힐");
        
    }

    IEnumerator HealingRegeneration()
    {
        
        //Debug.Log("코루틴시작");
        float regenPerSec = hptotal / duration;
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            HP += Time.deltaTime * regenPerSec;
            yield return null;
            State = kingState.Delay;
        }
        heal.color = Color.clear;
    }

    private void Update_Delay()
    {
        WaitTimer -= Time.fixedDeltaTime;
    }
}
