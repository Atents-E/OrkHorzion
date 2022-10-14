using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 3.0f;      // ���Ͱ� �����̴� �ӵ�
    public int monsterHp = 100;         // ���� �ִ� HP

    public float AttackRadius = 2.3f;   // ���Ͱ� ���ݰ����� ����
    public float AttackDemage = 10.0f;  // ���Ͱ� �����Ҷ��� ������
    public float AttackDelay = 5.0f;    // ���Ͱ� ������ �� ���� ���ݱ����� ��
    public float turnSpeed = 5.0f;      // ���Ͱ� ȸ���ϴ� �ӵ� 
    float currentAngle = 5.0f;          // �ʴ� �ٲ�� ����

    Transform playerTarget = null;       // �÷��̾ ����

    //Vector3 monsterToplayerDir;          // ���Ϳ� �÷��̾� ������ �Ÿ�

    bool looktargetOn = false;            // ���Ͱ� �÷��̾ �ٶ󺸴��� 

    NavMeshAgent agent;                  // �׺�Ž�

    public Transform target;             // �׺�Ž� Ÿ��
    
    Animator anim;

    //float random;
    //public float Random { get => UnityEngine.Random.Range(0, 1); }      // ����2�� ������ �Ҵ�

    public int MonsterHP        // HP ������Ƽ
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
        agent = GetComponent<NavMeshAgent>();   // �׺�Ž�
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //SphereCollider coll = GetComponent<SphereCollider>();
        //coll.radius = AttackRadius;
        
    }

    private void Update()
    {
        agent.SetDestination(target.position);

        if (looktargetOn)
        {
            Looktarget();
        }

        MonsterHP -= 10;
    }
    

    private void OnTriggerEnter(Collider other)     // Ʈ���� �ȿ� ������ ��
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("����");
            looktargetOn = true;
            playerTarget = other.transform;         // playerTarget�� null�� �ƴϰ� �Ǿ���.
            OffMoving();
        }
    }

    private void OnTriggerExit(Collider other)      // Ʈ���Ÿ� ���� ������ ��
    {
        if (other.CompareTag("Player"))  
        {
            Debug.Log("����");
            OnMoving();

            looktargetOn = false;
            playerTarget = null;                    // playerTarget�� null�� �Ǿ���.
            
        }
    }

    void OnMoving()
    {
        //agent.updatePosition = true;
        agent.isStopped = false;
        anim.SetBool("Idle", false);
        anim.SetBool("Attack", false);

    }

    void OffMoving()
    {
        //isMoving = false;
        anim.SetBool("Idle",true);
        anim.SetBool("Attack",true);
        agent.isStopped = true;
        //agent.updatePosition = false;
    }

    void Die()
    {
        anim.SetTrigger("Die");
        agent.isStopped = true;
        looktargetOn = false;
        Destroy(gameObject, 3.0f);  // 3�ʵڿ� ���� ������Ʈ ����    
    }

    void Looktarget()
    {
        if (looktargetOn)
        {
            if (playerTarget != null)
            {
                Vector3 dir = (playerTarget.position - transform.position).normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), currentAngle * Time.deltaTime);

            }
        }
    }

    //void LookTarget()       // �÷��̾� ���� �ٶ󺸱�
    //{
    //    if (looktargetOn)
    //    {
    //        if (playerTarget != null)
    //        {
    //            monsterToplayerDir = playerTarget.position - transform.position;
    //            monsterToplayerDir.y = 0;

    //            float betweenAngle = Vector3.SignedAngle(transform.forward, monsterToplayerDir, transform.up);

    //            Vector3 resultDir;

    //            if (Mathf.Abs(betweenAngle) < 10.0f)
    //            {
    //                float rotateDir = 1.0f;

    //                if (betweenAngle < 0)
    //                {
    //                    rotateDir = -1.0f;
    //                }

    //                currentAngle += (rotateDir * turnSpeed * Time.fixedDeltaTime);

    //                resultDir = Quaternion.Euler(0, currentAngle, 0) * transform.forward;
    //            }
    //            else
    //            {
    //                resultDir = monsterToplayerDir;
    //            }
    //            transform.rotation = Quaternion.LookRotation(resultDir);
    //        }

    //    }
    //}
}
