using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenuButton : MonoBehaviour
{
    TowerMenuPanel towerMenu;
    InventoryUI invenUI;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        towerMenu = GameManager.Inst.TowerMenuPanel;
        invenUI = GameManager.Inst.InventoryUI;
        button.onClick.AddListener(() => InventoryOpenClose(towerMenu.IsOpen));
    }
    private void InventoryOpenClose(bool isOpen)
    {
        if (isOpen)
        {
            towerMenu.Close();
        }
        else
        {
            towerMenu.Open();
        }
        if (invenUI.IsOpen)
        {
            invenUI.InventoryOpenClose(invenUI.IsOpen);
        }
    }
}
