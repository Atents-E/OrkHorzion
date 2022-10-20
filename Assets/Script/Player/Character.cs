using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected string characterName; // 캐릭터 이름
    public string CharacterName // 캐릭터 이름 프로퍼티
    {
        get => CharacterName;

        set
        {
            CharacterName = value;
        }
    }

    protected bool isDead = false; // 캐릭터 사망 확인용

    protected int hp; // 캐릭터 체력
    public int HP // 캐릭터 체력 프로퍼티
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
            }
        }
    }

    protected int maxHp; // 캐릭터 최대 체력
    public int MAXHP // 캐릭터 최대 체력 프로퍼티
    {
        get => maxHp;
        set
        {
            // 나중에 유물효과로 최대체력 늘릴 때 쓰는 공간
            maxHp = value;
        }
    }

    protected int def; // 캐릭터 방어력
    public int DEF // 캐릭터 방어력 프로퍼티
    {
        get => def;

        set
        {
            def = value;
        }
    }

    protected int atk; // 캐릭터 공격력
    public int ATK // 캐릭터 공격력 프로퍼티
    {
        get => atk;

        set
        {
            atk = value;
        }
    }

    protected float atkSpeed; // 캐릭터 공격속도
    public float ATKSpeed // 캐릭터 공격속도 프로퍼티
    {
        get => atkSpeed;

        set
        {
            atkSpeed = value;
        }
    }

    protected float moveSpeed; // 캐릭터 이동속도
    public float MoveSpeed // 캐릭터 이동속도 프로퍼티
    {
        get => moveSpeed;

        set
        {
            moveSpeed = value;
        }
    }

    private void Dead()
    {
        isDead = true;
    }


}