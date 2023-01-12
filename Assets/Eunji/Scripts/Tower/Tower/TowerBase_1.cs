using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;


#if UNITY_EDIOR
using UnityEditor;  // UNITY_EDIOR라는 전처리기가 설정되어있을 때만 실행버전에 넣어라
#endif

//public enum AttackState { SearchTarget = 0, AttackToTarget }

//[RequireComponent(typeof(SphereCollider))]
//[RequireComponent(typeof(CapsuleCollider))]
// TowerBase의 기능
// 1. 범위 안의 몬스터 확인하여 타겟으로 설정
// 2. 투사체 생성
// 3. 생성한 투사체를 자식으로 설정

// 4. 타워를 클릭하면 삭제 확인하는 창을 띄우고,
// 5. 삭제를 요청하면 타워 금액을 반환, 타워는 삭제된다.
public class TowerBase_1 : MonoBehaviour
{    
    public int price = 10;                  // 타워 가격

    public float sightRange = 5.0f;         // 범위
    public float sightRadius = 2.5f;        // 범위 반지름

    public float fireAngle = 10.0f;         // 타워의 공격 각도
    public float turnSpeed = 180.0f;        // 회전속도

    public float attackRate = 1.0f;         // 공격 속도
    public float fireInterval = 1.0f;
    public float coolTime = 0.0f;

    protected float currentAngle = 0.0f;    // 방향의 처음 각도

    protected Vector3 createPos;            // 투사체 생성 할 Vecotr3 위치

    public GameObject projectile;           // 투사체 프리팹

    Transform childPos;                    // 발사각 확인 할 위치
    Transform BulletPrefabPos;             // 투사체 생성 위치 Vector3
    Transform dirPos;
    Transform attackTarget = null;
    //AttackState attackState = AttackState.SearchTarget;

    //EnemySpawner enemySpawner;
    List<Transform> enemys;

    protected void Awake()
    {
        childPos = transform.GetChild(1);
        BulletPrefabPos = childPos.GetChild(0);

        dirPos = transform.GetChild(1);
        enemys = new List<Transform>();
    }

    /// <summary>
    ///  인터펙터 창에서 값이 성공적으로 변경되었을 때 호출되는 함수  //인스펙터 청에서도 값이 변경되는 것을(재생 전) 바로바로 확인 가능
    /// </summary>
    void OnValidate()
    {
        SphereCollider col = GetComponent<SphereCollider>();
        if (col != null)
        {
            col.radius = sightRange;
        }
    }

