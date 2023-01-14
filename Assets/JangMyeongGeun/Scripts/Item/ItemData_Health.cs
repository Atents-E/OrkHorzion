using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "New ItemData Health", menuName = "ScriptableObject/ItemData_Health", order = 4)]
public class ItemData_Health : ItemData_Base
{
    public float hpBonus;      // 추가 체력

    private void Awake()
    {
        itemType = ItemType.Health;  // 유물 타입은 체력 계열
    }

    /// <summary>
    /// 유물 기능 함수
    /// </summary>
    /// <param name="player">플레이어</param>
    public override void Effect(StatManager statManager)
    {
        //Debug.Log("체력용1 아이템 획득");
        //player.MAXHP += hpBonus; 
        //Debug.Log($"체력 : {player.MAXHP}");
        statManager.extra_MaxHp += hpBonus;
    }
}