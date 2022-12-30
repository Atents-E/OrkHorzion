using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float attackPower = 10.0f;       // 발사체 공격력
    public float initialSpeed = 20.0f;      // 처음 속도

    protected Rigidbody rigid;                        // 리지드바디
    protected GameObject target;                      // 발사체와 만날 타겟(적)

    private void Awake()    
    {
        rigid = GetComponent<Rigidbody>();  // 리지드바디 할당
    }

    private void Start()    // 첫 번째 업데이트 전에
    {
        rigid.velocity = transform.forward * initialSpeed;
    }

    //private void OnTriggerEnter(Collider other) 
    //{
    //    if (other.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
    //    {
    //        // target은 other
    //        target = other.gameObject;

    //        // Enemey의 HP는 감소
    //        MonsterBase monster = GetComponent<MonsterBase>();
    //        monster.monsterHp -= attackPower;
    //    }
    //    Destroy(this.gameObject); // 발사체 삭제

    //}

    protected void OnCollisionEnter(Collision collision) // 충돌이 일어나면
    {
        if (collision.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
        {
            // target은 collision
            target = collision.gameObject;

            // Enemey의 HP는 감소
            MonsterBase monster = GetComponent<MonsterBase>();
            monster.monsterHp -= attackPower;
        }
        Destroy(this.gameObject); // 발사체 삭제
    }
}
