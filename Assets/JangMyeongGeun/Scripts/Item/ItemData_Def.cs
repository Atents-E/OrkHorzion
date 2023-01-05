using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "New ItemData Def", menuName = "ScriptableObject/ItemData_Def", order = 5)]
public class ItemData_Def : ItemData_Base
{
    public float defBonus;      // 추가 방어력

    float DefBonus => defBonus * 0.01f;

    private void Awake()
    {
        itemType = ItemType.Def;  // 유물 타입은 방어 계열   
    }

    public override void Effect(StatManager statManager)
    {
        float newDef = statManager.Default_Def * DefBonus;

        statManager.extra_Def += newDef;
    }

    public override void RemoveEffect(StatManager statManager)
    {
        float newDef = statManager.Default_Def * DefBonus;

        statManager.extra_Def += (newDef * maxStackCount);
    }
}
