using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform target;

    bool isMoving = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Start()
    {
        EnemyBase enemy = GetComponent<EnemyBase>();
        enemy.OnMoving += IsMove;
    }

    private void Update()
    {
        if (isMoving)
        {
            agent.SetDestination(target.position);
        }
        
    }

    void IsMove(bool temp)
    {
        isMoving = temp;
    }
}
