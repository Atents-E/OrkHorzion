using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

public class EnemyBase : MonoBehaviour, IBattle, IHealth
{
    //float moveSpeed = 3.0f;                     // ���Ͱ� �����̴� �ӵ�
    float AttackRadius = 2.3f;                    // ���Ͱ� ���ݰ����� ����(������)
    public float currentAngle = 30.0f;            // �ʴ� �ٲ�� ����
    protected bool looktargetOn = false;          // ���Ͱ� �÷��̾ �ٶ󺸴��� 
    //float sightHalfAngle = 50.0f;               // ������
    
    public float attackPower = 10.0f;             // ���ݷ�
    public float defencePower = 0.0f;             // ����
    public float monsterHp = 100.0f;              // ���� HP
    public float monsterMaxHp = 100.0f;           // ���� �ִ� HP


    protected Transform playerTarget = null;      // �÷��̾ ����
    

    //float random;
    //public float Random { get => UnityEngine.Random.Range(0, 1); }      // ����2�� ������ �Ҵ�

    public float AttackPower => attackPower;
    public float DefencePower => defencePower;
    public float MaxHP => monsterMaxHp;

    /// <summary>
    /// HP ������Ƽ ������HP�� 0���� �۾����� �״´�.
    /// </summary>
    public float HP
    {
        get => monsterHp;
        set
        {
            if (monsterHp != value)
            {
                monsterHp = value;

                if (monsterHp < 0)
                {
                    monsterHp = 0;
                    Die();
                }

                onHealthChange?.Invoke(monsterHp/monsterMaxHp);

                monsterHp = Mathf.Clamp(monsterHp, 0.0f, monsterMaxHp);
            }
        }
    }


    // ��������Ʈ
    public Action<float> onHealthChange { get; set; }
    public Action onDie { get; set; } 


    //void Die()
    //{
    //    looktargetOn = false;
    //    Destroy(gameObject, 3.0f);  // 3�ʵڿ� ���� ������Ʈ ����    
    //}

    protected bool SearchPlayer()
    {
        bool result = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, AttackRadius, LayerMask.GetMask("Player"));

        if (colliders.Length > 0)
        {
            Vector3 playerPos = colliders[0].transform.position;    // �÷��̾��� ��ġ
            Vector3 toPlayerDir = playerPos - transform.position;   // �÷��̾�� ���� ����

            // �þ߰� �ȿ� �÷��̾ �ִ��� Ȯ��
            if (IsInsightAngle(toPlayerDir))
            {
                if (!IsSightBlocked(toPlayerDir))
                {
                    result = true;
                }
            }
        }
        return result;
    }

    protected bool IsInsightAngle(Vector3 toPlayerDir)
    {
        float angle = Vector3.Angle(transform.position, toPlayerDir);   // forward ���Ϳ� �÷��̾�� ���� ���� ������ ���̰� ���ϱ�
        return (AttackRadius > angle);
    }

    protected bool IsSightBlocked(Vector3 toPlayerDir)
    {
        bool result = true;

        Ray ray = new(transform.position + transform.up * 0.5f, toPlayerDir);
        if (Physics.Raycast(ray, out RaycastHit hit, AttackRadius))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return result;
            }
        }

        return result;
    }

    protected void Looktarget()
    {
        if (looktargetOn)
        {
            if (playerTarget != null)
            {
                Vector3 dir = (playerTarget.position - transform.position).normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), currentAngle * Time.deltaTime);

            }
        }
    }

    /// <summary>
    /// ���ݿ� �Լ�
    /// </summary>
    /// <param name="target">������ ���</param>
    public void Attack(IBattle target)
    {
        target.TakeDamage(AttackPower);
    }

    /// <summary>
    /// ���� �Լ�
    /// </summary>
    /// <param name="damage">���� ���� ������</param>
    public void TakeDamage(float damage)
    {
        // �⺻ ���� : ���� �Դ� ������ = �� ���� ������ - ����
        HP -= (damage - defencePower);
    }

    /// <summary>
    /// �׾��� �� ����� �Լ�
    /// </summary>
    public void Die()
    {
        onDie?.Invoke();
    }



    //    protected void OnDrawGizmosSelected()
    //    {
    //#if UNITY_EDITOR
    //        Handles.color = Color.red;
    //        Handles.DrawWireDisc(transform.position, transform.up, AttackRadius);

    //        if (SearchPlayer())
    //        {
    //            Handles.color = Color.yellow;
    //        }
    //#endif
    //    }


    //void LookTarget()       // �÷��̾� ���� �ٶ󺸱�
    //{
    //    if (looktargetOn)
    //    {
    //        if (playerTarget != null)
    //        {
    //            monsterToplayerDir = playerTarget.position - transform.position;
    //            monsterToplayerDir.y = 0;

    //            float betweenAngle = Vector3.SignedAngle(transform.forward, monsterToplayerDir, transform.up);

    //            Vector3 resultDir;

    //            if (Mathf.Abs(betweenAngle) < 10.0f)
    //            {
    //                float rotateDir = 1.0f;

    //                if (betweenAngle < 0)
    //                {
    //                    rotateDir = -1.0f;
    //                }

    //                currentAngle += (rotateDir * turnSpeed * Time.fixedDeltaTime);

    //                resultDir = Quaternion.Euler(0, currentAngle, 0) * transform.forward;
    //            }
    //            else
    //            {
    //                resultDir = monsterToplayerDir;
    //            }
    //            transform.rotation = Quaternion.LookRotation(resultDir);
    //        }

    //    }
    //}
}