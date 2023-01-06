using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ShortProjectile 기능
// 1. ProjectileBase를 상속받고
// 2. 몬스터와 충돌하면 몬스터에게 끼치는 영향 오버라이딩

public class ShortProjectile : ProjectileBase
{

    protected override void Awake()
    {
        base.Awake();

    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
        {
            // Enemey의 HP는 감소
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
