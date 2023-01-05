using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard :Character
{
    public float default_Hp = 80.0f; // 체력
    public float default_MaxHp = 100.0f; // 최대 체력
    public float default_Def = 50.0f; // 방어력
    public float default_Atk = 10.0f; // 공격력
    public float default_criticalChance = 0.3f; // 치명타 확률
    public float default_MoveSpeed = 6.0f; //이동 속도


    Transform hand_r;
    ParticleSystem weaponPS;

    protected override void Awake()
    {
        base.Awake();
        hand_r = GetComponentInChildren<WeaponPosition>().transform;
        weaponPS = hand_r.GetComponentInChildren<ParticleSystem>();

        characterName = "마법사";
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

    public void WeaponEffectEnable()
    {
        if (weaponPS != null)
        {
            weaponPS.Play();    // 파티클 이팩트 재생 시작
        }
    }
    public void WeaponEffectDisable()
    {
        if (weaponPS != null)
        {
            weaponPS.Stop();    // 파티클 이팩트 재생 시작
        }
    }

    public override void Attack(IBattle target) => base.Attack(target);

    public override void TakeDamage(float damage) => base.TakeDamage(damage);

    public override void Die() => base.Die();

    public override void Recover() => base.Recover();
}
