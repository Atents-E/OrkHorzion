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
        HP_Text.text = $"{character.HP:f0} / {character.MaxHP:f0}";
    }

    /// <summary>
    /// HP가 변경시 실행되는 함수
    /// </summary>
    /// <param name="ratio"></param>
    private void OnHealthChange(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0, 1);
        slider.value = ratio;
    }

    /// <summary>
    /// maxHP가 변경시 실행되는 함수 
    /// </summary>
    /// <param name="ratio"></param>
    void RefreshHPBarText(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0, maxHP);
        slider.value = ratio;

    }

}