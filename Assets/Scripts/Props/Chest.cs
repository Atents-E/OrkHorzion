using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // 애니메이터 선언
    Animator anim;
    ParticleSystem particle;

    private void Awake()
    {
        // 애니메이터 추가
        anim = GetComponent<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // "Player"라는 태그가 닿으면
        if (other.CompareTag("Player"))
        {
            // Open 애니메이션 활성화
            anim.SetTrigger("Open");
            particle.Play();

            // 활성화 후 0.5초 후 사라짐
            Destroy(this.gameObject, 1.3f);
        }
    }



}
