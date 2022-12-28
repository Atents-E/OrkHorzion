using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "New ItemData Debuff", menuName = "ScriptableObject/ItemData_Debuff", order = 2)]
public class ItemData_Debuff : ItemData_Base
{
    private void Awake()
    {
        itemType = ItemType.Debuff;  // 유물 타입은 디버프 계열
    }

    /// <summary>
    /// 유물 기능 함수
    /// </summary>
    /// <param name="player">플레이어</param>
    /// <param name="stackCount">현재 유물 소지량</param>
    public override void Effect(Player player, ItemSlot slot)
    {
    }
}
