using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

#if UNITY_EDIOR
using UnityEditor;  // UNITY_EDIOR라는 전처리기가 설정되어있을 때만 실행버전에 넣어라
#endif

//[RequireComponent(typeof(SphereCollider))]
//[RequireComponent(typeof(CapsuleCollider))]
// TowerBase의 기능
// 1. 범위 안의 몬스터 확인하여 타겟으로 설정
// 2. 투사체 생성
// 3. 생성한 투사체를 자식으로 설정

// 4. 타워를 클릭하면 삭제 확인하는 창을 띄우고,
// 5. 삭제를 요청하면 타워 금액을 반환, 타워는 삭제된다.
public class TowerBase : MonoBehaviour
{
    public int gold = 10;                   // 타워 가격

    public float sightRange = 5.0f;        // 범위
    public float sightRadius = 2.5f;        // 범위 반지름

    public float proCreatSpeed = 3.0f;      // 투사체 생성 속도

    public float fireAngle = 10.0f;         // 타워의 공격 각도
    public float turnSpeed = 10.0f;         // 회전속도
    protected float currentAngle = 0.0f;    // 방향의 처음 각도

    protected bool isFiring = false;        // 발사 중인지 확인
    
    //protected IEnumerator fireCoroutine;    // 코루틴을 끄려면 변수로 가지고 있어야 함.

    public GameObject target;               // 타겟은 null
    public GameObject projectile;           // 투사체 프리팹

    protected Vector3 createPos;               // 투사체 생성 할 Vecotr3 위치
    Transform fireTransform;
    // GameObject dirPos;                           // 회전 하는 오브젝트의 위치


    public float fireInterval = 1.0f;
    public float coolTime = 0.0f;

    protected Vector3 initialForward;                 // 처음 앞


    protected virtual void Awake()
    {
        //fireCoroutine = PeriodFire();               // 코루틴을 변수로 사용하려고 할당
        createPos = transform.GetChild(0).transform.position;    // 투사체 생성 위치
        fireTransform = transform.GetChild(0);

        // dirPos = transform.GetChild(1).gameObject;
    }

    protected virtual void Start()    // 첫번째 업데이트가 일어나기 전에 호출
    {
        //initialForward = transform.forward;         // 처음 앞은 게임 오브젝트의 앞
        SphereCollider col = GetComponent<SphereCollider>();    // 구 컬라이더 할당
        col.radius = sightRadius;                   // 
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))              // 범위 안에 Enemy가 들어오면
        {
            target = other.gameObject;               // target = Enemy
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))              // 범위 안에 Enemy가 나가면
        {
            target = null;                          // target 없음
            isFiring = false;                       // 발사 확인(false : fire코루틴 중지) 
        }
    }



    protected void Attack()
    {
        coolTime += Time.deltaTime;

        if(target != null && coolTime > fireInterval)
        {
            Vector3 dir = target.transform.position - fireTransform.position;
            dir.y = 0;

            fireTransform.forward = dir.normalized;
            Fire();
            coolTime = 0;
        }
    }

    protected virtual void Fire()       // 투사체 생성
    {
        GameObject obj= Instantiate(projectile, fireTransform);
        obj.transform.SetParent(null);
    }


    /// <summary>
    /// 타워의 금액의 80%를 반환하고, 타워를 삭제한다.
    /// </summary>
    public void DeleteTower()
    {
        // Inventory inventory = GetComponent<Inventory>();
        // inventory.gold += gold * 0.8f;

        Destroy(this.gameObject);
    }


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
