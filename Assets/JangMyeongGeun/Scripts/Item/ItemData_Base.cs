using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 유물 타입들
/// </summary>
public enum ItemType
{
    Atk = 1,    // 공격 계열
    Debuff,     // 디버프 계열
    Gold,       // 재화 계열
    Health,     // 회복 계열
    Special,     // 특수 계열
    Def         // 방어 계열
}

public class ItemData_Base : ScriptableObject
{
    [Header("유물 기본 데이터")]
    public uint id = 0;          // 유물 고유 ID
    public ItemType itemType;     // 유물 타입
    public Sprite itemTypeIcon;    // 유물 타입 이미지
    public string itemName;        // 유물 이름
    public Sprite itemIcon;        // 유물 이미지

    [TextArea (15, 20)]
    public string description;      // 유물 설명
    public uint maxStackCount = 3;   // 유물 최대 소지량

    /// <summary>
    /// 유물 기능 함수
    /// </summary>
    /// <param name="player">플레이어</param>
    public virtual void Effect(StatManager statManager)
    {
        
    }

    public virtual void RemoveEffect(StatManager statManager)
    { 
    
    }
}

