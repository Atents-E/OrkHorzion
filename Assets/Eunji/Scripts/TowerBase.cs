using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    //변수===========================================================================
    /// <summary>
    /// 타워 가격
    /// </summary>
    public int goal = 10;

    /// <summary>
    /// 타워의 공격 속도
    /// </summary>
    public float attackSpeed = 2.0f;

    /// <summary>
    /// 타워의 공격 각도
    /// </summary>
    public float fireAngle = 10.0f;     
    
    /// <summary>
    /// 총구의 처음 각도
    /// </summary>
    float currentAngle = 0.0f;
    
    /// <summary>
    /// 타워의 범위
    /// </summary>
    public float sightRadius = 5.0f;

    /// <summary>
    /// 타워의 회전속도
    /// </summary>
    public float turnSpeed = 5.0f;

    /// <summary>
    ///  총알의 트랜스폼
    /// </summary>
    Transform ShotTransform;

    /// <summary>
    /// 타겟x
    /// </summary>
    Transform targetTransform = null;        
    Vector3 initialForward;

    public GameObject bulletPrefab;     // 총알 프리팹 //
    

    //컴포넌트
    // 리지드바디
    // 컬라이더
    // 애니메이터
    Rigidbody body;

    //기능
    // 설치가 가능한 곳인지 확인
    // 설치
    // 범위 체크
    // 범위 안의 몬스터 공격


    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }




}
