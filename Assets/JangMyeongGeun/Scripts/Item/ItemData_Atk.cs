using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "New ItemData Atk", menuName = "ScriptableObject/ItemData_Atk", order = 1)]
public class ItemData_Atk : ItemData_Base
{
    public float atkBonus;      // 추가 공격력
    public float criticalBonus; // 추가 치명타 확률

    private void Awake()
    {
        itemType = ItemType.Atk;  // 유물 타입은 공격 계열
    }

    /// <summary>   
    /// 유물 기능 함수
    /// </summary>
    /// <param name="player">플레이어</param>
    /// <param name="stackCount">현재 유물 소지량</param>
    public override void Effect(Player player, ItemSlot slot)
    {
        Debug.Log("공격용1 아이템 획득");
        player.ATK += atkBonus;  // 현재 공격력에 추가 공격력 % 만큼 더해진다
        player.CriticalChance += criticalBonus; // 현재 치명타 확률에 추가 치명타 확률을 더한다
        Debug.Log($"공격력 : {player.ATK}, 치명타확률 : {player.CriticalChance}");
    }
}
