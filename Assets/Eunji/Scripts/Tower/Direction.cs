using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public float projectileSpeed = 0.3f;    // ����ü�� �ӵ�
    public float projectileCreate = 0.1f;   // ����ü�� �����ð�


    Transform projectile;                   // ����ü(����ü�� ���� ��ġ)
    Transform target = null;                // ����ü�� Ÿ��(����ü�� ���� �� ����)

    GameObject projentile;

    private void Awake()
    {
        projectile = GetComponent<Transform>(); // ������ ����ü�� �޾ƿ�
    }


    private void FixedUpdate()
    {
        if (target != null)                     // target�� �ִٸ�
        {
            Vector3 dir = target.position - projectile.position;                    // Enemy���� ���� ���⺤��
            dir.y = 0;

            projectile.position = dir * (projectileSpeed * Time.fixedDeltaTime);    // ����ü�� Enemy�� ���� ���󰡶�

        }
    }

    private void OnTriggerEnter(Collider other) // other�� Ʈ���ſ� ������
    {   
        if (other.CompareTag("Enemy"))          // other�� �±� Enemy���
        {
            target = other.transform;           // target�� other�� ��ġ
            if (projectile != null)             // ����ü�� �ִٸ�
            {
                //Destroy(projentile);                
            }
        }
    }

    private void OnTriggerExit(Collider other)  // other�� Ʈ���ſ��� ������
    {
        if (other.CompareTag("Enemy"))          // other�� �±� Enemy���
        {
            target = null;                      // target�� ����
        }
    }
 

}
