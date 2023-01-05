using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

// ProjectileBase ���
// 1. �����Ǹ� ������Ų Ŭ������ Ÿ������ ���ư� 
// 2. Ÿ�ٰ� ���ٸ� Ư�� ������ Ÿ�ٿ��� ��ħ
// 3. �����ǰ� 3�� �ڿ� �ڵ� ����
// 4. Ÿ�ٰ� ������ ��� �����ȴ�

public class ProjectileBase : MonoBehaviour
{
    public float attackPower = 10.0f;   // �߻�ü ���ݷ�
    public float speed = 0.2f;          // ó�� �ӵ�
    public float lifeTime = 2.0f;

    protected GameObject target;        // �߻�ü�� ���� Ÿ��(��)

    TowerBase parentTarget;             // �θ��� Ÿ��


    protected virtual void Awake()    
    {
        parentTarget = GetComponentInParent<TowerBase>();
        target = parentTarget.target;

        Destroy(this.gameObject, lifeTime); // �����Ǹ� 5�� �ڿ� �߻�ü ����
    }


    protected void Update()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;   // Monster���� ���� ���⺤��
        dir.y += 0.5f;

        transform.Translate(speed * Time.fixedDeltaTime * dir);         // ����ü �̵�

        // Debug.Log($"{target.transform.position}");
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))   // �浹�� Enemy�� �Ͼ�ٸ�
        {
            //// Enemey�� HP�� ����
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

            Destroy(this.gameObject); // ���� �浹�ϸ� �߻�ü ����
        }
    }
}
