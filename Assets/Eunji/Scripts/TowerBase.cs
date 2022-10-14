using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerBase : MonoBehaviour
{
    public int goal = 10;                   // Ÿ�� ����
    public float turnSpeed = 5.0f;          // Ÿ���� ȸ���ӵ�
    public float sightRadius = 5.0f;        // Ÿ���� ����

    public GameObject shotPrefab;           //�Ѿ� ������
    public float attackSpeed = 2.0f;        // Ÿ���� ���� �ӵ�
    public float fireAngle = 10.0f;         // Ÿ���� ���� ����
    float currentAngle = 0.0f;              // �ѱ��� ó�� ����
    bool isFiring = false;                  // �߻� ������ Ȯ��

    IEnumerator fireCoroutine;          // �ڷ�ƾ�� ������ ������ ������ �־�� ��.
    Transform direction;                //  �Ѿ��� Ʈ������
    Transform shotTransform;            // �Ѿ��� Ʈ������(�Ѿ��� ��ġ�� ���� �ʿ�)

    Transform target = null;        
    Vector3 initialForward;

    private void Awake()
    {
        direction = transform.GetChild(0);     // ������ �ѱ��� �޾ƿ�
        shotTransform = direction.GetChild(3); // �Ѿ� �߻� ��ġ �޾ƿ�
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))            // ���� �ȿ� Monster�� ������
        {
            target = other.transform;               // target = Monster
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))            // ���� �ȿ� Monster�� ������
        {
            target = null;                          // target ����
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
        Instantiate(shotPrefab, shotTransform.position, shotTransform.rotation);
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

    //================================================================================
    public class Turret : MonoBehaviour
    {
        // �÷��̾ ���� �ݰ� �ȿ� ������ �ش�������� �ѱ��� ������.
        // sightRadius �ƹ��� ������ ���� �ʴ´�.
        // turnSpeed�� �ƹ��� ������ ���� �ʴ´�. ( �ѱ��� ��� ȸ���Ѵ�. )

        public float turnSpeed = 2.0f;    // ȸ�� �ӵ�
        public float sightRadius = 5.0f;    // �ݰ�

        public GameObject bulletPrefab;     // �Ѿ� ������
        public float fireInterval = 0.5f;   // �Ѿ� �߻� ����
        public float fireAngle = 10.0f;     // �߻� ����

        Transform fireTransform;            // �Ѿ��� Ʈ������(�Ѿ��� ��ġ�� ���� �ʿ�)
        IEnumerator fireCoroutine;          // �ڷ�ƾ�� ������ ������ ������ �־�� ��.
        WaitForSeconds waitFireInterval;    // �ڷ�ƾ�� �������� ���̴� ���2
        bool isFiring = false;              // �߻� ������ Ȯ��


        Transform target = null;      // �÷��̾��� ��ġ ����
        Transform barrelBody;        // �ѱ�

        float currentAngle = 0.0f;
        //float targetAngle = 0.0f;
        Vector3 initialForward;

        private void Awake()
        {
            barrelBody = transform.GetChild(2);     // ������ �ѱ��� �޾ƿ�
            fireTransform = barrelBody.GetChild(3); // �Ѿ� �߻� ��ġ �޾ƿ�

            fireCoroutine = PeriodFire();
        }

        private void Start()
        {
            initialForward = transform.forward;
            SphereCollider col = GetComponent<SphereCollider>();
            col.radius = sightRadius;

            waitFireInterval = new WaitForSeconds(fireInterval);
            //StartCoroutine(fireCoroutine);    //  �ڷ�ƾ�� �������� ���̴� ���1 //�ڷ�ƾ�� ���� ���� �״� �� ���� �ڷ�ƾ�� ������ �����ϰ� ����ؾ��Ѵ�.
            // StartCoroutine(PeriodFire());    // �� �ڵ�� PeriodFire()�� 1ȸ������ ����Ѵ�. �׷��� �������� �����ȴ�.
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


        // �ݰ�ȿ� ���°��� Ȯ�� Trigger
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                target = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                target = null;
            }
        }

        private void LookTarget()
        {
            if (target != null)
            {

                // ������ ����ϴ� ���(��ӵ��� ȸ��)
                Vector3 barrelToPlayerDir = target.position - barrelBody.position;  // �ѱ����� �÷��̾��� ��ġ�� ���� ���� ���� ���
                barrelToPlayerDir.y = 0;

                // �������� �� 0~180��. �������� �� 0~-180��        //�޼� ��ǥ�迡�� ���� �հ����� ���� ���� ��, �ٸ� �հ����� �ð� �������� �����.
                float betweenAngle = Vector3.SignedAngle(barrelBody.forward, barrelToPlayerDir, barrelBody.up);

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
                    resultDir = barrelToPlayerDir;
                }
                barrelBody.rotation = Quaternion.LookRotation(resultDir);

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
            // target;
            // transform.position;
            // fireAngle;

            //(������ ���ϱ�. ����Ƽ���� Vector3�� Angle�Լ� �̿����� �����ϵ��� ������)

            Vector3 dir = target.position - barrelBody.position;
            dir.y = 0;
            return Vector3.Angle(barrelBody.forward, dir) < fireAngle;
        }

        private void Fire()
        {
            // �Ѿ��� �߻��Ѵ�. ==> �Ѿ� �����ʿ�
            // �Ѿ� ������. �Ѿ��� �߻� �� ����, �Ѿ��� �߻�Ǵ� �ֱ�
            //Instantiate �����ϴ� �Լ� �̸�(���� �� ������, ���� �� ��ġ, �����Ǵ� ȸ��)
            Instantiate(bulletPrefab, fireTransform.position, fireTransform.rotation);

        }

        IEnumerator PeriodFire()
        {
            while (true)
            {
                Fire();
                yield return waitFireInterval;  // �Ʒ����� ������ ������ ���� �� �ִ�.
                                                //yield return new WaitForSeconds(fireInterval);    //�Ź� ���Ӱ� ����(�������� ���� �����.)
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

        //private void OnDrawGizmos() // sceneâ�� �߻�Ǵ� ������ ǥ��
        //{

        //}
    }

}
