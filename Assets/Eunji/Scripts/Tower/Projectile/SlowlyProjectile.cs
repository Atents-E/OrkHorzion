using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlowlyProjectile : ProjectileBase
{
    public float reduceSpeed = 0.003f;    // ���� ��Ű�� ���ǵ�
    public float reduceAttack = 0.003f;    // ���� ��Ű�� ���ݷ�
    public float reduceTiem = 5.0f;	    // ���� ��Ű�� �ð� 

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
                //���� X (1 - �ۼ�Ʈ �� 100)
                monster.speed *= (1- reduceSpeed * 0.01f);
                monster.attackSpeed *= reduceAttack;

                reduceSpeed -= Time.deltaTime;
            }

        }
    }
}
