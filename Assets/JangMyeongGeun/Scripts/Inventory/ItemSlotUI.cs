using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ItemSlotUI : MonoBehaviour
{
    private uint id;    // 몇번쨰 슬롯인가?

    protected ItemSlot itemSlot;    // 이 UI와 연결된 ItemSlot

    private Image itemImage;
    private TextMeshProUGUI itemNameText;
    private TextMeshProUGUI itemDescriptionText;
    private TextMeshProUGUI itemCountText;
    private Button removeItemButton;

    public uint ID => id;
    public ItemSlot ItemSlot => itemSlot;

    ConfirmDeletionUI confirmDeletionUI;

    private void Awake()
    {
        itemImage = transform.GetChild(3).GetChild(0).GetComponent<Image>();
        itemNameText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        
        confirmDeletionUI = FindObjectOfType<ConfirmDeletionUI>();

        removeItemButton = transform.GetChild(2).GetComponent<Button>();
        removeItemButton.onClick.AddListener(() =>confirmDeletionUI.Open(ItemSlot));


        itemDescriptionText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemCountText = transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void InitializeSlot(uint id, ItemSlot slot)
    {
        this.id = id;
        this.itemSlot = slot;
        this.itemSlot.onSlotItemChange = Refresh;

        Refresh();
    }

    private void Refresh()
    {
        if (itemSlot.IsEmpty)
        {
            itemImage.sprite = null;
            itemImage.color = Color.clear;

            removeItemButton.gameObject.SetActive(false);

            itemNameText.text = "빈 슬롯";
            itemDescriptionText.text = "비어있는 슬롯입니다.";
            itemCountText.text = "";
        }
        else
        {
            itemImage.sprite = itemSlot.ItemData.itemIcon;
            itemImage.color = Color.white;

            removeItemButton.gameObject.SetActive(true);

            itemNameText.text = itemSlot.ItemData.itemName.ToString();
            itemDescriptionText.text = itemSlot.ItemData.description.ToString();

            if (itemSlot.ItemData.maxStackCount > itemSlot.ItemCount)
            {
                itemCountText.text = $"X{itemSlot.ItemCount}";
            }
            else
            {
                itemCountText.text = "Max";
                itemCountText.color = Color.red;
            }
        }
    }
    
}
