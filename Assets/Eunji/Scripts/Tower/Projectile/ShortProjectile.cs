using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ShortProjectile ���
// 1. ProjectileBase�� ��ӹް�
// 2. ���Ϳ� �浹�ϸ� ���Ϳ��� ��ġ�� ���� �������̵�

public class ShortProjectile : ProjectileBase
{

    protected override void Awake()
    {
        base.Awake();

    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))   // �浹�� Enemy�� �Ͼ�ٸ�
        {
            // Enemey�� HP�� ����
            MonsterBase monster = other.GetComponent<MonsterBase>();
            float MonsterHp = monster.MonsterHp;

            if (MonsterHp != 0)
            {
                MonsterHp -= attackPower;

                if (MonsterHp < 0)
                {
                    MonsterHp = 0;
                }
            }

            // Debug.Log($"{MonsterHp}");

            
            base.OnTriggerEnter(other);
        }
    }

}
