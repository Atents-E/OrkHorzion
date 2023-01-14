using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

public class EnemyBase : MonoBehaviour, IBattle, IHealth
{
    //float moveSpeed = 3.0f;                     // 몬스터가 움직이는 속도
    float AttackRadius = 2.3f;                    // 몬스터가 공격가능한 범위(반지름)
    public float currentAngle = 30.0f;            // 초당 바뀌는 각도
    protected bool looktargetOn = false;          // 몬스터가 플레이어를 바라보는지 
    //float sightHalfAngle = 50.0f;               // 반지름
    
    public float attackPower = 10.0f;             // 공격력
    public float defencePower = 0.0f;             // 방어력
    public float monsterHp = 100.0f;              // 몬스터 HP
    public float monsterMaxHp = 1000.0f;          // 몬스터 최대 HP

    public int gold;
    public int dieCount = 0;

    protected Transform playerTarget = null;      // 플레이어가 없다

    public Transform damageTextPos;
    public GameObject damageTextPrefab;

    private EnemySpawner enemySpawner;            // 적의 삭제를 본인이 하지 않고 EnemySpawner에 알려서 삭제
    //bool die = false;

    public float AttackPower => attackPower;
    public float DefencePower => defencePower;
    public float MaxHP
    {
        get => monsterMaxHp;
        set
        {
            monsterMaxHp = value;
        }
    }           

    /// <summary>
    /// HP 프로퍼티 몬스터의HP가 0보다 작아지면 죽는다.
    /// </summary>
    public float HP
    {
        get => monsterHp;
        set
        {
            if (monsterHp != value)
            {
                monsterHp = value;

                if (monsterHp < 0)
                {
                    monsterHp = 0;                    
                    
                    Die();
                }

                onHealthChange?.Invoke(monsterHp/monsterMaxHp);

                monsterHp = Mathf.Clamp(monsterHp, 0.0f, monsterMaxHp);
            }
        }
    }


    // 델리게이트
    public Action<float> onHealthChange { get; set; }
    public Action onDie { get; set; }

    //void Die()
    //{
    //    looktargetOn = false;
    //    Destroy(gameObject, 3.0f);  // 3초뒤에 몬스터 오브젝트 삭제    
    //}

    public void SetUp(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;
    }

    protected bool SearchPlayer()
    {
        bool result = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, AttackRadius, LayerMask.GetMask("Player"));

        if (colliders.Length > 0)
        {
            Vector3 playerPos = colliders[0].transform.position;    // 플레이어의 위치
            Vector3 toPlayerDir = playerPos - transform.position;   // 플레이어로 가는 방향

            // 시야각 안에 플레이어가 있는지 확인
            if (IsInsightAngle(toPlayerDir))
            {
                if (!IsSightBlocked(toPlayerDir))
                {
                    result = true;
                }
            }
        }
        return result;
    }

    protected bool IsInsightAngle(Vector3 toPlayerDir)
    {
        float angle = Vector3.Angle(transform.position, toPlayerDir);   // forward 벡터와 플레이어로 가는 방향 벡터의 사이각 구하기
        return (AttackRadius > angle);
    }

    protected bool IsSightBlocked(Vector3 toPlayerDir)
    {
        bool result = true;

        Ray ray = new(transform.position + transform.up * 0.5f, toPlayerDir);
        if (Physics.Raycast(ray, out RaycastHit hit, AttackRadius))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return result;
            }
        }

        return result;
    }

    protected void Looktarget()
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

    /// <summary>
    /// 공격용 함수
    /// </summary>
    /// <param name="target">공격할 대상</param>
    public void Attack(IBattle target)
    {
        target?.TakeDamage(AttackPower);
    }

    /// <summary>
    /// 방어용 함수
    /// </summary>
    /// <param name="damage">현재 입은 데미지</param>
    public void TakeDamage(float damage)
    {
        //GameObject obj = Instantiate(damageTextPrefab);
        //obj.transform.position = damageTextPos.position;
        GameObject obj = Instantiate(damageTextPrefab, damageTextPos);


        obj.GetComponent<DamageText>().Damage = (int)(damage - defencePower);
        Debug.Log($"입은 데미지 : {damage}");
        // 기본 공식 : 실제 입는 데미지 = 적 공격 데미지 - 방어력
        
        HP -= (damage - defencePower);
    }

    /// <summary>
    /// 죽었을 때 실행될 함수
    /// </summary>
    public void Die()
    {
        dieCount++;
        looktargetOn = false;
        playerTarget = null;
        onDie?.Invoke();
        GameManager.Inst.EnemySpawner.SetDel();
        GameManager.Inst.EnemySpawner.DestroyEnemy(this);
        //enemySpawner.DestroyEnemy(this);
        GameManager.Inst.PlayerGold.NowGold += gold;
    }



    //    protected void OnDrawGizmosSelected()
    //    {
    //#if UNITY_EDITOR
    //        Handles.color = Color.red;
    //        Handles.DrawWireDisc(transform.position, transform.up, AttackRadius);

    //        if (SearchPlayer())
    //        {
    //            Handles.color = Color.yellow;
    //        }
    //#endif
    //    }


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
