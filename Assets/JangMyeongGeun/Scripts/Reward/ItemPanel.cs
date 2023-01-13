using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour, IPointerClickHandler
{
    Image panel;
    Image itemImage;
    TextMeshProUGUI itemName;
    TextMeshProUGUI itemDescription;

    public ItemData_Base itemData;
    public int goldValue;

    RewardPanel rewardPanel;

    public Action<ItemPanel> onClick;

    public bool isSelect = false;

    Color panelColor = new Color(194 / 255f, 193 / 255f, 191 / 255f, 255 / 255f);    // 기본 패널 색
    Color selectColor = new Color(255 / 255f, 208 / 255f, 0 / 255f, 255 / 255f);     // 선택했을 때 나오는 패널 색
    public Sprite goldIcon;

    private void Awake()
    {
        panel = GetComponent<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemDescription = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        rewardPanel = GetComponentInParent<RewardPanel>();
    }

    private void Start()
    {
        itemData = null;
        goldValue = -1;
    }


    public void RefreshPanel(ItemData_Base itemData)
    {
        goldValue = -1;
        this.itemData = itemData;
        panel.color = panelColor;
        itemImage.sprite = itemData.itemIcon;
        itemName.text = itemData.itemName;
        itemDescription.text = itemData.description;
    }

    public void RefreshGoldPanel(int goldValue)
    {
        itemData = null;
        this.goldValue = goldValue;
        panel.color = panelColor;
        itemImage.sprite = rewardPanel.goldImage;
        itemName.text = "골드";
        itemDescription.text = $"{goldValue}골드";
    }


    public void SelectPanel()
    {
        panel.color = selectColor;
    }

    public void UnSelectPanel()
    {
        panel.color = panelColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(this);
        if (isSelect)
        {
            SelectPanel();
        }
        else
        {
            UnSelectPanel();
        }
    }
}
