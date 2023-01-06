using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Poison_Tower 기능
// 1. 타워의 범위 안에 들어오면 isFiring = true

public class Poison_Tower : TowerBase
{
    protected override void Awake()
    {
        base.Awake();
        porPos = transform.GetChild(0).position;
    }

protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if( target != null)
        {
            isFiring = true;
        }
    }
}
