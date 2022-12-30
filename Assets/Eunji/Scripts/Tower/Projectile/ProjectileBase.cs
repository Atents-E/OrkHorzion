using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float attackPower = 10.0f;       // �߻�ü ���ݷ�
    public float initialSpeed = 20.0f;      // ó�� �ӵ�

    protected Rigidbody rigid;                        // ������ٵ�
    protected GameObject target;                      // �߻�ü�� ���� Ÿ��(��)

    private void Awake()    
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

    protected void OnCollisionEnter(Collision collision) // �浹�� �Ͼ��
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
