using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Dragon_Breath : MonoBehaviour
{
    Dragon dragon;

    float breathTime;


    private void Awake()
    {
        dragon = GetComponentInParent<Dragon>();
    }

    private void Update()
    {
        breathTime += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && breathTime > 0.25)
        {
            IBattle target = other.GetComponent<IBattle>();
            dragon.Attack(target);
            breathTime = 0.0f;
            Debug.Log("플레이어가 브레스 공격범위 안에 있다.");
        }
    }
}
