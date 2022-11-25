using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory
{
    // 기본 사이즈
    public const int Default_Inventory_Size = 4;

    ItemSlot[] slots = null;

    public int SlotCount => slots.Length;


    /// <summary>
    /// 게임 매니저가 가지는 아이템 데이터 캐싱용
    /// </summary>
    ItemManager itemManager;

    /// <summary>
    /// 특정 번째의 ItemSlot을 돌려주는 인덱서
    /// </summary>
    /// <param name="index">돌려줄 슬롯의 위치</param>
    /// <returns>index번째에 있는 ItemSlot</returns>
    public ItemSlot this[uint index] => slots[index];

    public Inventory(int size = Default_Inventory_Size)
    {
        slots = new ItemSlot[size];
        for (int i = 0; i < size; i++)
        {
            slots[i] = new ItemSlot((uint)i);
        }

        itemManager = GameManager.Inst.ItemData;
    }

    // 아이템 추가
    /// <summary>
    /// 아이템을 인벤토리에 1개 추가하는 함수
    /// </summary>
    /// <param name="data">추가될 아이템</param>
    /// <returns>성공여부 true면 성공, false면 실패</returns>
    public bool AddItem(ItemIDCode code)
    {
        return AddItem(itemManager[code]);
    }

    /// <summary>
    /// 아이템을 인벤토리에 1개 추가하는 함수
    /// </summary>
    /// <param name="data">추가될 아이템</param>
    /// <returns>성공여부 true면 성공, false면 실패</returns>
    public bool AddItem(ItemData_Base data)
    {
        bool result = false;

        ItemSlot targetSlot = FindSameItem(data);
        if (targetSlot != null)
        {
            // 같은 아이템이라면
            targetSlot.IncreaseSlotItem(targetSlot);
            result = true;
        }
        else
        {
            // 같은 아이템이 아니라면

            ItemSlot emptySlot = FindEmptySlot();
            if (emptySlot != null)
            {
                // 비어있는 슬롯을 찾았다.
                emptySlot.AssignSlotItem(data);
            }
            else
            {
                // 인벤토리가 꽉찼다.
            }
        }

        return result;
    }

    public bool RemoveItem(uint slotIndex, uint decreaseCount = 1)
    {
        bool result = false;
        if (IsValidSlotIndex(slotIndex))
        {
            ItemSlot slot = slots[slotIndex];
            slot.DecreaseSlotItem(decreaseCount);
            result = true;
        }
        else
        { 
            // 받아온 인덱스값은 잘못된 인덱스이다.
        }


        return result;
    }

    /// <summary>
    /// 인벤토리에 파라메터와 같은 종류의 아이템이 있는지 찾아보는 함수
    /// </summary>
    /// <param name="itemData">찾을 아이템</param>
    /// <returns>찾았으면 null이 아닌값(찾는 아이템이 들어있는 슬롯), 찾지 못했으면 null</returns>
    private ItemSlot FindSameItem(ItemData_Base itemData)
    {
        ItemSlot findslot = null;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ItemData == itemData)
            {
                findslot = slots[i];
                break;
            }
        }

        return findslot;
    }

    /// <summary>
    /// 비어있는 슬롯을 찾는 함수
    /// </summary>
    /// <returns>비어있는 함수를 찾으면 null이 아니고 비어있는 함수가 없으면 null</returns>
    private ItemSlot FindEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                return slot;
            }
        }
        return null;
    }

    /// <summary>
    /// 파라메터로 받은 인덱스가 적절한 인덱스인지 판단하는 함수
    /// </summary>
    /// <param name="index">확인할 인덱스</param>
    /// <returns>true면 사용가능한 인덱스, false면 사용불가능한 인덱스</returns>

    private bool IsValidSlotIndex(uint index) => (index < SlotCount);

    // 아이템 버리기

    /// <summary>
    /// 특정 슬롯에서 아이템을 제거하는 함수
    /// </summary>
    /// <param name="slotIndex">아이템을 제거할 함수</param>
    /// <returns>true면 성공, false면 실패</returns>
    public bool ClearItem(uint slotIndex)
    {
        bool result = false;

        if (IsValidSlotIndex(slotIndex))
        {
            ItemSlot slot = slots[slotIndex];
            slot.ClearSlotItem();
        }
        else
        { 
            // 잘못된 인덱스
        }

        return result;
    }

    /// <summary>
    /// 인벤토리의 모든 아이템을 비우는 함수
    /// </summary>
    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlotItem();
        }
    }

}
