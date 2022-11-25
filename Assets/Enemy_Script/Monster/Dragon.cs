using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : EnemyBase
{
    Animator anim;
    Enemy_Navigation nav;

    DragonState state;  // ���� �巡���� ����

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

    private void Update_Move()      // ������ ����
    {
        
    }
    private void Update_Attack()    // ���ݿ�
    {
        
    }

    private void Update_Die()       // ���� ��
    {
        
    }

}
