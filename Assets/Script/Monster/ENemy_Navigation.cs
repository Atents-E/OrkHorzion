using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Navigation : MonoBehaviour
{
    private WayPoint waypoint;
    public Transform target;             // 네비매시 타겟
    protected NavMeshAgent agent;          // 네비매시
    int index = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();           // 네비매쉬 컴포넌트 찾기
        waypoint = FindObjectOfType<WayPoint>();        // 웨이포인트 스크립트 찾기
    }

    private void Start()
    {
        agent.SetDestination(waypoint.Current.position);        // 첫 번째 웨이포인틀 향해 걸어감
    }   

    public void Stopped(bool goStop)        // 걸어갈지 멈출지 정하는 함수
    {
        agent.isStopped = goStop;
        agent.velocity = Vector3.zero;
    }

    public void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)  // 경로 계산이 완료됬고 아직 도착지점으로 인정되는 거리까지 이동하지 않았다.
        {
            // WaypointTarget = wayPoint.MoveNext();   // 다음 웨이포인트 지점을 moveTarget로 설정
            index++;        // 인덱스를 1 증가 시킨다.
            index %= waypoint.transform.childCount;
            agent.SetDestination(waypoint.wayPoints[index].position);   // 다음 웨이포인트를 향해 걸어간다.
        } 
    }
}
