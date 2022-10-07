using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shot : MonoBehaviour
{
    /// <summary>
    /// �Ѿ��� ���ݷ�
    /// </summary>
    public float attack = 10.0f;

    public float shotSpeed = 0.3f;      // �Ѿ� �ӵ�
    public float shotCreate = 0.1f;     // �Ѿ��� �����ð�
    public Action onDie { get; set; }

    Transform target = null;            // �Ѿ��� Ÿ��(�Ѿ��� ���� �� ����)
    Transform shot;                     // �Ѿ�(�Ѿ��� ���� ��ġ)

    private void Awake()
    {
        shot = transform.GetChild(1).transform.GetChild(0);     // �Ѿ��� ��ġ �޾ƿ�
    }


    private void FixedUpdate()
    {
        if (target != null)              // target�� �ִٸ�
        {
            Vector3 dir = target.position - shot.position;      // Player���� ���� ���⺤��
            dir.y = 0;

            shot.position = dir * (shotSpeed * Time.fixedDeltaTime);   // �Ѿ��� Playr�� ���� ���󰡶�

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            target = other.transform;
            if (shot != null)
            {
                Destroy(shot);                      // Player�� ������ shot�� �������(shot�� �� ã�°� ����
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            target = null;
        }
    }


    //public void Die()       // IDead �������̽� ���
    //{
    //}
}
