using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerBase : MonoBehaviour
{
    //����===========================================================================
    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    public int goal = 10;
    public float turnSpeed = 5.0f;          // Ÿ���� ȸ���ӵ�
    public float sightRadius = 5.0f;        // Ÿ���� ����

    public GameObject shotPrefab;           //�Ѿ� ������
    public float attackSpeed = 2.0f;        // Ÿ���� ���� �ӵ�
    public float fireAngle = 10.0f;         // Ÿ���� ���� ����
    float currentAngle = 0.0f;              // �ѱ��� ó�� ����
    bool isFiring = false;                  // �߻� ������ Ȯ��

    Transform fireTransform;            // �Ѿ��� Ʈ������(�Ѿ��� ��ġ�� ���� �ʿ�)
    IEnumerator fireCoroutine;          // �ڷ�ƾ�� ������ ������ ������ �־�� ��.
    Transform shotTransform;                //  �Ѿ��� Ʈ������

    /// <summary>
    /// Ÿ��x
    /// </summary>
    Transform target = null;        
    Vector3 initialForward;
    
    
    //���
    // ��ġ�� ������ ������ Ȯ��
    // ��ġ
    // ���� üũ
    // ���� ���� ���� ����


    private void Awake()
    {
        shotTransform = transform.GetChild(1);     // ������ �ѱ��� �޾ƿ�
        shotTransform = shotTransform.GetChild(0); // �Ѿ� �߻� ��ġ �޾ƿ�

        //fireCoroutine = PeriodFire();
    }

    // �÷��̾ ���� �ݰ� �ȿ� ������ �ش�������� �ѱ��� ������.
    // sightRadius �ƹ��� ������ ���� �ʴ´�.
    // turnSpeed�� �ƹ��� ������ ���� �ʴ´�. ( �ѱ��� ��� ȸ���Ѵ�. )

    //public float turnSpeed = 2.0f;    // ȸ�� �ӵ�
    //public float sightRadius = 5.0f;    // �ݰ�

    //public GameObject bulletPrefab;     // �Ѿ� ������
    //public float fireInterval = 0.5f;   // �Ѿ� �߻� ����
    //public float fireAngle = 10.0f;     // �߻� ����

    //Transform fireTransform;            // �Ѿ��� Ʈ������(�Ѿ��� ��ġ�� ���� �ʿ�)
    //IEnumerator fireCoroutine;          // �ڷ�ƾ�� ������ ������ ������ �־�� ��.
    //WaitForSeconds waitFireInterval;    // �ڷ�ƾ�� �������� ���̴� ���2
    //bool isFiring = false;              // �߻� ������ Ȯ��


    //Transform target = null;      // �÷��̾��� ��ġ ����
    //Transform barrelBody;        // �ѱ�

    //float currentAngle = 0.0f;
    ////float targetAngle = 0.0f;
    //Vector3 initialForward;


    private void Start()
    {
        initialForward = transform.forward;
        SphereCollider col = GetComponent<SphereCollider>();
        col.radius = sightRadius;
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

    /// <summary>
    /// Tag�� Ÿ�� Ȯ��
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            target = null;
        }
    }

    private void LookTarget()
    {
        if (target != null)
        {
            // ������ ����ϴ� ���(��ӵ��� ȸ��)
            Vector3 shotToMonsterDir = target.position - shotTransform.position;  // �ѱ����� �÷��̾��� ��ġ�� ���� ���� ���� ���
            shotToMonsterDir.y = 0;

            // �������� �� 0~180��. �������� �� 0~-180��        //�޼� ��ǥ�迡�� ���� �հ����� ���� ���� ��, �ٸ� �հ����� �ð� �������� �����.
            float betweenAngle = Vector3.SignedAngle(shotTransform.forward, shotToMonsterDir, shotTransform.up);

            Vector3 resultDir;
            if (Mathf.Abs(betweenAngle) > 0.05f)    // ���̰��� ���� ���� �������� üũ
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
            shotTransform.rotation = Quaternion.LookRotation(resultDir);

            

            // �÷��̾ �߻簢 �ȿ� ������ � ����. ������ ����
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
        Vector3 dir = target.position - shotTransform.position;
        dir.y = 0;
        return Vector3.Angle(shotTransform.forward, dir) < fireAngle;
    }

    private void Fire()
    {
        Instantiate(shotPrefab, fireTransform.position, fireTransform.rotation);
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

}
