using System;
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
public class TowerBase_1 : MonoBehaviour
{    
    public int gold = 10;                   // 타워 가격

    public float sightRange = 5.0f;         // 범위
    public float sightRadius = 2.5f;        // 범위 반지름

    public float fireAngle = 10.0f;         // 타워의 공격 각도
    public float turnSpeed = 180.0f;        // 회전속도
    protected float currentAngle = 0.0f;    // 방향의 처음 각도
    
    //protected IEnumerator fireCoroutine;  // 코루틴을 끄려면 변수로 가지고 있어야 함.

    GameObject target;                      // 타겟은 null
    public GameObject projectile;           // 투사체 프리팹

    protected Vector3 createPos;            // 투사체 생성 할 Vecotr3 위치


    TowerInputActions inputActions;

    private void Awake()
    {
        inputActions = new TowerInputActions();
    }


    private void OnEnable()
    {
        inputActions.Tower.Enable();
        inputActions.Tower.Remove.performed += OnRemove;
    }

    private void OnDisable()
    {
        inputActions.Tower.Remove.performed -= OnRemove;
        inputActions.Tower.Disable();
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



    public  void OnRemove(InputAction.CallbackContext context)
    {

        Vector3 selectedTower = context.ReadValue<Vector3>();

        // 1. 클릭 된 오브젝트가 타워인지 확인
        Ray ray = Camera.main.ScreenPointToRay(selectedTower);   // 스크린 좌표로 레이 생성
        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, LayerMask.GetMask("Tower"))) // 레이와 타워의 충돌 여부 확인
        {
            // 충돌한 지점에 오브젝트가 있는지 확인
            Debug.Log($"{hit.collider}");

            // 2. 삭제 패널을 활성화 하라고 신호를 보내고
            CanvasTower canvas = FindObjectOfType<CanvasTower>();
            canvas.LookTowerDelete();

            //3. 
            if( canvas.OK != false)
            {
                DeleteTower();
            }
        }
    }


    /// <summary>
    /// 타워의 금액의 80%를 반환하고, 타워를 삭제한다.
    /// </summary>
    void DeleteTower()
    {
        // Inventory playerGold = GetComponent<Inventory>();
        // playerGold += gold * 0.8f;

        Destroy(this.gameObject);
    }

    //protected virtual void Update()
    //{
    //    //LookTarget();
    //    Attack();
    //}

    //protected virtual void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))              // 범위 안에 Enemy가 들어오면
    //    {
    //        target = other.gameObject;               // target = Enemy
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))              // 범위 안에 Enemy가 나가면
    //    {
    //        target = null;                          // target 없음
    //        isFiring = false;                       // 발사 확인(false : fire코루틴 중지) 
    //    }
    //}


    //protected virtual void LookTarget()   // 타겟을 보도록 회전
    //{
    //    if (target != null)     // 타겟이 있다면,
    //    {
    //        // 각도를 사용하는 경우(등속도로 회전)
    //        Vector3 shotToMonsterDir = target.transform.position - transform.position;  // 방향(타워)에서 적의 위치로 가는 방향 벡터 계산
    //        shotToMonsterDir.y = 0;

    //        // 정방향일 때 0~180도. 역방향일 떄 0~-180도        //왼손 좌표계에서 엄지 손가락이 나를 향할 때, 다른 손가락은 시계 방향으로 감긴다.
    //        float betweenAngle = Vector3.SignedAngle(dirPos.transform.forward, shotToMonsterDir, transform.up);

    //        Vector3 resultDir;
    //        if (Mathf.Abs(betweenAngle) > 1.0f)    // 사이각이 일정 각도 이하인지 체크
    //        {
    //            // 사이각이 충분히 벌어진 경우
    //            float rotateDirection = 1.0f;   //일단 +방향(정방향, 시계방향)으로 설정
    //            if (betweenAngle < 0)
    //            {
    //                rotateDirection = -1.0f;    // betweenAngle이 -면 rotateDirection도 -1로
    //            }

    //            // 초당 turnSpeed만큼 회전하는데 rotateDirection로 시계방향으로 회전할지 반시계 방향으로 회전할지 결정
    //            currentAngle += (rotateDirection * turnSpeed * Time.deltaTime);

    //            resultDir = Quaternion.Euler(0, currentAngle, 0) * initialForward;
    //        }
    //        else
    //        {
    //            //사이각이 거의 0인 경우
    //            resultDir = shotToMonsterDir;
    //        }
    //        dirPos.transform.rotation = Quaternion.LookRotation(resultDir);
    //    }
    //}

    //protected bool IsInFireAngle()        // 발사각 안에 있는지 확인하는 용도의 함수
    //{
    //    if (target != null)
    //    {
    //        Vector3 dir = target.transform.position - dirPos.transform.position;
    //        dir.y = 0;

    //        return Vector3.Angle(dirPos.transform.forward, dir) < fireAngle;   // 방향의 앞쪽과 dir사이의 내각이 발사각보다 작다
    //        // Debug.Log("타겟이 발사각 안에 있음");
    //    }
    //    else
    //    {
    //        return false;   // 발사각 안에 없다
    //    }
    //}

    //protected void Attack()
    //{
    //    coolTime += Time.deltaTime;

    //    if(target != null && coolTime > fireInterval)
    //    {
    //        Vector3 dir = target.transform.position - fireTransform.position;
    //        dir.y = 0;

    //        fireTransform.forward = dir.normalized;
    //        Fire();
    //        coolTime = 0;
    //    }
    //}

    //protected virtual void Fire()       // 투사체 생성
    //{
    //    GameObject obj= Instantiate(projectile, fireTransform);
    //    obj.transform.SetParent(null);
    //}

    //IEnumerator PeriodFire()            // 발사 시간
    //{
    //    WaitForSeconds wait = new WaitForSeconds(proCreatSpeed);

    //    while (isFiring)            // true일 동안에
    //    {
    //        Fire();             
    //        yield return wait;      // 발사체 생성 속도에 맞추어서 생성
    //    }
    //}

    /// <summary>
    /// 타워의 금액의 80%를 반환하고, 타워를 삭제한다.
    /// </summary>
    public void Delete_Tower()
    {
        // Inventory inventory = GetComponent<Inventory>();
        // inventory.gold += goal * 0.8f;

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
