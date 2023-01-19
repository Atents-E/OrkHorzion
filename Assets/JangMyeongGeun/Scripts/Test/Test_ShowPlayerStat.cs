using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test_ShowPlayerStat : MonoBehaviour
{
    Warrior player;
    TextMeshProUGUI[] stats;

    void Start()
    {
        player = GameManager.Inst.Warrior;
        stats = GetComponentsInChildren<TextMeshProUGUI>();
        stats[1].text = $"MAXHP : {player.MaxHP}";
        stats[2].text = $"Atk : {player.AttackPower}";
        stats[3].text = $"Critical % : {player.CriticalChance * 100:F0}%";
        stats[4].text = $"Def : {player.DefencePower}";
        stats[5].text = $"MoveSpeed : {player.MoveSpeed}";
    }

    // Update is called once per frame
    void Update()
    {
        stats[1].text = $"MaxHP : {player.MaxHP}";
        stats[2].text = $"Atk : {player.AttackPower}";
        stats[3].text = $"Critical % : {player.CriticalChance * 100:F0}%";
        stats[4].text = $"Def : {player.DefencePower}";
        stats[5].text = $"MoveSpeed : {player.MoveSpeed}";
    }
}
