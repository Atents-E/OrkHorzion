using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public int monsterHp = 5000;

    public float AttackRadius = 2.3f;
    public float AttackDemage = 10.0f;
    public float AttackDelay = 5.0f;
    public float turnSpeed = 5.0f;
    float currentAngle = 0.0f;

    Animator anim;

    Transform playerTarget = null;
    public Action<bool> OnMoving;

    Vector3 monsterToplayerDir;

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
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        AttackFalse();

        //SphereCollider coll = GetComponent<SphereCollider>();
        //coll.radius = AttackRadius;
    }

    private void Update()
    {
        LookTarget();
        MonsterHP -= 10;
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
        OnMoving?.Invoke(false);
        looktargetOn = false;
        anim.SetTrigger("Die");
        anim.SetBool("Attack", false);
        Destroy(gameObject,3.0f);
    }

    void AttackTrue()
    {
        OnMoving?.Invoke(false);
        anim.SetBool("Attack",true);
    }

    void AttackFalse()
    {
        OnMoving?.Invoke(true);
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

                if (Mathf.Abs(betweenAngle) < 10.0f)
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
