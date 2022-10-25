using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Navigation : MonoBehaviour
{
    Transform[] waypoints;
    int index = 0;

    //public Transform target;             // �׺�Ž� Ÿ��
    protected NavMeshAgent agent;          // �׺�Ž�

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
        //SetTarget(waypoints[targetIndex]);  // ù ��������Ʈ ����
        agent.SetDestination(waypoints[index].position);
    }

    private void Update()
    {
        transform.LookAt(waypoints[index]);   // �������� �ٶ�

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)  // ��� ����� �Ϸ��� ���� ������������ �����Ǵ� �Ÿ����� �̵����� �ʾҴ�.
        {
            // WaypointTarget = wayPoint.MoveNext();   // ���� ��������Ʈ ������ moveTarget�� ����
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
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)  // ��� ����� �Ϸ��� ���� ������������ �����Ǵ� �Ÿ����� �̵����� �ʾҴ�.
        {
           // WaypointTarget = wayPoint.MoveNext();   // ���� ��������Ʈ ������ moveTarget�� ����
            
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
