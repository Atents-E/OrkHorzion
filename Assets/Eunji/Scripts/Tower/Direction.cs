using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public float projectileSpeed = 0.3f;      // ����ü�� �ӵ�
    public float projectileCreate = 0.1f;     // ����ü�� �����ð�


    Transform projectile;                       // ����ü(����ü�� ���� ��ġ)
    Transform target = null;                    // ����ü�� Ÿ��(����ü�� ���� �� ����)

    GameObject projentile;

    private void Awake()
    {
        projectile = GetComponent<Transform>();     // ������ ����ü�� �޾ƿ�
    }


    private void FixedUpdate()
    {
        if (target != null)              // target�� �ִٸ�
        {
            Vector3 dir = target.position - projectile.position;      // Player���� ���� ���⺤��
            dir.y = 0;

            projectile.position = dir * (projectileSpeed * Time.fixedDeltaTime);   // ����ü�� Playr�� ���� ���󰡶�

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            target = other.transform;
            if (projectile != null)
            {
                //Destroy(projentile);                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            target = null;
        }
    }
 

}
