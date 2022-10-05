using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Item : MonoBehaviour
{
    protected string[] itemType = { "공격계열", "디버프계열", "재화계열", "회복계열", "특수계열" };    // 유물 타입
    protected string itemName;                                                                         // 유물 이름
    protected string itemid;                                                                           // 유물 고유 ID
    protected Sprite itemImage;                                                                        // 유물 이미지
    protected bool haveTheItem = false;                                                                // 유물 소유 여부
    protected bool stackitem = false;                                                                  // 유물 스택 가능 여부
    protected int currentStack = 0;                                                                    // 유물 현재 스택

    protected string characterName;
    protected bool isDead = false;
    protected int hp;
    public int HP 
    {
        get => hp;

        set 
        {
            hp = value;
            if (hp > maxHp) // 현재 체력이 최대체력 보다 높아지면 ex) 회복할 때
            {
                hp = maxHp; // 현재 체력을 최대체력과 같게 만들기
            }
            if (hp <= 0) // 현재 체력이 0 이거나 0 이하힐 때 ex) 현재 체력보다 더 큰 데미지를 입을 때
            {
                hp = 0; // 체력을 0으로 만들기
                Dead();
            }
        }
    }
    protected int maxHp;
    public int MAXHP 
    {
        get => maxHp;
        set 
        {
            // 나중에 유물효과로 최대체력 늘릴 때 쓰는 공간
            maxHp = value;
        }
    }
    protected float atk;
    protected float atkSpeed;
    protected float moveSpeed;

    private void Dead()
    {
        isDead = true;
    }




    









}
