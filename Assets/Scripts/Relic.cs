using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Relic : MonoBehaviour
{
    protected string[] relicType = { "공격계열", "디버프계열", "재화계열", "회복계열", "특수계열" };    // 유물 타입
    protected string relicName;                                                                         // 유물 이름
    protected int relicID;                                                                              // 유물 고유 ID
    protected Sprite relicImage;                                                                        // 유물 이미지
    protected bool haveTheRelic = false;                                                                // 유물 소유 여부
    protected bool isStack = false;                                                                     // 유물 스택 가능 여부
    protected int currentStack = 0;                                                                     // 유물 현재 스택

    Character player;

    private void Awake()
    {
        player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        Debug.Log(player.HP);
        Debug.Log(player.MAXHP);
        Debug.Log(player.ATK);
        Debug.Log(player.DEF);
        Debug.Log(player.ATKSpeed);
        Debug.Log(player.MoveSpeed);
    }

}
