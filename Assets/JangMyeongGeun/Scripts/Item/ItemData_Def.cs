using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "New ItemData Def", menuName = "ScriptableObject/ItemData_Def", order = 5)]
public class ItemData_Def : ItemData_Base
{
    public float defBonus;      // 추가 방어력

    private void Awake()
    {
        itemType = ItemType.Def;  // 유물 타입은 공격 계열   
    }

    /// <summary>   
    /// 유물 기능 함수
    /// </summary>
    /// <param name="player">플레이어</param>
    public override void Effect(Player player, ItemSlot slot)
    {
        Debug.Log("방어용1 아이템 획득");
        player.DEF += defBonus;  // 현재 공격력에 추가 공격력 % 만큼 더해진다
        Debug.Log($"방어력 : {player.DEF}");
    }
}
