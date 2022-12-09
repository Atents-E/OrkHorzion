using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDeletionUI : MonoBehaviour
{
    private Image itemImage;
    private TextMeshProUGUI itemNameText;
    private TextMeshProUGUI itemCountText;
    Button cancel;
    Button accept;
    ItemSlot removeSlot = null;

    private void Awake()
    {

        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemNameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemCountText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        cancel = transform.GetChild(5).GetComponent<Button>();
        cancel.onClick.AddListener(Close);

        accept = transform.GetChild(6).GetComponent<Button>();
        accept.onClick.AddListener(() => RemoveSlotItem(removeSlot));
    }

    private void Start()
    {
        Close();
    }

    public void Open(ItemSlot itemSlot)
    {
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

    public void RemoveSlotItem(ItemSlot itemSlot)
    {
       itemSlot.ClearSlotItem();
       Close();
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