    private void Update()
    {
        LookTarget();        
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //StartCoroutine(AttackToTarget());
            //attackTarget = other.transform;            
            //isFire = true;
            enemys.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //StartCoroutine(SearchTarget());
            //isFire = false;
            enemys.Remove(other.transform);
        }
    }

    protected void LookTarget()   // 타겟을 보도록 회전
    {
        if (enemys != null)     // 타겟이 있다면,
        {
            Attack(true);
            float closestDistSqr = 10.0f;

            for (int i = 0; i < enemys.Count; i++)
            {
                float distance = Vector3.Distance(enemys[i].transform.position, transform.position);
                if (distance <= sightRange && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    attackTarget = enemys[i].transform;
                }

                if (attackTarget != null)
                {
                    // 각도를 사용하는 경우(등속도로 회전)
                    Vector3 shotToMonsterDir = attackTarget.transform.position - childPos.position;  // 방향(타워)에서 적의 위치로 가는 방향 벡터 계산
                    shotToMonsterDir.y = 0;

                    // 정방향일 때 0~180도. 역방향일 떄 0~-180도        //왼손 좌표계에서 엄지 손가락이 나를 향할 때, 다른 손가락은 시계 방향으로 감긴다.
                    float betweenAngle = Vector3.SignedAngle(childPos.forward, shotToMonsterDir, transform.up);

                    Vector3 resultDir = new Vector3();

                    if (Mathf.Abs(betweenAngle) > 2.0f)    // 사이각이 일정 각도 이하인지 체크
                    {
                        // 사이각이 충분히 벌어진 경우
                        float rotateDirection = 1.0f;   //일단 +방향(정방향, 시계방향)으로 설정                
                        if (betweenAngle < 0)
                        {
                            rotateDirection = -1.0f;    // betweenAngle이 -면 rotateDirection도 -1로                   
                        }

                        // 초당 turnSpeed만큼 회전하는데 rotateDirection로 시계방향으로 회전할지 반시계 방향으로 회전할지 결정
                        currentAngle += (rotateDirection * turnSpeed * Time.deltaTime);

                        resultDir = Quaternion.Euler(30.0f, currentAngle, 0) * transform.forward;
                    }
                    else
                    {
                        //사이각이 거의 0인 경우
                        resultDir = shotToMonsterDir;
                    }
                    dirPos.transform.rotation = Quaternion.LookRotation(resultDir);
                }
            }
        }
    }

    //void Setup(EnemySpawner enemySpawner)
    //{
    //    this.enemySpawner = enemySpawner;

    //    ChangeState(AttackState.SearchTarget);
    //}

    //public void ChangeState(AttackState newState)
    //{
    //    // 이전에 실행중이던 상태 종료
    //    StopCoroutine(attackState.ToString());
    //    // 상태 변경
    //    attackState = newState;
    //    // 새로운 상태 실행
    //    StartCoroutine(attackState.ToString());
    //}

    //void LookTarget2()
    //{
    //    float dx = attackTarget.position.x - transform.position.x;
    //    float dz = attackTarget.position.z - transform.position.z;

    //    float degree = Mathf.Atan2(dz, dx) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(0, degree, 0);
    //}

    //IEnumerator SearchTarget()
    //{
    //    while(true)
    //    {
    //        float closestDistSqr = Mathf.Infinity;

    //        for (int i = 0; i < enemySpawner.EnemyList.Count; i++)
    //        {
    //            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
    //            if (distance <= sightRange && distance <= closestDistSqr)
    //            {
    //                closestDistSqr = distance;
    //                attackTarget = enemySpawner.EnemyList[i].transform;
    //            }
    //        }

    //        if (attackTarget != null)
    //        {
    //            ChangeState(AttackState.AttackToTarget);
    //        }
    //        yield return null;
    //    }
    //}

    //IEnumerator AttackToTarget()
    //{
    //    while (true)
    //    {
    //        if (attackTarget == null)
    //        {
    //            ChangeState(AttackState.SearchTarget);
    //            break;
    //        }

    //        float distance = Vector3.Distance(attackTarget.position, transform.position);
    //        if (distance > sightRange)
    //        {
    //            attackTarget = null;
    //            ChangeState(AttackState.SearchTarget);
    //            break;
    //        }

    //        yield return new WaitForSeconds(attackRate);

    //        Fire();
    //    }
    //}

    //bool isFire = false;
    protected void Attack(bool isFire)
    {
        if (isFire)
        {
            coolTime += Time.deltaTime;

            if (attackTarget != null && coolTime > fireInterval)
            {
                Vector3 dir = attackTarget.transform.position - BulletPrefabPos.position;
                dir.y = 0;

                BulletPrefabPos.forward = dir.normalized;
                Fire();
                coolTime = 0;
            }
            attackTarget = null;
        }
    }

    void Fire()
    {
        GameObject obj = Instantiate(projectile, BulletPrefabPos);
        obj.transform.SetParent(null);
    }


    /// <summary>
    /// 타워의 금액의 80%를 반환하고, 타워를 삭제한다.
    /// </summary>
    protected void DeleteTower()
    {
        // Inventory playerGold = GetComponent<Inventory>();
        // playerGold += price * 0.8f;

        Destroy(this.gameObject);
    }


    /// <summary>
    /// 타워의 금액의 80%를 반환하고, 타워를 삭제한다.
    /// </summary>


//    protected virtual void OnDrawGizmos/*Selected*/() // 씬창에서 타워의 공격 범위를 표시

//    {
//#if UNITY_EDITOR

//        Handles.color = Color.blue;    // 초록색으로 표시

//        if (target != null)             // 타겟이 있다면
//        {
//            Handles.color = Color.red;  // 이후에 작성된 코드들은 빨간색으로 표시
//        }

//        Handles.DrawWireDisc(transform.position, transform.up, sightRadius, 3.0f);     //시야 반경만큼 원 그리기 //원이 하나만 보임
        
//        //Vector3 forward = transform.forward;    // 움직일 방향 위치를 받아옴
//        //forward.y = 0;
//        //forward = forward.normalized * sightRange;


//        //Handles.DrawDottedLine(transform.position, transform.position + forward, 2.0f); // 중심선 그리기

//        //Quaternion q1 = Quaternion.AngleAxis(-sightHalfAngle, transform.up);// up벡터를 축으로 반시계방향으로 sightHalfAngle만큼 회전
//        //Quaternion q2 = Quaternion.AngleAxis(sightHalfAngle, transform.up); // up벡터를 축으로 시계방향으로 sightHalfAngle만큼 회전

//        //Handles.DrawLine(transform.position, transform.position + q1 * forward);    // 중심선을 반시계방향으로 회전시켜서 그리기
//        //Handles.DrawLine(transform.position, transform.position + q2 * forward);    // 중심선을 시계방향으로 회전시켜서 그리기
//        //Handles.DrawWireArc(transform.position, transform.up, q1 * forward, sightHalfAngle * 2, sightRange, 5.0f);  // 호 그리기
//#endif  
//    }

}
