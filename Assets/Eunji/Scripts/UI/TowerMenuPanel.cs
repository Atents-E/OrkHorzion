using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class TowerMenuPanel : MonoBehaviour
{
    CanvasGroup canvasGroup;

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
        private set
        {
            if (isOpen != value)
            {
                isOpen = value;
            }
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Close();
    }

    public void Open()
    {
        IsOpen = true;
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }


    public void Close()
    {
        IsOpen = false;
        canvasGroup.alpha = 0.0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

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

