using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "New ItemData Gold", menuName = "ScriptableObject/ItemData_Gold", order = 3)]
public class ItemData_Gold : ItemData_Base
{
    private void Awake()
    {
        itemType = ItemType.Gold;  // 유물 타입은 골드 계열
    }

    /// <summary>
    /// 유물 기능 함수
    /// </summary>
    /// <param name="player">플레이어</param>
    /// <param name="stackCount">현재 유물 소지량</param>
    public override void Effect(StatManager statManager)
    {
    }
}
