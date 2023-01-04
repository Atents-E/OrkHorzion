using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    /// <summary>
    /// 기본 사이즈
    /// </summary>
    public const int Default_Inventory_Size = 4;

    /// <summary>
    /// 지정한 새로운 최소 사이즈
    /// </summary>
    private int currentInventorySize = 0;
    
    /// <summary>
    /// 아이템을 담을 인벤토리 슬롯 리스트
    /// </summary>
    private List<ItemSlot> slots;

    /// <summary>
    /// 슬롯 개수 확인용
    /// </summary>
    public int SlotCount => slots.Count;

    /// <summary>
    /// 게임 매니저가 가지는 아이템 데이터 캐싱용
    /// </summary>
    ItemManager itemManager;

    InventoryUI invenUI;

    /// <summary>
    /// 특정 번째의 ItemSlot을 돌려주는 인덱서
    /// </summary>
    /// <param name="index">돌려줄 슬롯의 위치</param>
    /// <returns>index번째에 있는 ItemSlot</returns> 
    public ItemSlot this[int index] => slots[index];

    public Action onRefreshSlot;

    public Action<ItemData_Base> onAddSlotItemEffect;
    public Action<ItemData_Base, uint> onSubSlotItemEffect;
    

    /// <summary>
    /// 인벤토리 생성자
    /// </summary>
    /// <param name="size">인벤토리 기본 사이즈 지정값</param>
    public Inventory(int size = Default_Inventory_Size)
    {
        currentInventorySize = size;

        slots = new List<ItemSlot>(size);
        for (int i = 0; i < size; i++)
        {
            slots.Add(new ItemSlot());
        }

        itemManager = GameManager.Inst.ItemData;
        invenUI = GameManager.Inst.InventoryUI;

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
            if (targetSlot.IncreaseSlotItem(targetSlot))
            {
                //data.Effect(GameManager.Inst.Player, targetSlot);
                onAddSlotItemEffect?.Invoke(targetSlot.ItemData);
            }

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

                //data.Effect(GameManager.Inst.Player, emptySlot);
                onAddSlotItemEffect?.Invoke(emptySlot.ItemData);
            }
            else
            {
                // 인벤토리가 꽉찼다.
            }
        }

        return result;
    }

    /// <summary>
    /// 아이템 갯수 지정해서 삭제하는 함수(현재는 사용 안하는 함수)
    /// </summary>
    /// <param name="slotIndex">삭제할 아이템 슬롯 인덱스</param>
    /// <param name="decreaseCount">아이템 지울 개수</param>
    /// <returns></returns>
    public bool RemoveItem(uint slotIndex, uint decreaseCount = 1)
    {
        bool result = false;
        if (IsValidSlotIndex(slotIndex))
        {
            ItemSlot slot = slots[(int)slotIndex];
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

        for (int i = 0; i < SlotCount; i++) 
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

    /// <summary>
    /// 특정 슬롯에서 아이템을 제거하는 함수
    /// </summary>
    /// <param name="slot">삭제할 아이템 슬롯</param>
    /// <returns>true면 성공, false면 실패</returns>
    public void RemoveItem(ItemSlot slot)
    {
        int removeSlotIndex = GetItemSlotIndex(slot);
        bool findSlot = removeSlotIndex != -1;

        if (findSlot)
        {
            onSubSlotItemEffect?.Invoke(slot.ItemData,slot.ItemCount);
            slots[removeSlotIndex].ClearSlotItem();
            AlignItemSlot(slot);
        }
        else
        { 
            // 잘못된 인덱스
        }
    }

    /// <summary>
    /// 찾으려는 아이템 슬롯을 찾아서 인덱스를 리턴하는 함수
    /// </summary>
    /// <param name="slot">아이템 슬롯</param>
    /// <returns>찾았으면 해당 슬롯의 index, 못찾았으면 -1</returns>
    public int GetItemSlotIndex(ItemSlot slot)
    {
        int result = -1;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].ItemData.id == slot.ItemData.id)
            {
                result = i;
                return result;
            }
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



    /// <summary>
    /// 슬롯을 추가하는 함수
    /// </summary>
    /// <param name="amount">추가할 크기, 기본값은 1</param>
    public void IncreaseSlot(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            slots.Add(new ItemSlot());
        }
        onRefreshSlot?.Invoke();
    }

    /// <summary>
    /// 슬롯을 제거하는 함수
    /// </summary>
    /// <param name="amount">추가할 크기, 기본값은 1</param>
    public void DecreaseSlot(int amount = 1)
    {
        int slotEndIndex = SlotCount - 1;
        if ((SlotCount - amount) >= currentInventorySize)
        {
            for (int i = 0; i < amount; i++)
            {
                slots.RemoveAt(slotEndIndex);
                slotEndIndex--;
            }
            onRefreshSlot?.Invoke();
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                if (SlotCount != currentInventorySize)
                {
                    slots.RemoveAt(slotEndIndex);
                    slotEndIndex--;
                }
                else
                {
                    Debug.Log($"현재 최소 슬롯{currentInventorySize}칸이기에 더이상 삭제가 불가능합니다.");
                    break;
                }
            }
            onRefreshSlot?.Invoke();
        }
    }

    /// <summary>
    /// 아이템을 없애면 아이템을 가지고 있는 슬롯만 위로 정렬하는 함수
    /// </summary>
    /// <param name="slot">없어질 아이템을 가지고있는 슬롯</param>
    public void AlignItemSlot(ItemSlot slot)
    {
        slots.Remove(slot);
        slots.Add(new ItemSlot());
        onRefreshSlot?.Invoke();
    }

    public List<ItemSlot> GetInventorySlotList()
    {
        return slots;
    }

}
