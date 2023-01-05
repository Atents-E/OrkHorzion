using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Direction ���
// �θ�(Ÿ��)�� Ÿ���� �޾ƿ���,
// 1. Ÿ���� ���� y�� �������� ȸ��
// 2. �߻簢 ���ο� Ÿ���� ���Դ��� �˷���
public class Direction : MonoBehaviour
{

    public float turnSpeed = 10.0f;         // ȸ���ӵ�
    public float fireAngle = 10.0f;         // Ÿ���� ���� ����
    protected float currentAngle = 0.0f;    // ������ ó�� ����

    protected Vector3 initialForward;                 // ó�� ��

    GameObject target;              // �߻�ü�� ���� Ÿ��(��)
    ShortTower parent;              // �θ�


    protected virtual void Start()
    {
        parent = GetComponentInParent<ShortTower>();
        target = parent.target;
    }

    protected void Update()
    {
        LookTarget();
    }


    protected virtual void LookTarget()   // Ÿ���� ������ ȸ��
    {
        if (target != null)     // Ÿ���� �ִٸ�,
        {
            // ������ ����ϴ� ���(��ӵ��� ȸ��)
            Vector3 shotToMonsterDir = target.transform.position - transform.position;  // ����(Ÿ��)���� ���� ��ġ�� ���� ���� ���� ���
            shotToMonsterDir.y = 0;

            // �������� �� 0~180��. �������� �� 0~-180��        //�޼� ��ǥ�迡�� ���� �հ����� ���� ���� ��, �ٸ� �հ����� �ð� �������� �����.
            float betweenAngle = Vector3.SignedAngle(transform.forward, shotToMonsterDir, transform.up);

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
            transform.rotation = Quaternion.LookRotation(resultDir);
        }
    }

    public virtual bool IsInFireAngle()        // �߻簢 �ȿ� �ִ��� Ȯ���ϴ� �뵵�� �Լ�
    {
        if(target != null)
        {
            Vector3 dir = target.transform.position - transform.position;         // Ÿ�� ��ġ - ���� ��ġ
            dir.y = 0;
            return Vector3.Angle(transform.forward, dir) < fireAngle;   // ������ ���ʰ� dir������ ������ �߻簢���� �۴�
            // Debug.Log("Ÿ���� �߻簢 �ȿ� ����");
        }
        else
        {
            return false;   // �߻簢 �ȿ� ����
        }
    }
}
