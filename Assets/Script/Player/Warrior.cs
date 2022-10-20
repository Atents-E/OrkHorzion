using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    public int  default_Hp = 100; // 체력
    public int default_MaxHp = 100; // 최대 체력
    public int default_Def = 20; // 방어력
    public int default_Atk = 80; // 공격력
    public float default_AtkSpeed = 40.0f; // 공격 속도
    public float default_MoveSpeed = 30.0f; //이동 속도


    private void Awake()
    {
        characterName = "전사";
        maxHp = default_MaxHp;
        hp = maxHp;
        def = default_Def;
        atk = default_Atk;
        atkSpeed = default_AtkSpeed;
        moveSpeed = default_MoveSpeed;
    }

    private void Start()
    {
        Debug.Log($"이름 : {characterName}");
        Debug.Log($"체력 : {hp} / {maxHp}");
        Debug.Log($"방어력 : {def}");
        Debug.Log($"공격력 : {atk}");
        Debug.Log($"공격속도 : {atkSpeed}");
        Debug.Log($"이동속도 : {moveSpeed}");
    }

    void Update()
    {
        
    }
}
