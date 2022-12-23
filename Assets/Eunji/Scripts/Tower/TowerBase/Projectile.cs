using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float attackPower = 10.0f;       // �߻�ü ���ݷ�
    public float initialSpeed = 20.0f;      // ó�� �ӵ�

    Rigidbody rigid;                        // ������ٵ�
    GameObject target;                      // �߻�ü�� ���� Ÿ��(��)

    private void Awake()    // �� ��ũ��Ʈ�� ���� �Ϸ�Ǹ� 
    {
        rigid = GetComponent<Rigidbody>();  // ������ٵ� �Ҵ�
    }

    private void Start()    // ù ��° ������Ʈ ����
    {
        rigid.velocity = transform.forward * initialSpeed;
    }

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

    private void OnCollisionEnter(Collision collision) // �浹�� �Ͼ��
    {
        if (collision.gameObject.CompareTag("Enemy"))   // �浹�� Enemy�� �Ͼ�ٸ�
        {
            // target�� collision
            target = collision.gameObject;

            // Enemey�� HP�� ����
            MonsterBase monster = GetComponent<MonsterBase>();
            monster.monsterHp -= attackPower;
        }
        Destroy(this.gameObject); // �߻�ü ����

    }
}
