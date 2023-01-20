using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard :Character
{
    public Inventory inven; 
    StatManager statManager;
    InventoryUI invenUI;

    ParticleSystem weaponPS;

    protected override void Awake()
    {
        base.Awake();
        weaponPS = GetComponentInChildren<ParticleSystem>();  // 파티클 시스템 가져오기
        invenUI = FindObjectOfType<InventoryUI>();
        characterName = "마법사";
    }

    protected override void Start()
    {
        inven = new Inventory(2);   // 인벤토리 생성자를 통해 해당 Warrior 플레이어가 가지고 있을 인벤토리 __ 장명근 작성
        statManager = GameManager.Inst.StatManager; // 게임 매니저에서 스탯 매니저를 가져옴 __ 장명근 작성
        statManager.WizardInitializeStat(inven);   // 스탯 매니저에서 입력한 스탯을 적용하고 가지고 있는 아이템에 따라 스탯이 바뀌기 위해 해당 플레이어의 인벤토리를 인수로 넘김 __ 장명근 작성
        GameManager.Inst.InventoryUI.InitializeInventoy(inven); // 인벤토리 상황을 UI로 띄우기 위해 해당 플레이어의 인벤토리를 인수로 넘김 __ 장명근 작성

        base.Start();

        Debug.Log($"이름 : {characterName}");
        Debug.Log($"체력 : {hp} / {maxHp}");
        Debug.Log($"방어력 : {def}");
        Debug.Log($"공격력 : {atk}");
        Debug.Log($"치명타확률 : {criticalChance*100}%");
        Debug.Log($"이동속도 : {moveSpeed}");
    }

    /// <summary>
    /// wizard가 공격 행동을 할 때 파티클 재생하는 함수
    /// </summary>
    public void WeaponEffectEnable()
    {
        if (weaponPS != null)
        {
            weaponPS.Play();    // 파티클 이팩트 재생 시작
        }
    }

    /// <summary>
    /// wizard가 공격 행동이 끝날 때 파티클 멈추는 함수
    /// </summary>
    public void WeaponEffectDisable()
    {
        if (weaponPS != null)
        {
            weaponPS.Stop();    // 파티클 이팩트 중지
        }
    }


    public override void Attack(IBattle target) => base.Attack(target);

    public override void TakeDamage(float damage) => base.TakeDamage(damage);

    public override void Die()
    {
        base.Die();
        inven.PlayerRemoveItem(); // 죽으면 아이템 1개 삭제
    }

    public override void Recover() => base.Recover();

}
