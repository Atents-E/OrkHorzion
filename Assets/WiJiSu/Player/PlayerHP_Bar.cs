using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP_Bar : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI HP_Text;
    string maxHP_Text;
    float maxHP;
    float Hp;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        HP_Text = GetComponentInChildren<TextMeshProUGUI>();    
    }

    private void Start()
    {
        Character character = GameManager.Inst.Character;
        maxHP = character.MaxHP;
        Hp = character.HP;
        slider.value = 1;
        HP_Text.text = $"{Hp} / {maxHP}";
        character.onHealthChange += OnHealthChange;
        character.onMaxHealthChange += (maxHP) => { this.maxHP = maxHP; RefreshHPBarText(Hp, maxHP); }; 
    }

    private void OnHealthChange(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0, 1);
        slider.value = ratio;

        float hp = maxHP * ratio;
        Hp = hp;
        RefreshHPBarText(hp, maxHP);
    }

    void RefreshHPBarText(float hp, float maxHP)
    {
        HP_Text.text = $"{hp:f0} / {maxHP:f0}";
    }



}
