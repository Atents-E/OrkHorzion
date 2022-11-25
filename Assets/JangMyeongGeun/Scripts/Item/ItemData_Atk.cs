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
    public override void Effect(Player player, uint stackCount)
    {
        if (stackCount > maxStackCount) // 현재 유물 소지량이 3 이상이 되면
        {
            stackCount = maxStackCount; // 현재 유물 소지량은 최대 소지량으로 고정
        }

        atkBonus = (atkBonus * stackCount);             // 추가 공격력은 소지량만큼 추가로 곱해서 늘어난다   
        criticalBonus = (criticalBonus * stackCount);   // 추가 치명타 확률도 동일하다

        player.ATK += (player.ATK * atkBonus);  // 현재 공격력에 추가 공격력 % 만큼 더해진다
        player.CriticalChance += criticalBonus; // 현재 치명타 확률에 추가 치명타 확률을 더한다
    }
}
