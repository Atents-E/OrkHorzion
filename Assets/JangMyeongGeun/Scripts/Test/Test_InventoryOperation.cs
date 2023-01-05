using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_InventoryOperation : MonoBehaviour
{
    InventoryUI invenUI;

    Test_Inventory test_inven;

    Button openCloseButton;
    Button addSlotButton;
    Button removeSlotButton;

    private void Awake()
    {
        invenUI = FindObjectOfType<InventoryUI>();
        test_inven = FindObjectOfType<Test_Inventory>();
        openCloseButton = transform.GetChild(0).GetComponent<Button>();
        addSlotButton = transform.GetChild(1).GetComponent<Button>();
        removeSlotButton = transform.GetChild(2).GetComponent<Button>();
    }

    private void Start()
    {
        openCloseButton.onClick.AddListener(() => InventoryOpenClose(invenUI.IsOpen));
        addSlotButton.onClick.AddListener(() =>test_inven.inven.IncreaseSlot());
        removeSlotButton.onClick.AddListener(() => test_inven.inven.DecreaseSlot());
    }

    private void InventoryOpenClose(bool isOpen)
    {
        if (isOpen)
        {
            invenUI.Close();
        }
        else
        {
            invenUI.Open();
        }
    }
}
