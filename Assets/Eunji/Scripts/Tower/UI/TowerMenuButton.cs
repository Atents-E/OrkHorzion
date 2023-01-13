using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenuButton : MonoBehaviour
{
    TowerMenuPanel towerMenu;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        towerMenu = GameManager.Inst.TowerMenuPanel;
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
    }
}
