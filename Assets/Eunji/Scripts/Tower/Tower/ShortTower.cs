using System.Collections;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

#if UNITY_EDIOR
using UnityEditor;  // UNITY_EDIOR��� ��ó���Ⱑ �����Ǿ����� ���� ��������� �־��
#endif

public class ShortTower : TowerBase
{
    Transform direction;

    protected override void Awake()
    {
        direction = transform.GetChild(1);          // ������ ���� ��ġ�� �޾ƿ�
        directionTransform = direction.GetChild(0); // ����ü �߻� ��ġ �޾ƿ�

        base.Awake();
    }

    protected override void LookTarget()   // Ÿ���� ����
    {
        if (target != null)     // Ÿ���� �ִٸ�,
        {
            // ������ ����ϴ� ���(��ӵ��� ȸ��)
            Vector3 shotToMonsterDir = target.position
                - direction.position;  // ����(Ÿ��)���� ���� ��ġ�� ���� ���� ���� ���
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

    protected override bool IsInFireAngle()        // �߻簢 �ȿ� �ִ��� Ȯ���ϴ� �뵵�� �Լ�
    {
        Vector3 dir = target.position - direction.position;         // Ÿ�� ��ġ - ���� ��ġ
        return Vector3.Angle(direction.forward, dir) < fireAngle;   // ������ ���ʰ� dir������ ������ �߻簢���� �۴�

        //Debug.Log("Ÿ���� �߻簢 �ȿ� ����");
    }



    protected override void OnDrawGizmos/*Selected*/() // �����
    {
#if UNITY_EDITOR


        Handles.color = Color.green;    // �ʷϻ����� ǥ��
        Handles.DrawWireDisc(transform.position, transform.up, sightRange, 3.0f);     //�þ� �ݰ游ŭ �� �׸��� //���� �ϳ��� ����

        if (target != null)             // Ÿ���� �ִٸ�
        {
            Handles.color = Color.red;  // ���Ŀ� �ۼ��� �ڵ���� ���������� ǥ��
        }

        Vector3 forward = transform.GetChild(1).forward;    // ������ ���� ��ġ�� �޾ƿ�
        forward.y = 0;
        forward = forward.normalized * sightRange;


        Handles.DrawDottedLine(transform.position, transform.position + forward, 2.0f); // �߽ɼ� �׸���

        Quaternion q1 = Quaternion.AngleAxis(-sightHalfAngle, transform.up);// up���͸� ������ �ݽð�������� sightHalfAngle��ŭ ȸ��
        Quaternion q2 = Quaternion.AngleAxis(sightHalfAngle, transform.up); // up���͸� ������ �ð�������� sightHalfAngle��ŭ ȸ��

        Handles.DrawLine(transform.position, transform.position + q1 * forward);    // �߽ɼ��� �ݽð�������� ȸ�����Ѽ� �׸���
        Handles.DrawLine(transform.position, transform.position + q2 * forward);    // �߽ɼ��� �ð�������� ȸ�����Ѽ� �׸���
        Handles.DrawWireArc(transform.position, transform.up, q1 * forward, sightHalfAngle * 2, sightRange, 5.0f);  // ȣ �׸���
#endif  
    }

    //public GameObject projectile;

    //protected void Awake()    // �� ��ũ��Ʈ�� �����Ϸ� �� �Ŀ� ȣ��
    //{
    //    goal = 10;
    //    turnSpeed = 10;         
    //    sightRadius = 5;     
    //    projectile = base.projectile;       
    //    attackSpeed = base.attackSpeed;     
    //    fireAngle = base.fireAngle;         
    //    currentAngle = base.currentAngle;
    //    isFiring = base.isFiring;
    //    sightRange = base.sightRange;
    //    sightHalfAngle = base.sightHalfAngle;
    //    fireCoroutine = base.fireCoroutine;
    //    direction = base.direction;
    //    directionTransform = base.directionTransform;
    //    target = base.target;
    //    initialForward = base.initialForward;

    //    direction = transform.GetChild(1);          // ������ ���� ��ġ�� �޾ƿ�
    //    directionTransform = direction.GetChild(0); // ����ü �߻� ��ġ �޾ƿ�

    //    fireCoroutine = PeriodFire();
    //}

    //protected override void Start()    // ù��° ������Ʈ�� �Ͼ�� ���� ȣ��
    //{
    //    initialForward = transform.forward;         // ó�� ���� ���� ������Ʈ�� ��
    //    SphereCollider col = GetComponent<SphereCollider>();    // �� �ö��̴� �Ҵ�
    //    col.radius = sightRadius;                   // 

    //    StartCoroutine(fireCoroutine);              // �ڷ�ƾ ����
    //}

    ///// <summary>
    /////  �������� â���� ���� ���������� ����Ǿ��� �� ȣ��Ǵ� �Լ�  //�ν����� û������ ���� ����Ǵ� ����(��� ��) �ٷιٷ� Ȯ�� ����
    ///// </summary>
    //protected override void OnValidate()
    //{
    //    SphereCollider col = GetComponent<SphereCollider>();
    //    if (col != null)
    //    {
    //        col.radius = sightRadius;
    //    }
    //}

    //protected override void Update()
    //{
    //    LookTarget();       // Ÿ���� ����
    //}

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))              // ���� �ȿ� Enemy�� ������
    //    {
    //        target = other.transform;               // target = Enemy
    //    }
    //}

