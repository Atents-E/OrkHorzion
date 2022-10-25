using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Navigation : MonoBehaviour
{
    Transform[] waypoints;
    int index = 0;

    //public Transform target;             // 네비매시 타겟
    protected NavMeshAgent agent;          // 네비매시

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //wayPoint = FindObjectOfType<WayPoint>();

        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        //SetTarget(waypoints[targetIndex]);  // 첫 웨이포인트 지정
        agent.SetDestination(waypoints[index].position);
    }

    private void Update()
    {
        transform.LookAt(waypoints[index]);   // 목적지를 바라봄

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)  // 경로 계산이 완료됬고 아직 도착지점으로 인정되는 거리까지 이동하지 않았다.
        {
            // WaypointTarget = wayPoint.MoveNext();   // 다음 웨이포인트 지점을 moveTarget로 설정
            GoNextWayPoint();
        }
    }

    public void Stopped(bool goStop)
    {
        agent.isStopped = goStop;
    }

    public void Update_Run()
    {
        //agent.SetDestination(wayPoint.Current.position);
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)  // 경로 계산이 완료됬고 아직 도착지점으로 인정되는 거리까지 이동하지 않았다.
        {
           // WaypointTarget = wayPoint.MoveNext();   // 다음 웨이포인트 지점을 moveTarget로 설정
            
        }
    }

    public void Destination()
    {
        agent.SetDestination(waypoints[index].position);
    }

    void GoNextWayPoint()
    {
        index++;
        index %= waypoints.Length;
    }

}
