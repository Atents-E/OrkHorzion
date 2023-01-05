using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDeletionUI : MonoBehaviour
{
    Image itemImage;
    TextMeshProUGUI itemNameText;
    TextMeshProUGUI itemCountText;
    Button cancel;
    Button accept;
    ItemSlot removeSlot = null;

    /// <summary>
    /// 삭제 문구창 열렸는지 확인할 수 있는 변수
    /// </summary>
    bool isOpen = false;

    /// <summary>
    /// bool변수 확인용
    /// </summary>
    public bool IsOpen
    {
        get => isOpen;
        private set
        {
            if (isOpen != value)
            {
                isOpen = value;
            }
        }
    }

    /// <summary>
    /// 아이템을 지울 때 사용하는 델리게이트
    /// </summary>
    public Action<ItemSlot> onRemoveSlotItem;


    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemNameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemCountText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        cancel = transform.GetChild(5).GetComponent<Button>();
        cancel.onClick.AddListener(Close);

        accept = transform.GetChild(6).GetComponent<Button>();
        accept.onClick.AddListener(()=> { RemoveSlotItem(removeSlot); });
    }

    private void Start()
    {
        Close();    // 우선 시작하면 삭제문구창 닫아 놓기
    }

    /// <summary>
    /// 삭제문구창 여는 함수
    /// </summary>
    /// <param name="itemSlot">삭제할 아이템을 가지고 있는 아이템 슬롯</param>
    public void Open(ItemSlot itemSlot)
    {
        isOpen = true;
        this.gameObject.SetActive(true);
        removeSlot = itemSlot;
        itemImage.sprite = itemSlot.ItemData.itemIcon;
        itemNameText.text = itemSlot.ItemData.itemName;
        if (itemSlot.ItemData.maxStackCount > itemSlot.ItemCount)
        {
            itemCountText.text = $"X{itemSlot.ItemCount}";
        }
        else
        {
            itemCountText.text = "Max";
        }
    }

    /// <summary>
    /// 삭제문구창 닫는 함수
    /// </summary>
    public void Close()
    {
        isOpen = false;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 아이템을 삭제하는 함수
    /// </summary>
    /// <param name="itemSlot">삭제할 아이템을 가지고 있는 아이템 슬롯</param>
    public void RemoveSlotItem(ItemSlot itemSlot)
    {
        onRemoveSlotItem?.Invoke(itemSlot);

        Close();
    }
}
