using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
// using static System.IO.Enumeration.FileSystemEnumerable<TResult>;

#if UNITY_EDIOR
using UnityEditor;  // UNITY_EDIOR라는 전처리기가 설정되어있을 때만 실행버전에 넣어라
#endif

// TowerBase_Final 기능
// 타워베이스와 타워베이스_1를 합침(회전타워와 회전하지 않는 타워 모두 상속 받도록 해야함)
public class TowerBase : MonoBehaviour
{
    /// <summary>
    /// 타워
    /// </summary>
    public int price = 10;               // 타워 가격

    public float sightRange = 5.0f;         // 범위
    float sightHalfAngle = 50.0f;

    public float fireAngle = 10.0f;         // 타워의 공격 각도
    public float turnSpeed = 180.0f;        // 타워의 회전속도

    public float attackRate = 1.0f;         // 공격 속도
    public float fireInterval = 1.0f;       // 발사 간격
    public float coolTime = 0.0f;           // 쿨타임
    protected float currentAngle = 0.0f;    // 처음 각도

    public GameObject projectile;           // 투사체 프리팹
    protected Transform BulletPrefabPos;    // 투사체 생성 위치 Vector3
    protected Transform attackTarget = null;    

    public SpriteRenderer towerChoose;             // 선택한 타워를 알려줄 스프라이트
    
    Transform childPos;                     // 발사각 확인 할 위치

    protected GameObject enemy;
    //protected List<Transform> enemys;       // 몬스터 리스트

    protected virtual void Awake()
    {
        childPos = transform.GetChild(2);
        BulletPrefabPos = childPos.GetChild(0);
        towerChoose = transform.GetChild(1).GetComponent<SpriteRenderer>();

        towerChoose.color = Color.clear;

        //enemys = new List<Transform>();
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

    protected virtual void Update()
    {
        Attack();
    }

    /// <summary>
    /// 범위 안의 몬스터 감지
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy = other.gameObject;

            //StartCoroutine(AttackToTarget());
            //attackTarget = other.transform;            
            //isFire = true;
            //enemys.Add(other.transform);
        }
    }

    /// <summary>
    /// 몬스터가 범위 밖으로 나가는 것 감지
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy = other.gameObject;
            enemy = null;
            //StartCoroutine(SearchTarget());
            //isFire = false;
            // enemys.Remove(other.transform);
        }
    }

    /// <summary>
    /// 타겟을 보도록 타워 회전
    /// </summary>
    protected void LookTarget()   // 타겟을 보도록 회전
    {
        if (enemy != null)     // 타겟이 있다면,
        {
            Attack(true);
            attackTarget = enemy.transform;

            //float closestDistSqr = 10.0f;

            //for (int i = 0; i < enemys.Count; i++)
            //{
            //float distance = Vector3.Distance(enemys[i].transform.position, transform.position);
            //if (distance <= sightRange && distance <= closestDistSqr)
            //{
            //    closestDistSqr = distance;
            //    attackTarget = enemys[i].transform;
            //}

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

                        resultDir = Quaternion.Euler(0, currentAngle, 0) * transform.forward;

                    }
                    else
                    {
                        //사이각이 거의 0인 경우
                        resultDir = shotToMonsterDir;
                    }
                    childPos.transform.rotation = Quaternion.LookRotation(resultDir);
                //}
            }
        }
    }

    /// <summary>
    /// 몬스터가 있고, 쿨타임이 되었는지 확인
    /// 몬스터 방향으로 투사체 발사
    /// </summary>
    protected virtual void Attack()     // 확인하고, virtual 삭제 예정
    {
        coolTime += Time.deltaTime;                 // 쿨타임에 시간 더하고,
        attackTarget = enemy.transform;

        if (attackTarget != null && coolTime > fireInterval)   // 적도 있고, 쿨타임이 차면
        {
            Vector3 dir = attackTarget.transform.position - BulletPrefabPos.position;
            dir.y += 0.3f;

            BulletPrefabPos.forward = dir.normalized;
            Fire();
            coolTime = 0;                           // 쿨타임 초기화
        }
        attackTarget = null;
    }

    /// <summary>
    /// Attack함수 오버로딩
    /// </summary>
    /// <param name="isFire"></param>
    void Attack(bool isFire)
    {
        if (isFire)
        {
            Attack();
        }
    }

    /// <summary>
    /// 투사체 생성
    /// </summary>
    void Fire()
    {
        GameObject obj = Instantiate(projectile, BulletPrefabPos);
        obj.transform.SetParent(transform);
    }

    /// <summary>
    /// 타워의 금액의 80%를 반환하고, 타워를 삭제한다.
    /// </summary>
    public void DeleteTower()
    {
        PlayerGold playerGold = GameManager.Inst.PlayerGold;
        float returnGold = price * 0.8f;

        playerGold.NowGold += Mathf.FloorToInt(returnGold);

        Destroy(transform.gameObject);
    }

    protected virtual void OnDrawGizmosSelected() // 기즈모
    {
#if UNITY_EDITOR

        Handles.color = Color.green;                // 초록색으로 표시
        Handles.DrawWireDisc(transform.position, transform.up, sightRange, 3.0f);     //시야 반경만큼 원 그리기 //원이 하나만 보임

        if (attackTarget != null)                   // 타겟이 있다면
        {
            Handles.color = Color.red;              // 이후에 작성된 코드들은 빨간색으로 표시
        }

        Vector3 forward = transform.GetChild(1).forward;    // 움직일 방향 위치를 받아옴
        forward.y = 0;
        forward = forward.normalized * sightRange;

        Handles.DrawDottedLine(transform.position, transform.position + forward, 2.0f); // 중심선 그리기

        Quaternion q1 = Quaternion.AngleAxis(-sightHalfAngle, transform.up);            // up벡터를 축으로 반시계방향으로 sightHalfAngle만큼 회전
        Quaternion q2 = Quaternion.AngleAxis(sightHalfAngle, transform.up);             // up벡터를 축으로 시계방향으로 sightHalfAngle만큼 회전

        Handles.DrawLine(transform.position, transform.position + q1 * forward);        // 중심선을 반시계방향으로 회전시켜서 그리기
        Handles.DrawLine(transform.position, transform.position + q2 * forward);        // 중심선을 시계방향으로 회전시켜서 그리기
        Handles.DrawWireArc(transform.position, transform.up, q1 * forward, sightHalfAngle * 2, sightRange, 5.0f);  // 호 그리기
#endif  
    }
}
