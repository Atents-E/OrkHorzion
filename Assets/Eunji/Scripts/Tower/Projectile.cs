using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float initialSpeed = 20.0f;      // ó���ӵ�
    Rigidbody rigid;                        // ������ٵ�

    private void Awake()    // �� ��ũ��Ʈ�� ���� �Ϸ�Ǹ� 
    {
        rigid = GetComponent<Rigidbody>();  // ������ٵ� �Ҵ�
    }

    private void Start()    // ù ��° ������Ʈ ����
    {
        rigid.velocity = transform.forward * initialSpeed;
    }

    private void OnCollisionEnter(Collision collision) // �浹�� �Ͼ��
    {
        if (collision.gameObject.CompareTag("Enemy"))   // �浹�� Enemy�� �Ͼ�ٸ�
        {
            // Enemey�� HP�� ����

        }
        Destroy(this.gameObject, 2.0f); // 2�� �Ŀ� ��������

    }
}
