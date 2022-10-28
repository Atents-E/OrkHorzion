using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 3.0f;      // ���Ͱ� �����̴� �ӵ�
    public int monsterHp = 100;         // ���� �ִ� HP
    public float AttackRadius = 2.3f;   // ���Ͱ� ���ݰ����� ����(������)
    public float AttackDemage = 10.0f;  // ���Ͱ� �����Ҷ��� ������
    public float currentAngle = 30.0f;             // �ʴ� �ٲ�� ����
    protected bool looktargetOn = false;           // ���Ͱ� �÷��̾ �ٶ󺸴��� 
    public float sightHalfAngle = 50.0f;           // ������

    protected Transform playerTarget = null;       // �÷��̾ ����
    
    

    //float random;
    //public float Random { get => UnityEngine.Random.Range(0, 1); }      // ����2�� ������ �Ҵ�

    public int MonsterHP        // HP ������Ƽ
    {
        get => monsterHp;
        set
        {
            monsterHp = value;

            if (monsterHp < 0)
            {
                monsterHp = 0;

                Die(); // ��� ó�� �Լ� ȣ��
            }
        }
    }


    void Die()
    {
        looktargetOn = false;
        Destroy(gameObject, 3.0f);  // 3�ʵڿ� ���� ������Ʈ ����    
    }

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
