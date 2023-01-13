using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    InventoryUI invenUI;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        invenUI = GameManager.Inst.InventoryUI;
        button.onClick.AddListener(() => InventoryOpenClose(invenUI.IsOpen));
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
