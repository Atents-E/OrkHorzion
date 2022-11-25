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
        Anger = 0,      // �г� ����
        Heal,           // ü�� ȸ��
        Delay           // ��ٸ��� �ð�
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
                    Debug.Log("�г�(��ų) �Դϴ�.");
                }
                else
                {
                    State = kingState.Heal;
                    Debug.Log("��(��ų) �Դϴ�.");
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
        //Debug.Log($"������ : {Rand}");
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
            Debug.Log("��ƼŬ ����");

            attackPower *= 2;
            Debug.Log("�������� 2�谡 �Ǿ���.");
        }
        if (angerduration < 0.1)
        {
            particle.Stop();
            Debug.Log("��ƼŬ ����");

            anger.color = Color.clear;

            attackPower = oldDamage;
            Debug.Log("�������� ������� �Ǿ���.");

            State = kingState.Delay;
        }
    }

    private void Update_Heal()
    {
        // HP�� ������ ȸ���ǰ�     
        StartCoroutine(HealingRegeneration());
        if (HP > MaxHP)
        {
            Debug.Log("HP�� MaxHP�� �Ѿ����ϴ�.");
            StopCoroutine(HealingRegeneration());
        }
        Debug.Log("��");
        
    }

    IEnumerator HealingRegeneration()
    {
        
        //Debug.Log("�ڷ�ƾ����");
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
