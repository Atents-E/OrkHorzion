using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // �ִϸ����� ����
    Animator anim;

    private void Awake()
    {
        // �ִϸ����� �߰�
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // "Player"��� �±װ� ������
        if (other.CompareTag("Player"))
        {
            // Open �ִϸ��̼� Ȱ��ȭ
            anim.SetTrigger("Open");

            // Ȱ��ȭ �� 0.5�� �� �����
            Destroy(this.gameObject, 1.3f);
        }
    }



}
