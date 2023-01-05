using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 3.0f;      // 몬스터가 움직이는 속도
    public int monsterHp = 100;         // 몬스터 최대 HP

    public float AttackRadius = 2.3f;   // 몬스터가 공격가능한 범위
    public float AttackDemage = 10.0f;  // 몬스터가 공격할때의 데미지
    public float AttackDelay = 5.0f;    // 몬스터가 공격한 뒤 다음 공격까지의 텀
    public float turnSpeed = 5.0f;      // 몬스터가 회전하는 속도 
    float currentAngle = 5.0f;          // 초당 바뀌는 각도

    Transform playerTarget = null;       // 플레이어가 없다

    //Vector3 monsterToplayerDir;          // 몬스터와 플레이어 사이의 거리

    bool looktargetOn = false;            // 몬스터가 플레이어를 바라보는지 

    NavMeshAgent agent;                  // 네비매시

    public Transform target;             // 네비매시 타겟
    
    Animator anim;

    //float random;
    //public float Random { get => UnityEngine.Random.Range(0, 1); }      // 어택2용 랜덤값 할당

    public int MonsterHP        // HP 프로퍼티
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
        agent = GetComponent<NavMeshAgent>();   // 네비매시
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
    

    private void OnTriggerEnter(Collider other)     // 트리거 안에 들어왔을 때
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("들어옴");
            looktargetOn = true;
            playerTarget = other.transform;         // playerTarget이 null이 아니게 되었다.
            OffMoving();
        }
    }

    private void OnTriggerExit(Collider other)      // 트리거를 빠져 나갔을 때
    {
        if (other.CompareTag("Player"))  
        {
            Debug.Log("나감");
            OnMoving();

            looktargetOn = false;
            playerTarget = null;                    // playerTarget이 null이 되었다.
            
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
        Destroy(gameObject, 3.0f);  // 3초뒤에 몬스터 오브젝트 삭제    
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

    //void LookTarget()       // 플레이어 방향 바라보기
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
