using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDIOR
using UnityEditor;  // UNITY_EDIOR라는 전처리기가 설정되어있을 때만 실행버전에 넣어라
#endif

// Static_Tower(Poison_Tower) 기능
// 1. 타워의 범위 안에 몬스터가 들어오면 확인
// 2. 몬스터를 향해 투사체를 발사

public class Static_Tower : TowerBase
{
    protected override void Awake()
    {
        BulletPrefabPos = transform.GetChild(0);            // 투사체 생성 위치

        //enemys = new List<Transform>();
    }

    protected override void Update()
    {
        StaticAttack();
    }

    void StaticAttack()
    {
        if(enemy != null)
        {
            Attack();
        }
        //for (int i = 0; i < enemys.Count; i++)
        //{
        //    attackTarget = enemys[i].transform;
        //    Attack();
        //}
    }

    protected override void OnDrawGizmosSelected() // 기즈모
    {
#if UNITY_EDITOR

        Handles.color = Color.green;    // 초록색으로 표시

        if (attackTarget != null)       // 타겟이 있다면
        {
            Handles.color = Color.red;  // 이후에 작성된 빨간색으로 표시
        }

        Handles.DrawWireDisc(transform.position, transform.up, sightRange, 3.0f);     //시야 반경만큼 원 그리기 //원이 하나만 보임
#endif  
    }
}
