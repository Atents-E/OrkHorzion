using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using Unity.VisualScripting;

#if UNITY_EDIOR
using UnityEditor;  // UNITY_EDIOR��� ��ó���Ⱑ �����Ǿ����� ���� ��������� �־��
#endif

public class TowerBase : MonoBehaviour
{
    public int goal = 10;                   // Ÿ�� ����
    public float turnSpeed = 10.0f;         // Ÿ���� ȸ���ӵ�
    public float sightRadius = 5.0f;        // Ÿ���� ����

    public GameObject projectile;           // ����ü ������
    public float attackSpeed = 0.5f;        // Ÿ���� ���� �ӵ�
    public float fireAngle = 10.0f;         // Ÿ���� ���� ����
    float currentAngle = 0.0f;              // ������ ó�� ����
    bool isFiring = false;                  // �߻� ������ Ȯ��

    public float sightRange = 10.0f;


    IEnumerator fireCoroutine;              // �ڷ�ƾ�� ������ ������ ������ �־�� ��.
    Transform direction;                    // Ÿ�� ����
    Transform directionTransform;           // ����ü Ʈ������

    Transform target = null;        
    Vector3 initialForward;

    private void Awake()
    {
        direction = transform.GetChild(1);          // ������ ���� ��ġ�� �޾ƿ�
        directionTransform = direction.GetChild(0); // ����ü �߻� ��ġ �޾ƿ�
        
        fireCoroutine = PeriodFire();
    }

    private void Start()
    {
        initialForward = transform.forward;
        SphereCollider col = GetComponent<SphereCollider>();
        col.radius = sightRadius;

        StartCoroutine(fireCoroutine);
    }

    /// <summary>
    ///  �������� â���� ���� ���������� ����Ǿ��� �� ȣ��Ǵ� �Լ�  //�ν����� û������ ���� ����Ǵ� ����(��� ��) �ٷιٷ� Ȯ�� ����
    /// </summary>
    private void OnValidate()
    {
        SphereCollider col = GetComponent<SphereCollider>();
        if (col != null)
        {
            col.radius = sightRadius;
        }
    }

    private void Update()
    {
        LookTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))              // ���� �ȿ� Enemy�� ������
        {
            target = other.transform;               // target = Enemy
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))              // ���� �ȿ� Enemy�� ������
        {
            target = null;                          // target ����
        }
    }

    private void LookTarget()
    {
        if (target != null)
        {
            // ������ ����ϴ� ���(��ӵ��� ȸ��)
            Vector3 shotToMonsterDir = target.position 
                - direction.position;  // �ѱ����� �÷��̾��� ��ġ�� ���� ���� ���� ���
            shotToMonsterDir.y = 0;

            // �������� �� 0~180��. �������� �� 0~-180��        //�޼� ��ǥ�迡�� ���� �հ����� ���� ���� ��, �ٸ� �հ����� �ð� �������� �����.
            float betweenAngle = Vector3.SignedAngle(direction.forward, shotToMonsterDir, direction.up);

            Vector3 resultDir;
            if (Mathf.Abs(betweenAngle) > 1.0f)    // ���̰��� ���� ���� �������� üũ
            {
                // ���̰��� ����� ������ ���
                float rotateDirection = 1.0f;   //�ϴ� +����(������, �ð����)���� ����
                if (betweenAngle < 0)
                {
                    rotateDirection = -1.0f;    // betweenAngle�� -�� rotateDirection�� -1��
                }

                // �ʴ� turnSpeed��ŭ ȸ���ϴµ� rotateDirection�� �ð�������� ȸ������ �ݽð� �������� ȸ������ ����
                currentAngle += (rotateDirection * turnSpeed * Time.deltaTime);

                resultDir = Quaternion.Euler(0, currentAngle, 0) * initialForward;
            }
            else
            {
                //���̰��� ���� 0�� ���
                resultDir = shotToMonsterDir;
            }
            direction.rotation = Quaternion.LookRotation(resultDir);

            

            // �÷��̾ �߻簢 �ȿ� ������ ���� ����. ������ ����
            if (!isFiring && IsInFireAngle())
            {
                FireStart();
            }
            if (isFiring && !IsInFireAngle())
            {
                FireStop();
            }
        }
    }

    bool IsInFireAngle()        // �߻簢 �ȿ� �ִ��� Ȯ���ϴ� �뵵�� �Լ�
    {

        Vector3 dir = target.position - direction.position;
        // Debug.Log(Vector3.Angle(direction.forward, dir));
        return Vector3.Angle(direction.forward, dir) < fireAngle;
    }

    private void Fire()
    {
        Instantiate(projectile, directionTransform.position, directionTransform.rotation);
    }

    IEnumerator PeriodFire()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private void FireStart()
    {
        isFiring = true;                // �߻� ���̸�
        StartCoroutine(fireCoroutine);  // �ڷ�ƾ ����
    }

    private void FireStop()
    {
        StopCoroutine(fireCoroutine);   // �ڷ�ƾ �ߴ�
        isFiring = false;               // �߻� ������ ������
    }

    private void OnDrawGizmos/*Selected*/()
    {
#if UNITY_EDITOR

        Handles.color = Color.green; 

        Handles.DrawWireDisc(transform.position, transform.up, sightRange, 3.0f);     //�þ� �ݰ游ŭ �� �׸��� //���� �ϳ��� ����
#endif  
    }

}
