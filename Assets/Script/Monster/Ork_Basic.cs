using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ork_Basic : EnemyBase
{
    Animator anim;

    protected void Awake()
    {
        anim = GetComponent<Animator>();        // 애니메이터
    }
    protected void Start()
    {
        SearchPlayer();         // 플레이어 찾기
    }

    protected void Update()
    {
        Looktarget();           // 플레이어쪽을 쳐다본다.
    }


    private void OnTriggerEnter(Collider other)     // 트리거 안에 들어왔을 때
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("들어옴");
            looktargetOn = true;
            playerTarget = other.transform;         // playerTarget이 null이 아니게 되었다.
            anim.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit(Collider other)      // 트리거를 빠져 나갔을 때
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("나감");
            playerTarget = null;                    // playerTarget이 null이 되었다.
            looktargetOn = false;
            anim.SetBool("Attack", false);
        }
    }



}
