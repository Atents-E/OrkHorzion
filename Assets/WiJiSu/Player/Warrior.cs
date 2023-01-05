using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class Warrior : Character
{
    public float default_Hp = 100.0f; // 체력
    public float default_MaxHp = 100.0f; // 최대 체력
    public float default_Def = 100.0f; // 방어력
    public float default_Atk = 30.0f; // 공격력
    public float default_criticalChance = 0.3f; // 치명타 확률
    public float default_MoveSpeed = 3.0f; //이동 속도

    Transform hand_r;
    Collider weaponBlade;


    protected override void Awake()
    {
        base.Awake();
        hand_r = GetComponentInChildren<WeaponPosition>().transform;
        weaponBlade = hand_r.GetComponentInChildren<Collider>();

        characterName = "전사";
        maxHp = default_MaxHp;
        hp = default_Hp;
        def = default_Def;
        atk = default_Atk;
        criticalChance = default_criticalChance;
        moveSpeed = default_MoveSpeed;

    }

    protected override void Start()
    {
        base.Start();
        Debug.Log($"이름 : {characterName}");
        Debug.Log($"체력 : {hp} / {maxHp}");
        Debug.Log($"방어력 : {def}");
        Debug.Log($"공격력 : {atk}");
        Debug.Log($"치명타확률 : {criticalChance*100}%");
        Debug.Log($"이동속도 : {moveSpeed}");
    }

    /// <summary>
    /// 무기가 공격 행동을 할 때 무기의 트리거 켜는 함수
    /// </summary>
    public void WeaponBladeEnable()
    {
        if (weaponBlade != null)
        {
            weaponBlade.enabled = true;
        }
    }

    /// <summary>
    /// 무기가 공격 행동이 끝날 때 무기의 트리거를 끄는 함수
    /// </summary>
    public void WeaponBladeDisable()
    {
        if (weaponBlade != null)
        {
            weaponBlade.enabled = false;
        }
    }

    public override void Attack(IBattle target) => base.Attack(target);
  
    public override void TakeDamage(float damage) => base.TakeDamage(damage);

    public override void Die() => base.Die();

    public override void Recover() => base.Recover();
}
