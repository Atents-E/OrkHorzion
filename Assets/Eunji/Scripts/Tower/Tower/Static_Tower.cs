using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Static_Tower(Poison_Tower) 기능
// 1. 타워의 범위 안에 몬스터가 들어오면 확인
// 2. 몬스터를 향해 투사체를 발사

public class Static_Tower : TowerBase
{
    protected override void Awake()
    {
        BulletPrefabPos = transform.GetChild(0);            // 투사체 생성 위치

        enemys = new List<Transform>();
    }

    protected override void Update()
    {
        StaticAttack();
    }

    void StaticAttack()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            attackTarget = enemys[i].transform;
            Attack();
        }
    }
}
