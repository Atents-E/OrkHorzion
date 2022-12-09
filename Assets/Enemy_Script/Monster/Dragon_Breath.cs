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
        Debug.Log("����");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IBattle target = other.GetComponent<IBattle>();
            Debug.Log("�÷��̾ �극�� ���ݹ��� �ȿ� �ִ�.");
        }
    }
}
