using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ü�� ������ ����
public class Direction_0000 : MonoBehaviour
{

    public float projectileSpeed = 0.3f;    // ����ü�� �ӵ�
    public float projectileCreate = 0.1f;   // ����ü�� �����ð�

    Transform projectile;                   // ����ü(����ü�� ���� ��ġ)
    Transform target = null;                // ����ü�� Ÿ��(����ü�� ���� �� ����)


    private void Awake()
    {
        projectile = GetComponent<Transform>(); // ������ ����ü�� �޾ƿ�
    }


    private void FixedUpdate()
    {
        if (target != null )                                   // target�� �ְ�, ����ü�� ���� �Ǿ��ٸ�
        {
            Vector3 dir = target.position - projectile.position;                    // Enemy���� ���� ���⺤��
            dir.y = 0;

            projectile.position = dir * (projectileSpeed * Time.fixedDeltaTime);    // ����ü�� Enemy�� ���� �̵�
        }
    }

    //private void OnTriggerEnter(Collider other) // other�� Ʈ���ſ� ������
    //{   
    //    if (other.gameObject.CompareTag("Enemy"))          // other�� �±� Enemy���
    //    {
    //        target = other.gameObject;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))   // �浹�� Enemy�� �Ͼ�ٸ�
    //    {
    //        // target�� other
    //        target = other.gameObject;

    //        // Enemey�� HP�� ����
    //        MonsterBase monster = GetComponent<MonsterBase>();
    //        monster.monsterHp -= attackPower;
    //    }
    //    Destroy(this.gameObject); // �߻�ü ����

    //}

    private void OnTriggerExit(Collider other)  // other�� Ʈ���ſ��� ������
    {
        if (other.CompareTag("Enemy"))          // other�� �±� Enemy���
        {
            target = null;                      // target�� ����
        }
    }
 

}
