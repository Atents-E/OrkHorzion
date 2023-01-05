using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Direction 기능
// 부모(타워)의 타겟을 받아오고,
// 1. 타겟을 향해 y축 기준으로 회전
// 2. 발사각 내부에 타겟이 들어왔는지 알려줌
public class Direction : MonoBehaviour
{

    public float turnSpeed = 10.0f;         // 회전속도
    public float fireAngle = 10.0f;         // 타워의 공격 각도
    protected float currentAngle = 0.0f;    // 방향의 처음 각도

    protected Vector3 initialForward;                 // 처음 앞

    GameObject target;              // 발사체와 만날 타겟(적)
    ShortTower parent;              // 부모


    protected virtual void Start()
    {
        parent = GetComponentInParent<ShortTower>();
        target = parent.target;
    }

    protected void Update()
    {
        LookTarget();
    }


    protected virtual void LookTarget()   // 타겟을 보도록 회전
    {
        if (target != null)     // 타겟이 있다면,
        {
            // 각도를 사용하는 경우(등속도로 회전)
            Vector3 shotToMonsterDir = target.transform.position - transform.position;  // 방향(타워)에서 적의 위치로 가는 방향 벡터 계산
            shotToMonsterDir.y = 0;

            // 정방향일 때 0~180도. 역방향일 떄 0~-180도        //왼손 좌표계에서 엄지 손가락이 나를 향할 때, 다른 손가락은 시계 방향으로 감긴다.
            float betweenAngle = Vector3.SignedAngle(transform.forward, shotToMonsterDir, transform.up);

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
            transform.rotation = Quaternion.LookRotation(resultDir);
        }
    }

    public virtual bool IsInFireAngle()        // 발사각 안에 있는지 확인하는 용도의 함수
    {
        if(target != null)
        {
            Vector3 dir = target.transform.position - transform.position;         // 타겟 위치 - 방향 위치
            dir.y = 0;
            return Vector3.Angle(transform.forward, dir) < fireAngle;   // 방향의 앞쪽과 dir사이의 내각이 발사각보다 작다
            // Debug.Log("타겟이 발사각 안에 있음");
        }
        else
        {
            return false;   // 발사각 안에 없다
        }
    }
}
