using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ShortProjectile 기능
// 1. ProjectileBase를 상속받고
// 2. 몬스터와 충돌하면 몬스터에게 끼치는 영향 오버라이딩

public class Cannon_Projectile : ProjectileBase
{

    protected override void Awake()
    {
        attackPower = 10.0f;
        speed = 2.0f;
        lifeTime = 2.0f;

        base.Awake();
    }


    //protected override void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
    //    {

    //        base.OnTriggerEnter(other);
    //    }
    //}

}
