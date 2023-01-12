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

public class ProjectileBase : MonoBehaviour, IBattle
{
    public float attackPower = 10.0f;   // 투사체 공격력
    public float speed = 5.0f;          // 투사체 속도
    public float lifeTime = 2.0f;       // 투사체 유지 시간

    protected GameObject target;        // 발사체와 만날 타겟(적)

    protected TowerBase parentTarget;             // 부모의 타겟

    MonsterBase monster;

    public float AttackPower => attackPower;

    public float DefencePower => 0.0f;

    protected virtual void Awake()    
    {
        Destroy(this.gameObject, lifeTime); // 생성되면 lifeTime 뒤에 삭제
    }

    protected void Update()
    {
        transform.Translate(speed * Time.deltaTime * transform.forward, Space.World);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
        {            
            IBattle target = other.gameObject.GetComponent<IBattle>();
            Attack(target);


            Destroy(this.gameObject); // 적과 충돌하면 발사체 삭제
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IBattle target = collision.gameObject.GetComponent<IBattle>();
            Attack(target);
            Destroy(this.gameObject);
        }
    }

    public void Attack(IBattle target)
    {
        target?.TakeDamage(AttackPower);
    }

    public void TakeDamage(float damage)
    {
        monster.MonsterHp -= damage;
    }
}
