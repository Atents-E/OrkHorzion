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
        anim = GetComponent<Animator>();        // �ִϸ�����
    }
    protected void Start()
    {
        SearchPlayer();         // �÷��̾� ã��
    }

    protected void Update()
    {
        Looktarget();           // �÷��̾����� �Ĵٺ���.
    }


    private void OnTriggerEnter(Collider other)     // Ʈ���� �ȿ� ������ ��
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("����");
            looktargetOn = true;
            playerTarget = other.transform;         // playerTarget�� null�� �ƴϰ� �Ǿ���.
            anim.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit(Collider other)      // Ʈ���Ÿ� ���� ������ ��
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("����");
            playerTarget = null;                    // playerTarget�� null�� �Ǿ���.
            looktargetOn = false;
            anim.SetBool("Attack", false);
        }
    }



}
