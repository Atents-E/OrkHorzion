using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ork_Basic : EnemyBase
{
    Animator anim;
    Enemy_Navigation nav;
    CapsuleCollider ccoll;

    EnemySpawner spawn;

    bool isDead = true;

    float testTime = 0.0f;

    IBattle target;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();        // �ִϸ�����
        nav = GetComponent<Enemy_Navigation>();
        ccoll = GetComponent<CapsuleCollider>();

        spawn = FindObjectOfType<EnemySpawner>();
    }
    protected virtual void Start()
    {
        SearchPlayer();         // �÷��̾� ã��

        MaxHP += spawn.waveCount * 500.0f;
        HP = MaxHP;

        onHealthChange += HP_Change;   
        onDie += Dead;
    }

    
    protected virtual void Update()
    {
        //HP -= testHpCount;    // �����Ӹ��� testHpCount��ŭ HP�� ���δ�
        testTime += Time.deltaTime;
               

        Looktarget();           // �÷��̾����� �Ĵٺ���.
    }


    private void OnTriggerEnter(Collider other)     // Ʈ���� �ȿ� ������ ��
    {
        target = other.GetComponent<IBattle>();
        if (other.CompareTag("Player"))
        {
            Debug.Log("����");
            looktargetOn = true;
            playerTarget = other.transform;         // playerTarget�� null�� �ƴϰ� �Ǿ���.
            anim.SetBool("Attack", true);
            //Attack(target);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && target != null)
        {
            if (target != null && testTime > 1)
            {
                Debug.Log("�÷��̾� ����");
                Attack(target);
                testTime = 0.0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)      // Ʈ���Ÿ� ���� ������ ��
    {
        if (other.CompareTag("Player"))
        {
            target = null;
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
