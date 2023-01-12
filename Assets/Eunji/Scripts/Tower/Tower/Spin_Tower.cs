using System.Collections;
using UnityEditor;
using UnityEngine;

// Spin_Tower(Cannon_Tower, Crossbow_Tower) 기능
// 1. 범위 안의 몬스터 확인하여 타겟으로 설정
// 2. 몬스터를 향해서 회전
// 3. 몬스터를 향해 투사체를 발사


public class Spin_Tower : TowerBase
{
    protected override void Update()
    {
        LookTarget();
    }
}
