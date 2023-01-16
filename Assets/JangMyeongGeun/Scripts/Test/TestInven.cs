using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestInven : MonoBehaviour
{
    Warrior player;
    InventoryUI invenUI;

    Button[] buttons;
    private void Start()
    {
        player = FindObjectOfType<Warrior>();
        invenUI = FindObjectOfType<InventoryUI>();
        buttons = GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(()=>player.inven.AddItem(ItemIDCode.ATK1));
        buttons[1].onClick.AddListener(()=>player.inven.AddItem(ItemIDCode.ATK2));
        buttons[2].onClick.AddListener(()=>player.inven.AddItem(ItemIDCode.ATK3));
        buttons[3].onClick.AddListener(()=>player.inven.AddItem(ItemIDCode.DEF1));
        buttons[4].onClick.AddListener(()=>player.inven.AddItem(ItemIDCode.Buff1));
        buttons[5].onClick.AddListener(()=>player.inven.AddItem(ItemIDCode.Health1));
        buttons[6].onClick.AddListener(()=>player.inven.IncreaseSlot());
        buttons[7].onClick.AddListener(()=>player.inven.DecreaseSlot());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
