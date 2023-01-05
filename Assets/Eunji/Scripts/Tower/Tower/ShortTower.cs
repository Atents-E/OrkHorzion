using System.Collections;
using UnityEditor;
using UnityEngine;

#if UNITY_EDIOR
using UnityEditor;  // UNITY_EDIOR라는 전처리기가 설정되어있을 때만 실행버전에 넣어라
#endif

// ShortTower의 기능
// 1. 범위 안의 몬스터 확인하여 타겟으로 설정
// 2. 투사체 생성
// 3. 생성한 투사체를 자식으로 설정

// 1. 투사체 생성 조건 설정(0번째 자식이 타겟을 바라본게 맞으면 투사체 생성(fireAngle이면))
// 2. 투사체 생성 위치 변경
public class ShortTower : TowerBase
{
    Transform childPos;                    // 발사각 확인 할 위치
    Vector3 proCreatPos;                   // 투사체 생성 위치 Vector3 
    Direction dir;                         // 발사각도를 확인할 수 있는 참조

    protected override void Awake()
    {
        base.Awake();

        childPos = transform.GetChild(1);
        porPos = childPos.GetChild(0).transform.position;   // 투사체 생성 위치를 재 할당.
        dir = childPos.GetComponent<Direction>();   
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        //if (other.CompareTag("Enemy"))              // 범위 안에 Enemy가 들어오면
        //{
        //    target = other.gameObject;              // target을 Enemy로 설정

        //    if (target != null && dir.IsInFireAngle())    // 발사각도 내에 타워가 있으면
        //    {
        //        isFiring = true;
        //    }
        //}

        if (target != null && dir.IsInFireAngle())    // 발사각도 내에 타워가 있으면
        {
            isFiring = true;
        }
    }


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
