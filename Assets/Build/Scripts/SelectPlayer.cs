using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// 버튼으로 플레이어를 선택하면 선택 된 플레이어의 발밑이 환해진다.
/// </summary>
public class SelectPlayer : MonoBehaviour
{
    public bool wizardSelect = false;
    public bool warriorSelect = false;

    Button wizardButton;
    Button warriorButton;

    Button selectButton;

    SpriteRenderer warrior_Sprite;
    SpriteRenderer wiazrd_Sprite;

    Color selectColor;

    private void Awake()
    {
        wizardButton = transform.GetChild(0).GetComponent<Button>();
        warriorButton = transform.GetChild(1).GetComponent<Button>();
        selectButton = transform.GetChild(3).GetComponent<Button>();

        warrior_Sprite = GameObject.Find("Player_Warrior").transform.GetChild(2).GetComponent<SpriteRenderer>();
        wiazrd_Sprite = GameObject.Find("Player_Wizard").transform.GetChild(2).GetComponent<SpriteRenderer>();

        selectColor = selectButton.image.color;
        selectButton.image.color = Color.clear;
        selectButton.interactable = false;

        wizardButton.onClick.AddListener( () =>
        {
            WizardSelec();
            if(wizardSelect)
            {
                wiazrd_Sprite.gameObject.SetActive(true);
                warrior_Sprite.gameObject.SetActive(false);
            }
        });

        warriorButton.onClick.AddListener( () =>
        {
            WarriorSelec();
            if (warriorSelect)
            {
                warrior_Sprite.gameObject.SetActive(true);
                wiazrd_Sprite.gameObject.SetActive(false);
            }
        });

        selectButton.onClick.AddListener(() =>
        {
            if (warriorSelect)
            {
                // warrior씬
                SceneManager.LoadScene("Warrior");
            }
            else
            {
                // wizard씬
            }

        });
    }

    private void Start()
    {
        warrior_Sprite.gameObject.SetActive(false);
        wiazrd_Sprite.gameObject.SetActive(false);
    }


    void WizardSelec()
    {
        wizardSelect = true;
        if (warriorSelect)
        {
            warriorSelect = false;
        }
        selectButton.image.color = selectColor;
        selectButton.interactable = true;
    }


    void WarriorSelec()
    {
        warriorSelect = true;
        if (wizardSelect)
        {
            wizardSelect = false;
        }
        selectButton.image.color = selectColor;
        selectButton.interactable = true;
    }


}
