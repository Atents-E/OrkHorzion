using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlowlyProjectile : ProjectileBase
{
    public float reduceSpeed = 0.003f;    // ���� ��Ű�� ���ǵ�
    public float reduceAttack = 0.003f;    // ���� ��Ű�� ���ݷ�
    public float reduceTiem = 5.0f;	    // ���� ��Ű�� �ð� 

    bool isReduceTime = false;

    protected void OnCollisionEnter(Collision collision) // �浹�� �Ͼ��
    {
        if (collision.gameObject.CompareTag("Enemy"))   // �浹�� Enemy�� �Ͼ�ٸ�
        {
            // target�� ����
            target = collision.gameObject;

            // �߻�ü ����
            Destroy(this.gameObject);

            MonsterBase monster = GetComponent<MonsterBase>();
 
            for(int i = 0; i < reduceTiem; i++)     // ���ӽð� ����
            {
                // Monster�� �̼Ӱ� ���� ����
                reduceSpeed -= Time.deltaTime;
                isReduceTime = false;
            }

            isReduceTime = true;
            monster.speed -= reduceSpeed;
            monster.attackSpeed -= reduceAttack;
        }
    }

}
