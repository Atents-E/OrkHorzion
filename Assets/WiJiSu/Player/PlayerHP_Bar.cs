using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;

public class PlayerHP_Bar : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI HP_Text;
    string maxHP_Text;
    float maxHP;
    float Hp;
    Character character;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        HP_Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        character = GameManager.Inst.Character;
        maxHP = character.MaxHP;
        Hp = character.HP;
        slider.value = 1;
        HP_Text.text = $"{Hp} / {maxHP}";
        character.onHealthChange += OnHealthChange;
        character.onMaxHealthChange += RefreshHPBarText;
    }

    private void Update()
    {
        //OnHealthChange(character.HP);
        //RefreshHPBarText(Hp, maxHP);
        HP_Text.text = $"{character.HP:f0} / {character.MaxHP:f0}";
    }

    private void OnHealthChange(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0, 1);
        slider.value = ratio;

        //float hp = maxHP * ratio;
        //Hp = hp;
        //RefreshHPBarText(Hp, maxHP);
    }

    //void RefreshHPBarText(float hp, float maxHP)
    //{
    //    float aaa = Mathf.Clamp(hp, 0, maxHP);
    //    slider.value = aaa;

    //    HP_Text.text = $"{hp:f0} / {maxHP:f0}";
    //    //HP_Text.text = $"{hp:f0}{maxHP_Text}";
    //}

    void RefreshHPBarText(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0, maxHP);
        slider.value = ratio;

        //HP_Text.text = $"{hp:f0}{maxHP_Text}";
    }

}