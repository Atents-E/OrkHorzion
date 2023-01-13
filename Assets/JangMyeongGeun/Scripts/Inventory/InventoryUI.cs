using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // ItemSlotUI가 있는 프리팹. 인벤토리 크기 변화에 대비해서 가지고 있기
    public GameObject slotPrefab;

    Inventory inven;

    ItemSlotUI[] slotUIs;

    ConfirmDeletionUI removeUI;

    CanvasGroup canvasGroup;

    RewardPanel rewardPanel;

    /// <summary>
    /// 인벤토리창 열렸는지 확인할 수 있는 변수
    /// </summary>
    bool isOpen = false;

    /// <summary>
    /// bool변수 확인용
    /// </summary>
    public bool IsOpen
    {
        get => isOpen;
        set
        {
            if (isOpen != value)
            { 
                isOpen = value;
            }
        }
    }

    private void Awake()
    {
        slotUIs = GetComponentsInChildren<ItemSlotUI>();
        removeUI = GetComponentInChildren<ConfirmDeletionUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 입력받은 인벤토리에 맞게 각종 초기화 작업 및 갱신하는 함수
    /// </summary>
    /// <param name="playerInven">이 UI로 표시할 인벤토리</param>
    public void InitializeInventoy(Inventory playerInven)
    {
        inven = playerInven;
        rewardPanel = GameManager.Inst.RewardPanel;
        rewardPanel.GetPlayerInventory(playerInven);

        Close();

        if (Inventory.Default_Inventory_Size != inven.SlotCount)
        {
            // 기본 사이즈와 다르면 기존 슬롯을 전부 삭제하고 새로 만들기
            RefreshSlotUI();
        }
        else
        {
            // 지정한 사이즈와 기본 사이즈가 같으면 바로 슬롯UI들의 초기화 진행
            for (uint i = 0; i < inven.SlotCount; i++)
            {
                slotUIs[i].InitializeSlot(i, inven[(int)i]);

                slotUIs[i].onRemoveSlotOpen = OnRemoveUIOpen;
            }
        }

        // 삭제 문구창에서 아이템을 삭제할 때 데이터를 정렬시키도록 델리게이트 연결
        removeUI.onRemoveSlotItem = inven.RemoveItem;    
        // 인벤토리 데이터를 새로 갱신할 때 UI도 같이 갱신되도록 델리게이트 연결
        inven.onRefreshSlot = RefreshSlotUI;
    }

    /// <summary>
    /// 기존 슬롯 다시 지우고 오브젝트를 새로 만드는 함수
    /// </summary>
    private void RefreshSlotUI()
    {
        for (int i = 0; i < slotUIs.Length; i++)
        {
            Destroy(slotUIs[i].gameObject);
        }

        Transform slotParent = transform.GetChild(1).GetChild(0);
        slotUIs = new ItemSlotUI[inven.SlotCount];
        for (uint i = 0; i < inven.SlotCount; i++)
        {
            GameObject obj = Instantiate(slotPrefab, slotParent);
            obj.name = $"{slotPrefab.name}_{i}";
            slotUIs[i] = obj.GetComponent<ItemSlotUI>();
            slotUIs[i].InitializeSlot(i, inven[(int)i]);

            slotUIs[i].onRemoveSlotOpen = OnRemoveUIOpen;
        }
    }

    /// <summary>
    /// 해당 슬롯에 있는 아이템을 지울 때 삭제 문구창이 뜨는 함수
    /// </summary>
    /// <param name="itemSlot">아이템을 없앨 슬롯</param>
    private void OnRemoveUIOpen(ItemSlot itemSlot)
    {
        removeUI.Open(itemSlot);
    }

    /// <summary>
    /// 인벤토리 UI를 여는 함수
    /// </summary>
    public void Open()
    {
        IsOpen = true;
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    /// <summary>
    /// 인벤토리 UI를 닫는 함수
    /// </summary>
    public void Close()
    {
        IsOpen = false;
        canvasGroup.alpha = 0.0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        if (removeUI.IsOpen)
        { 
            removeUI.Close();
        }
    }

    public void InventoryOpenClose(bool isOpen)
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
