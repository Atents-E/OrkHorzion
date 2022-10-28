using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Navigation : MonoBehaviour
{
    private WayPoint waypoint;
    public Transform target;             // �׺�Ž� Ÿ��
    protected NavMeshAgent agent;          // �׺�Ž�
    int index = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();           // �׺�Ž� ������Ʈ ã��
        waypoint = FindObjectOfType<WayPoint>();        // ��������Ʈ ��ũ��Ʈ ã��
    }

    private void Start()
    {
        agent.SetDestination(waypoint.Current.position);        // ù ��° ��������Ʋ ���� �ɾ
    }   

    public void Stopped(bool goStop)        // �ɾ�� ������ ���ϴ� �Լ�
    {
        agent.isStopped = goStop;
        agent.velocity = Vector3.zero;
    }

    public void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)  // ��� ����� �Ϸ��� ���� ������������ �����Ǵ� �Ÿ����� �̵����� �ʾҴ�.
        {
            // WaypointTarget = wayPoint.MoveNext();   // ���� ��������Ʈ ������ moveTarget�� ����
            index++;        // �ε����� 1 ���� ��Ų��.
            index %= waypoint.transform.childCount;
            agent.SetDestination(waypoint.wayPoints[index].position);   // ���� ��������Ʈ�� ���� �ɾ��.
        } 
    }
}
