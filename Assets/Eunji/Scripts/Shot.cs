using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shot : MonoBehaviour
{
    public float attack = 10.0f;        // �Ѿ��� ���ݷ�

    public float shotSpeed = 0.3f;      // �Ѿ� �ӵ�
    public float shotCreate = 0.1f;     // �Ѿ��� �����ð�

    Transform target = null;            // �Ѿ��� Ÿ��(�Ѿ��� ���� �� ����)
    Transform shot;                     // �Ѿ�(�Ѿ��� ���� ��ġ)

    private void Awake()
    {
        shot = transform.GetChild(0).transform.GetChild(3);     // �Ѿ��� ��ġ �޾ƿ�
    }


    private void FixedUpdate()
    {
        if (target != null)              // target�� �ִٸ�
        {
            Vector3 dir = target.position - shot.position;      // Player���� ���� ���⺤��
            dir.y = 0;

            shot.position = dir * (shotSpeed * Time.fixedDeltaTime);   // �Ѿ��� Playr�� ���� ���󰣴�.

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            target = other.transform;
            if (shot != null)
            {
                Destroy(shot); 
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
}
