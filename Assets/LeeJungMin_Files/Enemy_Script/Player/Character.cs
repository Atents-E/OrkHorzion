using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Character : MonoBehaviour, IBattle, IHealth
{
    public GameObject point;
    public ParticleSystem spawnPS;
    

    protected string characterName; // 캐릭터 이름
    protected float atk; // 캐릭터 공격력
    protected float def; // 캐릭터 방어력
    protected float hp; // 캐릭터 체력
    protected float maxHp; // 캐릭터 최대 체력
    protected float criticalChance; // 캐릭터 치명타 확률
    protected float moveSpeed; // 캐릭터 이동속도

    public bool isAlive = true;

    Animator anim;


    // 프로퍼티-----------------------------------------------------------------------------------------------------------
    public string CharacterName => characterName; // 캐릭터 이름 프로퍼티

    public float MaxHP
    {
        get => maxHp;  //캐릭터 최대 체력 프로퍼티
        set => maxHp = value;
    }

    public float DEF => def; // 캐릭터 방어력 프로퍼티

    public float ATK => atk; // 캐릭터 공격력 프로퍼티

    public float CriticalChance => criticalChance; // 캐릭터 치명타 확률 프로퍼티

    public float MoveSpeed => moveSpeed; // 캐릭터 이동속도 프로퍼티

    public float HP 
    { 
        get => hp;
        set { 
            if(hp != value)
            { 
                hp = value;
               
                if (hp <= 0)
                {
                    Die();
                }
                
                hp = Mathf.Clamp(hp, 0.0f, maxHp);

                onHealthChange?.Invoke(hp/maxHp);
            }
            
        } 
    }


    // 델리게이트 --------------------------------------------------------------------------------------------
    
    public Action <float> onHealthChange { get; set; }
    
    public Action onDie { get; set; }

    public float AttackPower => atk;

    public float DefencePower => def;

    // --------------------------------------------------------------------------------------------------------

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        point = GameObject.Find("SpawnPoint");
        spawnPS = point.GetComponentInChildren<ParticleSystem>();
    }

    protected virtual void Start()
    {
        hp = maxHp;
        isAlive = true;
        
    }

    public virtual void Attack(IBattle target)
    {
        float damage = ATK; // 데미지 = 해당 클래스의 공격력
        if (Random.Range(0.0f, 1.0f) < criticalChance)
        {
            Debug.Log("Critical★");
            damage *= 1.5f;
              
        }
        target?.TakeDamage(damage);// 플레이어는 공격력만큼 데미지를 주지만 치명타가 뜨면 1.5배의 데미지를 준다.
    }

    public virtual void Defence(float damage)
    {
        
    }

    public virtual void Die()
    {
            Debug.Log($"{gameObject.name}가 죽었습니다");
            isAlive = false;
            anim.SetBool("IsAlive", isAlive);   // 죽었다고 표시해서 사망 애니메이션 재생
            onDie?.Invoke();
            Invoke("Recover", 3f);

    }

    public virtual void Recover()
    {
        bool on = true;
        if (spawnPS != null)
        {
            if (on)
            {
                spawnPS.Play();
            }
            else
            {
                spawnPS.Stop();
            }
        }
        isAlive = true;
        anim.SetBool("IsAlive", isAlive);
        anim.SetTrigger("Recover");

        HP = maxHp;

        Vector3 spotPos = point.transform.position;
        transform.position = spotPos;
        Debug.Log($"{gameObject.name}가 다시 살아납니다");
       
        
    }

    public void TakeDamage(float damage)
    {
        if (isAlive)                // 살아있을 때만 데미지 입음.
        {
            anim.SetTrigger("Hit"); // 피격 애니메이션 재생            
            float finalDamage = damage * (1.0f - DEF / (DEF + 100.0f));
            HP -= finalDamage;
            Debug.Log($"{gameObject.name}의 HP가 {HP}로 변경되었습니다.");
        }
    }
}