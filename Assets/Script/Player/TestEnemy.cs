using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Rigidbody))]   // �ʼ������� �ʿ��� ������Ʈ�� ���� �� �ڵ����� �־��ִ� ����Ƽ �Ӽ�(Attribute)
[RequireComponent(typeof(Animator))]

public class TestEnemy : MonoBehaviour, IBattle, IHealth
{
    public float attackPower = 10.0f;
    public float defencePower = 3.0f;
    float hp = 100.0f;
    public float maxHp = 100.0f;

    public bool isAlive = true;

    Animator anim;

    // ������Ƽ-----------------------------------------------------------------------------------------------------------
    public float HP
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                
                if (hp < 0)
                {
                    Die();
                }

                hp = Mathf.Clamp(hp, 0.0f, maxHp);

                onHealthChange?.Invoke(hp / maxHp);

               
            }

        }
    }

    public float MaxHP // ĳ���� �ִ� ü�� ������Ƽ
    {
        get => maxHp;
        set
        {
            // ���߿� ����ȿ���� �ִ�ü�� �ø� �� ���� ����
            maxHp = value;
        }
    }

    public float AttackPower => attackPower;

    public float DefencePower => defencePower;

    // ��������Ʈ --------------------------------------------------------------------------------------------

    public Action <float> onHealthChange { get; set; }

    public Action onDie { get; set; }

    // --------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        hp = maxHp;
        isAlive = true;
        // �׽�Ʈ �ڵ� 
        onHealthChange += Test_HP_Change;
        onDie += Test_Die;
    }

    public void Attack(IBattle target)
    {
        target?.TakeDamage(AttackPower);
    }

    public void TakeDamage(float damage)
    {
        HP -= (damage - DefencePower);
    }

    //void OnParticleCollision(GameObject other)

    //{
    //    Debug.Log("��ƼŬ �浹");

    //}

    public void Die()
    {
        anim.SetTrigger("Die");
        onDie?.Invoke();
        Invoke("Recover", 3f);

    }

    public void Recover()
    {
        isAlive = true;
        anim.SetTrigger("Recover");
        HP = maxHp;
        Debug.Log($"{gameObject.name}�� �ٽ� ��Ƴ��ϴ�");


    }

    void Test_HP_Change(float ratio)
    {
        anim.SetTrigger("Hit");
        Debug.Log($"{gameObject.name}�� HP�� {HP}�� ����Ǿ����ϴ�.");
        
    }

    void Test_Die()
    {
        Debug.Log($"{gameObject.name}�� �׾����ϴ�");
    }

}
