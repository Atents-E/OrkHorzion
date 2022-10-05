using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public float moveSpeed = 1.0f;
    public int monsterHp = 100;

    public float AttackRadius = 2.3f;
    public float AttackDemage = 10.0f;
    public float AttackDelay = 5.0f;
    float currentAngle = 0.0f;
    public float turnSpeed = 5.0f;

    Rigidbody rigid;

    Animator anim;

    Transform playerTarget = null;
    Vector3 monsterToplayerDir;

    bool isMove = true;
    bool looktargetOn = true;

    public int MonsterHP
    {
        get => monsterHp;
        set
        {
            monsterHp = value;

            if (monsterHp < 0)
            {
                monsterHp = 0;
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
        isMove = true;

        //SphereCollider coll = GetComponent<SphereCollider>();
        //coll.radius = AttackRadius;
    }

    private void FixedUpdate()
    {
        LookTarget();
        MonsterHP -= 10;
        if (isMove)
        {
            rigid.MovePosition(transform.position + moveSpeed * Time.fixedDeltaTime * transform.forward);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTarget = other.transform;
            
            AttackTrue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTarget = null;
            AttackFalse();
        }
    }

    void Die()
    {
        isMove = false;
        looktargetOn = false;
        anim.SetTrigger("Die");
        anim.SetBool("Attack", false);
        Destroy(gameObject,5.0f);
    }

    void AttackTrue()
    {
        isMove = false;
        anim.SetBool("Attack",true);
    }

    void AttackFalse()
    {
        isMove = true;
        anim.SetBool("Attack", false);
    }


    void LookTarget()
    {
        if (looktargetOn)
        {
            if (playerTarget != null)
            {

                monsterToplayerDir = playerTarget.position - transform.position;
                monsterToplayerDir.y = 0;

                float betweenAngle = Vector3.SignedAngle(transform.forward, monsterToplayerDir, transform.up);

                Vector3 resultDir;

                if (Mathf.Abs(betweenAngle) < 30.0f)
                {
                    float rotateDir = 1.0f;

                    if (betweenAngle < 0)
                    {
                        rotateDir = -1.0f;
                    }

                    currentAngle += (rotateDir * turnSpeed * Time.fixedDeltaTime);

                    resultDir = Quaternion.Euler(0, currentAngle, 0) * transform.forward;
                }
                else
                {
                    resultDir = monsterToplayerDir;
                }
                transform.rotation = Quaternion.LookRotation(resultDir);
            }

        }
    }
}