    //protected override void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))              // ���� �ȿ� Enemy�� ������
    //    {
    //        target = null;                          // target ����
    //    }
    //}

    //protected override void LookTarget()   // Ÿ���� ����
    //{
    //    if (target != null)     // Ÿ���� �ִٸ�,
    //    {
    //        // ������ ����ϴ� ���(��ӵ��� ȸ��)
    //        Vector3 shotToMonsterDir = target.position
    //            - direction.position;  // ����(Ÿ��)���� ���� ��ġ�� ���� ���� ���� ���
    //        shotToMonsterDir.y = 0;

    //        // �������� �� 0~180��. �������� �� 0~-180��        //�޼� ��ǥ�迡�� ���� �հ����� ���� ���� ��, �ٸ� �հ����� �ð� �������� �����.
    //        float betweenAngle = Vector3.SignedAngle(direction.forward, shotToMonsterDir, direction.up);

    //        Vector3 resultDir;
    //        if (Mathf.Abs(betweenAngle) > 1.0f)    // ���̰��� ���� ���� �������� üũ
    //        {
    //            // ���̰��� ����� ������ ���
    //            float rotateDirection = 1.0f;   //�ϴ� +����(������, �ð����)���� ����
    //            if (betweenAngle < 0)
    //            {
    //                rotateDirection = -1.0f;    // betweenAngle�� -�� rotateDirection�� -1��
    //            }

    //            // �ʴ� turnSpeed��ŭ ȸ���ϴµ� rotateDirection�� �ð�������� ȸ������ �ݽð� �������� ȸ������ ����
    //            currentAngle += (rotateDirection * turnSpeed * Time.deltaTime);

    //            resultDir = Quaternion.Euler(0, currentAngle, 0) * initialForward;
    //        }
    //        else
    //        {
    //            //���̰��� ���� 0�� ���
    //            resultDir = shotToMonsterDir;
    //        }
    //        direction.rotation = Quaternion.LookRotation(resultDir);



    //        // �÷��̾ �߻簢 �ȿ� ������ ���� ����. ������ ����
    //        if (!isFiring && IsInFireAngle())
    //        {
    //            FireStart();
    //        }
    //        if (isFiring && !IsInFireAngle())
    //        {
    //            FireStop();
    //        }
    //    }
    //}

    //protected override bool IsInFireAngle()        // �߻簢 �ȿ� �ִ��� Ȯ���ϴ� �뵵�� �Լ�
    //{
    //    Vector3 dir = target.position - direction.position;         // Ÿ�� ��ġ - ���� ��ġ
    //    return Vector3.Angle(direction.forward, dir) < fireAngle;   // ������ ���ʰ� dir������ ������ �߻簢���� �۴�

    //    Debug.Log("Ÿ���� �߻簢 �ȿ� ����");
    //}

    //protected override void Fire()         // �߻�
    //{
    //    Instantiate(projectile, directionTransform.position, directionTransform.rotation);  // ����ü�� ����
    //}


    //protected override IEnumerator PeriodFire()    // �߻� �Ⱓ
    //{
    //    while (true)            // true�� ���ȿ�
    //    {
    //        Fire();             // �߻�
    //        yield return new WaitForSeconds(attackSpeed);   // ���� �ӵ���ŭ ��ٸ��� ���� �Ҵ�
    //    }
    //}

    //protected override void FireStart()
    //{
    //    isFiring = true;                // �߻� ���̸�
    //    StartCoroutine(fireCoroutine);  // �ڷ�ƾ ����
    //}

    //protected override void FireStop()
    //{
    //    StopCoroutine(fireCoroutine);   // �ڷ�ƾ �ߴ�
    //    isFiring = false;               // �߻� ������ ������
    //}
}
