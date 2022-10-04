using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // �ִϸ����� ����
    Animator anim;
    ParticleSystem particle;

    private void Awake()
    {
        // �ִϸ����� �߰�
        anim = GetComponent<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // "Player"��� �±װ� ������
        if (other.CompareTag("Player"))
        {
            // Open �ִϸ��̼� Ȱ��ȭ
            anim.SetTrigger("Open");
            particle.Play();

            // Ȱ��ȭ �� 0.5�� �� �����
            Destroy(this.gameObject, 1.3f);
        }
    }



}
