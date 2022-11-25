using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : EnemyBase
{
    Animator anim;
    Enemy_Navigation nav;

    DragonState state;  // 현재 드래곤의 상태

    protected enum DragonState
    {
        Move = 0,
        Attack,
        Die
    }

    Action stateUpdate;

    protected DragonState State
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;

                switch (state)
                {
                    case DragonState.Move:
                        anim.SetBool("IsMove", true);
                        
                        stateUpdate = Update_Move;
                        break;
                    case DragonState.Attack:
                        nav.Stopped(true);
                        anim.SetBool("IsMove", false);
                        anim.SetTrigger("Attack");
                        stateUpdate = Update_Attack;
                        break;
                    case DragonState.Die:
                        stateUpdate = Update_Die;
                        break;
                }
            }
        }
    }


    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<Enemy_Navigation>();
    }

    private void Update_Move()      // 움직임 조정
    {
        
    }
    private void Update_Attack()    // 공격용
    {
        
    }

    private void Update_Die()       // 죽을 때
    {
        
    }

}
