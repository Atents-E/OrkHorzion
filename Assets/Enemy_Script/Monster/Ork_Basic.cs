using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ork_Basic : EnemyBase
{
    Animator anim;
    Enemy_Navigation nav;
    CapsuleCollider ccoll;

    bool isDead = true;
    public float testHpCount = 5.0f;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();        // �ִϸ�����
        nav = GetComponent<Enemy_Navigation>();
        ccoll = GetComponent<CapsuleCollider>();
    }
    protected virtual void Start()
    {
        SearchPlayer();         // �÷��̾� ã��
        monsterHp = monsterMaxHp;

        onHealthChange += HP_Change;   
        onDie += Dead;
    }

    protected virtual void Update()
    {
        //HP -= testHpCount;    // �����Ӹ��� testHpCount��ŭ HP�� ���δ�

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

    protected virtual void Dead()
    {
        if (isDead)
        {
            isDead = false;
            nav.Stopped(true);
            anim.SetBool("Attack", false);
            anim.SetTrigger("Die");
            ccoll.enabled = false;
            nav.enabled = false;
            nav.IsEnabled();
            Destroy(this.gameObject, 2.0f);
            //Debug.Log("��ũ ����");
        }
    }

    void HP_Change(float ratio)
    {
        if (isDead)
        {
            //Debug.Log($"{gameObject.name}�� HP�� {HP}�� ���� �Ǿ����ϴ�.");
        }
    }
}
