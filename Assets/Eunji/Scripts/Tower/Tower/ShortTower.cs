using System.Collections;
using UnityEditor;
using UnityEngine;

#if UNITY_EDIOR
using UnityEditor;  // UNITY_EDIOR��� ��ó���Ⱑ �����Ǿ����� ���� ��������� �־��
#endif

// ShortTower�� ���
// 1. ���� ���� ���� Ȯ���Ͽ� Ÿ������ ����
// 2. ����ü ����
// 3. ������ ����ü�� �ڽ����� ����

// 1. ����ü ���� ���� ����(0��° �ڽ��� Ÿ���� �ٶ󺻰� ������ ����ü ����(fireAngle�̸�))
// 2. ����ü ���� ��ġ ����
public class ShortTower : TowerBase
{
    Transform childPos;                    // �߻簢 Ȯ�� �� ��ġ
    Vector3 proCreatPos;                   // ����ü ���� ��ġ Vector3 
    Direction dir;                         // �߻簢���� Ȯ���� �� �ִ� ����

    protected override void Awake()
    {
        base.Awake();

        childPos = transform.GetChild(1);
        porPos = childPos.GetChild(0).transform.position;   // ����ü ���� ��ġ�� �� �Ҵ�.
        dir = childPos.GetComponent<Direction>();   
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        //if (other.CompareTag("Enemy"))              // ���� �ȿ� Enemy�� ������
        //{
        //    target = other.gameObject;              // target�� Enemy�� ����

        //    if (target != null && dir.IsInFireAngle())    // �߻簢�� ���� Ÿ���� ������
        //    {
        //        isFiring = true;
        //    }
        //}

        if (target != null && dir.IsInFireAngle())    // �߻簢�� ���� Ÿ���� ������
        {
            isFiring = true;
        }
    }


    //    protected override void OnDrawGizmos/*Selected*/() // �����
    //    {
    //#if UNITY_EDITOR


    //        Handles.color = Color.green;    // �ʷϻ����� ǥ��
    //        Handles.DrawWireDisc(transform.position, transform.up, sightRange, 3.0f);     //�þ� �ݰ游ŭ �� �׸��� //���� �ϳ��� ����

    //        if (target != null)             // Ÿ���� �ִٸ�
    //        {
    //            Handles.color = Color.red;  // ���Ŀ� �ۼ��� �ڵ���� ���������� ǥ��
    //        }

    //        Vector3 forward = transform.GetChild(1).forward;    // ������ ���� ��ġ�� �޾ƿ�
    //        forward.y = 0;
    //        forward = forward.normalized * sightRange;


    //        //Handles.DrawDottedLine(transform.position, transform.position + forward, 2.0f); // �߽ɼ� �׸���

    //        //Quaternion q1 = Quaternion.AngleAxis(-sightHalfAngle, transform.up);// up���͸� ������ �ݽð�������� sightHalfAngle��ŭ ȸ��
    //        //Quaternion q2 = Quaternion.AngleAxis(sightHalfAngle, transform.up); // up���͸� ������ �ð�������� sightHalfAngle��ŭ ȸ��

    //        //Handles.DrawLine(transform.position, transform.position + q1 * forward);    // �߽ɼ��� �ݽð�������� ȸ�����Ѽ� �׸���
    //        //Handles.DrawLine(transform.position, transform.position + q2 * forward);    // �߽ɼ��� �ð�������� ȸ�����Ѽ� �׸���
    //        //Handles.DrawWireArc(transform.position, transform.up, q1 * forward, sightHalfAngle * 2, sightRange, 5.0f);  // ȣ �׸���
    //#endif  
    //    }
}
