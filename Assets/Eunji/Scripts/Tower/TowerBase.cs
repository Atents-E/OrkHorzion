using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using Unity.VisualScripting;

#if UNITY_EDIOR
using UnityEditor;  // UNITY_EDIOR라는 전처리기가 설정되어있을 때만 실행버전에 넣어라
#endif

public class TowerBase : MonoBehaviour
{
    public int goal = 10;                   // 타워 가격
    public float turnSpeed = 10.0f;         // 타워의 회전속도
    public float sightRadius = 5.0f;        // 타워의 범위

    public GameObject projectile;           // 투사체 프리팹
    public float attackSpeed = 0.5f;        // 타워의 공격 속도
    public float fireAngle = 10.0f;         // 타워의 공격 각도
    float currentAngle = 0.0f;              // 방향의 처음 각도
    bool isFiring = false;                  // 발사 중인지 확인

    public float sightRange = 10.0f;


    IEnumerator fireCoroutine;              // 코루틴을 끄려면 변수로 가지고 있어야 함.
    Transform direction;                    // 타워 방향
    Transform directionTransform;           // 투사체 트랜스폼

    Transform target = null;        
    Vector3 initialForward;

    private void Awake()
    {
        direction = transform.GetChild(1);          // 움직일 방향 위치를 받아옴
        directionTransform = direction.GetChild(0); // 투사체 발사 위치 받아옴
        
        fireCoroutine = PeriodFire();
    }

    private void Start()
    {
        initialForward = transform.forward;
        SphereCollider col = GetComponent<SphereCollider>();
        col.radius = sightRadius;

        StartCoroutine(fireCoroutine);
    }

    /// <summary>
    ///  인터펙터 창에서 값이 성공적으로 변경되었을 때 호출되는 함수  //인스펙터 청에서도 값이 변경되는 것을(재생 전) 바로바로 확인 가능
    /// </summary>
    private void OnValidate()
    {
        SphereCollider col = GetComponent<SphereCollider>();
        if (col != null)
        {
            col.radius = sightRadius;
        }
    }

    private void Update()
    {
        LookTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))              // 범위 안에 Enemy가 들어오면
        {
            target = other.transform;               // target = Enemy
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))              // 범위 안에 Enemy가 나가면
        {
            target = null;                          // target 없음
        }
    }

    private void LookTarget()
    {
        if (target != null)
        {
            // 각도를 사용하는 경우(등속도로 회전)
            Vector3 shotToMonsterDir = target.position 
                - direction.position;  // 총구에서 플레이어의 위치로 가는 방향 벡터 계산
            shotToMonsterDir.y = 0;

            // 정방향일 때 0~180도. 역방향일 떄 0~-180도        //왼손 좌표계에서 엄지 손가락이 나를 향할 때, 다른 손가락은 시계 방향으로 감긴다.
            float betweenAngle = Vector3.SignedAngle(direction.forward, shotToMonsterDir, direction.up);

            Vector3 resultDir;
            if (Mathf.Abs(betweenAngle) > 1.0f)    // 사이각이 일정 각도 이하인지 체크
            {
                // 사이각이 충분히 벌어진 경우
                float rotateDirection = 1.0f;   //일단 +방향(정방향, 시계방향)으로 설정
                if (betweenAngle < 0)
                {
                    rotateDirection = -1.0f;    // betweenAngle이 -면 rotateDirection도 -1로
                }

                // 초당 turnSpeed만큼 회전하는데 rotateDirection로 시계방향으로 회전할지 반시계 방향으로 회전할지 결정
                currentAngle += (rotateDirection * turnSpeed * Time.deltaTime);

                resultDir = Quaternion.Euler(0, currentAngle, 0) * initialForward;
            }
            else
            {
                //사이각이 거의 0인 경우
                resultDir = shotToMonsterDir;
            }
            direction.rotation = Quaternion.LookRotation(resultDir);

            

            // 플레이어가 발사각 안에 있으면 공격 시작. 없으면 중지
            if (!isFiring && IsInFireAngle())
            {
                FireStart();
            }
            if (isFiring && !IsInFireAngle())
            {
                FireStop();
            }
        }
    }

    bool IsInFireAngle()        // 발사각 안에 있는지 확인하는 용도의 함수
    {

        Vector3 dir = target.position - direction.position;
        // Debug.Log(Vector3.Angle(direction.forward, dir));
        return Vector3.Angle(direction.forward, dir) < fireAngle;
    }

    private void Fire()
    {
        Instantiate(projectile, directionTransform.position, directionTransform.rotation);
    }

    IEnumerator PeriodFire()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private void FireStart()
    {
        isFiring = true;                // 발사 중이면
        StartCoroutine(fireCoroutine);  // 코루틴 시작
    }

    private void FireStop()
    {
        StopCoroutine(fireCoroutine);   // 코루틴 중단
        isFiring = false;               // 발사 중이지 않으면
    }

    private void OnDrawGizmos/*Selected*/()
    {
#if UNITY_EDITOR

        Handles.color = Color.green; 

        Handles.DrawWireDisc(transform.position, transform.up, sightRange, 3.0f);     //시야 반경만큼 원 그리기 //원이 하나만 보임
#endif  
    }

}
