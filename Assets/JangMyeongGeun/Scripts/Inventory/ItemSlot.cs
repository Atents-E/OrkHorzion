using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot
{
    /// <summary>
    /// 이 슬롯에 들어있는 아이템
    /// </summary>
    ItemData_Base slotItemData = null;

    /// <summary>
    /// 이 슬롯에 들어있는 아이템 데이터
    /// </summary>
    public ItemData_Base ItemData
    {
        get => slotItemData;
        private set
        {
            if (slotItemData != value)
            { 
                slotItemData = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    /// <summary>
    /// 이 슬롯에 들어있는 아이템 갯수
    /// </summary>
    uint itemCount = 0;

    public Action onSlotItemChange;

    /// <summary>
    /// 이 슬롯에 들어있는 아이템 갯수
    /// </summary>
    public uint ItemCount
    { 
        get => itemCount;
        private set
        {
            if (itemCount != value)
            {
                itemCount = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    /// <summary>
    /// 이 슬롯이 비었는지 여부(true면 비었고, false면 무엇인가 들어있다.)
    /// </summary>
    public bool IsEmpty => (slotItemData == null);

    /// <summary>
    /// 이 슬롯에 지정된 아이템을 지정된 갯수로 넣는 함수
    /// </summary>
    /// <param name="data">추가할 아이템</param>
    /// <param name="count">설정된 갯수</param>
    public void AssignSlotItem(ItemData_Base data, uint count = 1)
    {
        if (data != null)
        {
            ItemCount = count;
            ItemData = data;
        }
        else
        {
            ClearSlotItem();
        }
    }

    /// <summary>
    /// 아이템 비우는 함수
    /// </summary>
    public void ClearSlotItem()
    {
        slotItemData = null;
        ItemCount = 0;
    }

    /// <summary>
    /// 이 슬롯에 아이템 갯수를 증가시키는 함수
    /// </summary>
    /// <param name="count">증가시킬 아이템 갯수</param>
    /// <param name="itemSlot">아이템 최대 소지 개수 확인용</param>
    public bool IncreaseSlotItem(ItemSlot itemSlot , uint count = 1)
    {
        if (itemSlot.ItemData.maxStackCount > ItemCount)
        {
            // 아이템이 안꽉차면
            ItemCount += count;
            if (itemSlot.ItemData.maxStackCount < ItemCount)
            {
                ItemCount = itemSlot.ItemData.maxStackCount;
            }
            return true;
        }
        else
        {
            // 아이템이 꽉차면
            Debug.Log("아이템이 꽉차서 더 추가가 불가능합니다");
            return false;
        }
    }

    /// <summary>
    /// 이 슬롯에 아이템 갯수를 감소시키는 함수
    /// </summary>
    /// <param name="count">감소시킬 아이템 갯수</param>
    public void DecreaseSlotItem(uint count = 1)
    {
        if (!IsEmpty)
        {
            int newCount = (int)ItemCount - (int)count;

            if (newCount < 1)
            {
                ClearSlotItem();
            }
            else
            {
                ItemCount = (uint)newCount;
            }
        }
    }

}
