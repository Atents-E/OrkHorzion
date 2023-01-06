using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

// ProjectileBase 기능
// 1. 생성되면 생성시킨 클래스의 타겟으로 날아감 
// 2. 타겟과 만다면 특정 영향을 타겟에게 미침
// 3. 생성되고 3초 뒤에 자동 삭제
// 4. 타겟과 만나면 즉시 삭제된다

public class ProjectileBase : MonoBehaviour
{
    public float attackPower = 10.0f;   // 투사체 공격력
    public float speed = 2.0f;          // 투사체 속도
    public float lifeTime = 2.0f;       // 투사체 유지 시간

    protected GameObject target;        // 발사체와 만날 타겟(적)

    TowerBase parentTarget;             // 부모의 타겟


    protected virtual void Awake()    
    {
        parentTarget = GetComponentInParent<TowerBase>();
        target = parentTarget.target;

        Destroy(this.gameObject, lifeTime); // 생성되면 lifeTime 뒤에 삭제
    }

    protected void Update()
    {
        Vector3 dir = (transform.position - target.transform.position).normalized;   // Monster에게 가는 방향벡터
        dir.y += 0f;

        transform.Translate(speed * Time.fixedDeltaTime * dir);         // 투사체 이동

        Debug.Log($"{target.transform.position}");
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
        {
            //// Enemey의 HP는 감소
            MonsterBase monster = other.GetComponent<MonsterBase>();
            //float MonsterHp = monster.MonsterHp;

            //if (MonsterHp != 0)
            //{
            //    MonsterHp -= attackPower;

            //    if (MonsterHp < 0)
            //    {
            //        MonsterHp = 0;
            //    }
            //}

            //Debug.Log($"{MonsterHp}");

            Destroy(this.gameObject); // 적과 충돌하면 발사체 삭제
        }
    }
}
