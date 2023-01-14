using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "New ItemData Atk", menuName = "ScriptableObject/ItemData_Atk", order = 1)]
public class ItemData_Atk : ItemData_Base
{
    public float atkBonus;      // 기본스탯기반 추가 공격력
    public float otherAtkBonus; // 추가 공격력
    public float criticalBonus; // 추가 치명타 확률

    float OtherAtkBonus => otherAtkBonus;

    float AtkBonus => atkBonus * 0.01f;
    float CriticalBonus => criticalBonus * 0.01f;


    private void Awake()
    {
        itemType = ItemType.Atk;  // 유물 타입은 공격 계열
    }


    public override void Effect(StatManager statManager)
    {
        float newAtk = statManager.Default_Atk * AtkBonus;
        float newCri = CriticalBonus;

        statManager.extra_Atk += otherAtkBonus;
        statManager.extra_Atk += newAtk;
        statManager.extra_CriticalChance += newCri;
    }
}
