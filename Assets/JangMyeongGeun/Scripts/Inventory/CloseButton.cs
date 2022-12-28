using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    Button closeButton;
    InventoryUI invenUI;

    private void Awake()
    {
        closeButton = GetComponent<Button>();
        invenUI = GetComponentInParent<InventoryUI>();
    }

    private void Start()
    {
        closeButton.onClick.AddListener(invenUI.Close);
    }
}
