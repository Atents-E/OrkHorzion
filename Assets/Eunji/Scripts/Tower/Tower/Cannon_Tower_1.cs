using System.Collections;
using UnityEditor;
using UnityEngine;

#if UNITY_EDIOR
using UnityEditor;  // UNITY_EDIOR라는 전처리기가 설정되어있을 때만 실행버전에 넣어라
#endif

// Cannon_Tower 기능
// 1. 범위 안의 몬스터 확인하여 타겟으로 설정
// 2. 투사체 생성
// 3. 생성한 투사체를 자식으로 설정

// 1. 투사체 생성 조건 설정(0번째 자식이 타겟을 바라본게 맞으면 투사체 생성(fireAngle이면))
// 2. 투사체 생성 위치 변경
public class Cannon_Tower_1 : TowerBase_1
{
    Transform childPos;                    // 발사각 확인 할 위치
    Vector3 proCreatPos;                   // 투사체 생성 위치 Vector3 
    Direction dir;                         // 발사각도를 확인할 수 있는 참조
    Transform dirPos;
    Transform target;

    protected override void Awake()
    {
        base.Awake();

        childPos = transform.GetChild(1);
        createPos = childPos.GetChild(0).transform.position;   // 투사체 생성 위치를 재 할당.
        dir = childPos.GetComponent<Direction>();

        dirPos = transform.GetChild(1);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            target = other.transform;            
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            target = null;
        }
    }

    protected void Update()
    {        
        LookTarget();
    }


    protected void LookTarget()   // 타겟을 보도록 회전
    {
        if (target != null)     // 타겟이 있다면,
        {
            // 각도를 사용하는 경우(등속도로 회전)
            Vector3 shotToMonsterDir = target.transform.position - childPos.position;  // 방향(타워)에서 적의 위치로 가는 방향 벡터 계산
            shotToMonsterDir.y = 0;

            // 정방향일 때 0~180도. 역방향일 떄 0~-180도        //왼손 좌표계에서 엄지 손가락이 나를 향할 때, 다른 손가락은 시계 방향으로 감긴다.
            float betweenAngle = Vector3.SignedAngle(childPos.forward, shotToMonsterDir, transform.up);

            Vector3 resultDir = new Vector3();

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

                resultDir = Quaternion.Euler(0, currentAngle, 0) * transform.forward;
            }
            else
            {
                //사이각이 거의 0인 경우
                resultDir = shotToMonsterDir;
            }
            dirPos.transform.rotation = Quaternion.LookRotation(resultDir);
        }
    }

    //public virtual bool IsInFireAngle()        // 발사각 안에 있는지 확인하는 용도의 함수
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

    //    protected override void OnDrawGizmos/*Selected*/() // 기즈모
    //    {
    //#if UNITY_EDITOR


    //        Handles.color = Color.green;    // 초록색으로 표시
    //        Handles.DrawWireDisc(transform.position, transform.up, sightRange, 3.0f);     //시야 반경만큼 원 그리기 //원이 하나만 보임

    //        if (target != null)             // 타겟이 있다면
    //        {
    //            Handles.color = Color.red;  // 이후에 작성된 코드들은 빨간색으로 표시
    //        }

    //        Vector3 forward = transform.GetChild(1).forward;    // 움직일 방향 위치를 받아옴
    //        forward.y = 0;
    //        forward = forward.normalized * sightRange;


    //        //Handles.DrawDottedLine(transform.position, transform.position + forward, 2.0f); // 중심선 그리기

    //        //Quaternion q1 = Quaternion.AngleAxis(-sightHalfAngle, transform.up);// up벡터를 축으로 반시계방향으로 sightHalfAngle만큼 회전
    //        //Quaternion q2 = Quaternion.AngleAxis(sightHalfAngle, transform.up); // up벡터를 축으로 시계방향으로 sightHalfAngle만큼 회전

    //        //Handles.DrawLine(transform.position, transform.position + q1 * forward);    // 중심선을 반시계방향으로 회전시켜서 그리기
    //        //Handles.DrawLine(transform.position, transform.position + q2 * forward);    // 중심선을 시계방향으로 회전시켜서 그리기
    //        //Handles.DrawWireArc(transform.position, transform.up, q1 * forward, sightHalfAngle * 2, sightRange, 5.0f);  // 호 그리기
    //#endif  
    //    }
}
