using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float initialSpeed = 20.0f;      // 처음속도
    Rigidbody rigid;                        // 리지드바디

    private void Awake()    // 이 스크립트가 생성 완료되면 
    {
        rigid = GetComponent<Rigidbody>();  // 리지드바디 할당
    }

    private void Start()    // 첫 번째 업데이트 전에
    {
        rigid.velocity = transform.forward * initialSpeed;
    }

    private void OnCollisionEnter(Collision collision) // 충돌이 일어나면
    {
        if (collision.gameObject.CompareTag("Enemy"))   // 충돌이 Enemy와 일어났다면
        {
            // Enemey의 HP는 감소

        }
        Destroy(this.gameObject, 2.0f); // 2초 후에 없어져라

    }
}
