using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Rigidbody))]   // 필수적으로 필요한 컴포넌트가 있을 때 자동으로 넣어주는 유니티 속성(Attribute)
[RequireComponent(typeof(Animator))]

public class TestEnemy : MonoBehaviour, IBattle, IHealth
{
    public float attackPower = 10.0f;
    public float defencePower = 3.0f;
    float hp = 100.0f;
    public float maxHp = 100.0f;

    public bool isAlive = true;

    Animator anim;

    // 프로퍼티-----------------------------------------------------------------------------------------------------------
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

    public float MaxHP // 캐릭터 최대 체력 프로퍼티
    {
        get => maxHp;
        set
        {
            // 나중에 유물효과로 최대체력 늘릴 때 쓰는 공간
            maxHp = value;
        }
    }

    public float AttackPower => attackPower;

    public float DefencePower => defencePower;

    // 델리게이트 --------------------------------------------------------------------------------------------

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
        // 테스트 코드 
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
    //    Debug.Log("파티클 충돌");

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
        Debug.Log($"{gameObject.name}가 다시 살아납니다");


    }

    void Test_HP_Change(float ratio)
    {
        anim.SetTrigger("Hit");
        Debug.Log($"{gameObject.name}의 HP가 {HP}로 변경되었습니다.");
        
    }

    void Test_Die()
    {
        Debug.Log($"{gameObject.name}가 죽었습니다");
    }

}
