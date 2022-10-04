using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public float moveSpeed = 1.0f;
    public int monsterHp = 100;

    public float AttackDemage = 10.0f;
    public float AttackDelay = 5.0f;

    Rigidbody rigid;

    Animator anim;
    
    bool isMove = false;

    public int MonsterHP
    {
        get => monsterHp;
        set
        {
            monsterHp = value;

            if (monsterHp < 0)
            { 
                Die();
            }
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        AttackFalse();
        isMove = false;
    }

    private void FixedUpdate()
    {
        if (!isMove)
        {
            rigid.MovePosition(transform.position + moveSpeed * Time.fixedDeltaTime * transform.forward);
            MonsterHP -= 10;
        }
        else
        {
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AttackTrue();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AttackFalse();
        }
    }

    void Die()
    {
        anim.SetTrigger("Die");
        isMove = true;
        Destroy(gameObject,2.0f);
    }

    void AttackTrue()
    {
        isMove = true;
        anim.SetBool("Attack",true);
    }
    void AttackFalse()
    {
        isMove = false;
        anim.SetBool("Attack", false);
    }

}
