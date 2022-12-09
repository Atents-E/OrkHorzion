using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Breath : MonoBehaviour
{
    Dragon dragon;

    private void Awake()
    {
        dragon = GetComponentInParent<Dragon>();
    }
    private void Start()
    {
        Debug.Log("범위");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IBattle target = other.GetComponent<IBattle>();
            Debug.Log("플레이어가 브레스 공격범위 안에 있다.");
        }
    }
}
