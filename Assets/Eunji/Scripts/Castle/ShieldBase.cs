using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

// ShieldBase ���
// 1. ���� ��ġ�� ����Ÿ���� �������� �Ѵ�
// 2. ���� ���ڸ��� ȸ��
// 3. HP�� 0�� ���ų� ������ �ش� ������Ʈ�� �����Ѵ�.
// 4. ���� ���ϸ� �� ������ Ÿ�� ����Ʈ�� �����ش�.

public class ShieldBase : MonoBehaviour
{
    protected int goal = 100;               // �ǵ� �ݾ�
    protected float shildHP = 300.0f;       // �ǵ� HP
    public float rotateSpeed = 30.0f;       // ���� ȸ�� �ӵ�
    public float removeSpeed = 30.0f;       // ���� ���� ���ǵ�
    public float removeTime = 5.0f;       // ���� ���� �ð�

    public float remaining = 0.0f;

    Transform shield;       // �ǽ� ��ġ
    Castle castle;          // ����Ÿ��
    SphereCollider collider;

    public float ShildHP        
    {
        get => shildHP;
        set
        {
            ShildHP = value;
            if (shildHP == 0)               // �ǵ� ü�� 0�̸�
            {
                if(shildHP < 0)             // 0���� ������  
                {
                    remaining = shildHP - value;    // ���� �������� ����
                    castle.hp -= remaining;         // ���� ��������ŭ ����Ÿ�� ü���� ���̱�
                }

                RemoveDelay();              // �ǵ� ũ�Ⱑ �پ���
                Destroy(this.gameObject, removeTime);   // 5�� �� �ǵ� ����
            }
        }
    }

    private void Awake()
    {
        shield = GetComponent<Transform>();
        castle = FindObjectOfType<Castle>();
        collider = GetComponent<SphereCollider>();

        transform.position = castle.transform.position + new Vector3(0, 0.5f, 0);   // Ÿ�� ��ġ
    }

    private void Update()
    {
        shield .Rotate(rotateSpeed, rotateSpeed, rotateSpeed * Time.deltaTime);
    }

    protected virtual IEnumerator RemoveDelay()    // �ǵ尡 �����Ǵµ� �ɸ��� �ð�
    {
        while (true)            
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)              // �ǵ�� �浹�Ѱ� ���� �����̶�� 
    {
        if (collision.gameObject.CompareTag("MonsterAttack"))
        {
            MonsterBase monster = GetComponent<MonsterBase>();      // 
            ShildHP -= monster.attackPower;                         // ���� ���ݷ¸�ŭ hp����
        }
    }

}
