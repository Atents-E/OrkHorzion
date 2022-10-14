using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Relic_Test : MonoBehaviour  
{
    protected enum relicType { Atk, Debuff, Gold, Heal, Special };                                      // 유물 타입
    protected string relicName;                                                                         // 유물 이름
    protected int relicID;                                                                              // 유물 고유 ID
    protected Sprite[] relicImage;                                                                      // 유물 이미지
    protected bool haveTheRelic = false;                                                                // 유물 소유 여부
    protected bool stackUp = false;                                                                     // 유물 스택 가능 여부
    protected int currentStack = 0;                                                                     // 유물 현재 스택

    Player player;

    protected virtual void GenerateRelics(string name, int id, bool isStack) 
    {
        relicName = name;   
        relicID = id;
        stackUp = isStack;
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }





}
