using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerBase : MonoBehaviour
{
    public int goal = 10;                   // 타워 가격
    public float turnSpeed = 5.0f;          // 타워의 회전속도
    public float sightRadius = 5.0f;        // 타워의 범위

    public GameObject shotPrefab;           //총알 프리팹
    public float attackSpeed = 2.0f;        // 타워의 공격 속도
    public float fireAngle = 10.0f;         // 타워의 공격 각도
    float currentAngle = 0.0f;              // 총구의 처음 각도
    bool isFiring = false;                  // 발사 중인지 확인

    IEnumerator fireCoroutine;          // 코루틴을 끄려면 변수로 가지고 있어야 함.
    Transform direction;                //  총알의 트랜스폼
    Transform shotTransform;            // 총알의 트랜스폼(총알의 위치와 방향 필요)

    Transform target = null;        
    Vector3 initialForward;

    private void Awake()
    {
        direction = transform.GetChild(1);     // 움직일 총구를 받아옴
        shotTransform = direction.GetChild(0); // 총알 발사 위치 받아옴
    }

    private void Start()
    {
        initialForward = transform.forward;
        SphereCollider col = GetComponent<SphereCollider>();
        col.radius = sightRadius;
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
        if (other.CompareTag("Monster"))            // 범위 안에 Monster가 들어오면
        {
            target = other.transform;               // target = Monster
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))            // 범위 안에 Monster가 나가면
        {
            target = null;                          // target 없음
        }
    }

    private void LookTarget()
    {
        if (target != null)
        {
            // 각도를 사용하는 경우(등속도로 회전)
            Vector3 shotToMonsterDir = target.position - shotTransform.position;  // 총구에서 플레이어의 위치로 가는 방향 벡터 계산
            shotToMonsterDir.y = 0;

            // 정방향일 때 0~180도. 역방향일 떄 0~-180도        //왼손 좌표계에서 엄지 손가락이 나를 향할 때, 다른 손가락은 시계 방향으로 감긴다.
            float betweenAngle = Vector3.SignedAngle(shotTransform.forward, shotToMonsterDir, shotTransform.up);

            Vector3 resultDir;
            if (Mathf.Abs(betweenAngle) > 0.05f)    // 사이각이 일정 각도 이하인지 체크
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
            shotTransform.rotation = Quaternion.LookRotation(resultDir);

            

            // 플레이어가 발사각 안에 있으면 곡역 시작. 없으면 중지
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
        Vector3 dir = target.position - shotTransform.position;
        dir.y = 0;
        return Vector3.Angle(shotTransform.forward, dir) < fireAngle;
    }

    private void Fire()
    {
        Instantiate(shotPrefab, shotTransform.position, shotTransform.rotation);
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

    //================================================================================
    public class Turret : MonoBehaviour
    {
        // 플레이어가 일정 반경 안에 들어오면 해당방향으로 총구를 돌린다.
        // sightRadius 아무런 영향을 주지 않는다.
        // turnSpeed가 아무런 영향을 주지 않는다. ( 총구가 즉시 회전한다. )

        public float turnSpeed = 2.0f;    // 회전 속도
        public float sightRadius = 5.0f;    // 반경

        public GameObject bulletPrefab;     // 총알 프리팹
        public float fireInterval = 0.5f;   // 총알 발사 간격
        public float fireAngle = 10.0f;     // 발사 각도

        Transform fireTransform;            // 총알의 트랜스폼(총알의 위치와 방향 필요)
        IEnumerator fireCoroutine;          // 코루틴을 끄려면 변수로 가지고 있어야 함.
        WaitForSeconds waitFireInterval;    // 코루틴의 가비지를 줄이는 방식2
        bool isFiring = false;              // 발사 중인지 확인


        Transform target = null;      // 플레이어의 위치 정보
        Transform barrelBody;        // 총구

        float currentAngle = 0.0f;
        //float targetAngle = 0.0f;
        Vector3 initialForward;

        private void Awake()
        {
            barrelBody = transform.GetChild(2);     // 움직일 총구를 받아옴
            fireTransform = barrelBody.GetChild(3); // 총알 발사 위치 받아옴

            fireCoroutine = PeriodFire();
        }

        private void Start()
        {
            initialForward = transform.forward;
            SphereCollider col = GetComponent<SphereCollider>();
            col.radius = sightRadius;

            waitFireInterval = new WaitForSeconds(fireInterval);
            //StartCoroutine(fireCoroutine);    //  코루틴의 가비지를 줄이는 방식1 //코루틴을 자주 껏다 켰다 할 때는 코루틴을 변수에 저장하고 사용해야한다.
            // StartCoroutine(PeriodFire());    // 이 코드는 PeriodFire()를 1회용으로 사용한다. 그래서 가비지가 생성된다.
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


        // 반경안에 들어온건지 확인 Trigger
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                target = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                target = null;
            }
        }

        private void LookTarget()
        {
            if (target != null)
            {

                // 각도를 사용하는 경우(등속도로 회전)
                Vector3 barrelToPlayerDir = target.position - barrelBody.position;  // 총구에서 플레이어의 위치로 가는 방향 벡터 계산
                barrelToPlayerDir.y = 0;

                // 정방향일 때 0~180도. 역방향일 떄 0~-180도        //왼손 좌표계에서 엄지 손가락이 나를 향할 때, 다른 손가락은 시계 방향으로 감긴다.
                float betweenAngle = Vector3.SignedAngle(barrelBody.forward, barrelToPlayerDir, barrelBody.up);

                Vector3 resultDir;
                if (Mathf.Abs(betweenAngle) > 0.05f)    // 사이각이 일정 각도 이하인지 체크
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
                    resultDir = barrelToPlayerDir;
                }
                barrelBody.rotation = Quaternion.LookRotation(resultDir);

                // 플레이어가 발사각 안에 있으면 곡역 시작. 없으면 중지
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
            // target;
            // transform.position;
            // fireAngle;

            //(내적을 구하기. 유니티에서 Vector3에 Angle함수 이용으로 가능하도록 만들어둠)

            Vector3 dir = target.position - barrelBody.position;
            dir.y = 0;
            return Vector3.Angle(barrelBody.forward, dir) < fireAngle;
        }

        private void Fire()
        {
            // 총알을 발사한다. ==> 총알 생성필요
            // 총알 프리팹. 총알이 발사 될 방향, 총알이 발사되는 주기
            //Instantiate 생성하는 함수 이름(생성 할 프리팹, 생성 할 위치, 생성되는 회전)
            Instantiate(bulletPrefab, fireTransform.position, fireTransform.rotation);

        }

        IEnumerator PeriodFire()
        {
            while (true)
            {
                Fire();
                yield return waitFireInterval;  // 아래보다 가비지 생성을 줄일 수 있다.
                                                //yield return new WaitForSeconds(fireInterval);    //매번 새롭게 생성(가비지가 많이 생긴다.)
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

        //private void OnDrawGizmos() // scene창에 발사되는 각도를 표시
        //{

        //}
    }

}
