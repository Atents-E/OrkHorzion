using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_InventoryButton : MonoBehaviour
{
    InventoryUI invenUI;
    Button invenButton;
    bool isOpen = false;

    private void Awake()
    {
        invenUI = FindObjectOfType<InventoryUI>();
        invenButton = GetComponent<Button>();
    }

    private void Start()
    {
        invenButton.onClick.AddListener( () => InventoryOpenClose(isOpen) );
    }

    private void InventoryOpenClose(bool openClose)
    {
        if (!openClose)
        {
            invenUI.Open();
            isOpen = true;
        }
        else
        {
            invenUI.Close();
            isOpen = false;
        }
    }
}
