using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "New ItemData Buff", menuName = "ScriptableObject/ItemData_Buff", order = 2)]
public class ItemData_Buff : ItemData_Base
{
    public float moveSpeedBonus;      // 추가 이동속도

    float MoveSpeedBonus => moveSpeedBonus * 0.01f;

    private void Awake()
    {
        itemType = ItemType.Buff;  // 유물 타입은 디버프 계열
    }

    /// <summary>
    /// 유물 기능 함수
    /// </summary>
    /// <param name="player">플레이어</param>
    /// <param name="stackCount">현재 유물 소지량</param>
    public override void Effect(StatManager statManager)
    {
        float newMoveSpeed = statManager.Default_MoveSpeed * MoveSpeedBonus;
        statManager.extra_MoveSpeed += newMoveSpeed;
    }
}
