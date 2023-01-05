using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_EDIOR
using UnityEditor;  // UNITY_EDIOR��� ��ó���Ⱑ �����Ǿ����� ���� ��������� �־��
#endif

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(CapsuleCollider))]
// TowerBase�� ���
// 1. ���� ���� ���� Ȯ���Ͽ� Ÿ������ ����
// 2. ����ü ����
// 3. ������ ����ü�� �ڽ����� ����

// 4. Ÿ���� Ŭ���ϸ� ���� Ȯ���ϴ� â�� ����,
// 5. ������ ��û�ϸ� Ÿ�� �ݾ��� ��ȯ, Ÿ���� �����ȴ�.
public class TowerBase : MonoBehaviour, IPointerClickHandler
{
    public int gold = 10;                   // Ÿ�� ����

    public float sightRange = 10.0f;        // ����
    public float sightRadius = 5.0f;        // ���� ������

    public float attactCreatSpeed = 0.5f;   // ����ü ���� �ӵ�
    public float fireAngle = 10.0f;         // Ÿ���� ���� ����

    protected float currentAngle = 0.0f;    // ������ ó�� ����
    protected bool isFiring = false;        // �߻� ������ Ȯ��
    
    //public float projectileSpeed = 0.3f;    // ����ü�� �ӵ�
    //public float projectileCreate = 0.1f;   // ����ü�� �����ð�
    
    protected IEnumerator fireCoroutine;    // �ڷ�ƾ�� ������ ������ ������ �־�� ��.

    public GameObject target;               // Ÿ���� null
    public GameObject projectile;           // ����ü ������

    protected Transform porjectilePos;      // ����ü ���� �� ��ġ
    protected Vector3 porPos;               // ����ü ���� �� Vecotr3 ��ġ

    protected virtual void Awake()   
    {
        fireCoroutine = PeriodFire();               // �߻�Ⱓ

        porjectilePos = transform.GetChild(0);
        porPos = porjectilePos.transform.position;

        porPos.y = 0.5f;
    }

    protected virtual void Start()    // ù��° ������Ʈ�� �Ͼ�� ���� ȣ��
    {
        //initialForward = transform.forward;         // ó�� ���� ���� ������Ʈ�� ��
        SphereCollider col = GetComponent<SphereCollider>();    // �� �ö��̴� �Ҵ�
        col.radius = sightRadius;                   // 

        // StartCoroutine(fireCoroutine);              // �ڷ�ƾ ����
    }

    /// <summary>
    ///  �������� â���� ���� ���������� ����Ǿ��� �� ȣ��Ǵ� �Լ�  //�ν����� û������ ���� ����Ǵ� ����(��� ��) �ٷιٷ� Ȯ�� ����
    /// </summary>
    void OnValidate()
    {
        SphereCollider col = GetComponent<SphereCollider>();
        if (col != null)
        {
            col.radius = sightRange;
        }
    }

    protected virtual void Update()
    {
        // Attack();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))              // ���� �ȿ� Enemy�� ������
        {
            target = other.gameObject;               // target = Enemy
            Attack();

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))              // ���� �ȿ� Enemy�� ������
        {
            target = null;                          // target ����
            isFiring = false;                       // �߻� Ȯ��(false : fire�ڷ�ƾ ����) 
            Attack();
        }
    }

    void Attack()
    {
        if (isFiring)
        {
            StartCoroutine(fireCoroutine);  // �ڷ�ƾ ����
        }
        else
        {
            StopCoroutine(fireCoroutine);   // �ڷ�ƾ �ߴ�
        }
    }


    protected virtual void Fire()         // �߻�
    {
        Instantiate(projectile, porPos, transform.rotation, transform);  // ����ü�� ����
    }

    IEnumerator PeriodFire()    // �߻� �Ⱓ
    {
        while (isFiring)            // true�� ���ȿ�
        {
            Fire();             // �߻�
            yield return new WaitForSeconds(attactCreatSpeed);   // ���� �ӵ���ŭ ��ٸ��� ���� �Ҵ�
        }
    }


    public void OnPointerClick(PointerEventData _)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        TowerDelete towerDelete = canvas.GetComponentInChildren<TowerDelete>();

        towerDelete.gameObject.SetActive(true);

        if (towerDelete.OK != false)     // ���� �³��� �Ǿ�����
        {
            DeleteTower();
        }
    }

    /// <summary>
    /// Ÿ���� �ݾ��� 80%�� ��ȯ�ϰ�, Ÿ���� �����Ѵ�.
    /// </summary>
    private void DeleteTower()
    {
        // Inventory inventory = GetComponent<Inventory>();
        // inventory.gold += value * 0.8f;

        Destroy(this.gameObject);
    }


    protected virtual void OnDrawGizmos/*Selected*/() // ��â���� Ÿ���� ���� ������ ǥ��
    {
#if UNITY_EDITOR

        Handles.color = Color.blue;    // �ʷϻ����� ǥ��

        if (target != null)             // Ÿ���� �ִٸ�
        {
            Handles.color = Color.red;  // ���Ŀ� �ۼ��� �ڵ���� ���������� ǥ��
        }

        Handles.DrawWireDisc(transform.position, transform.up, sightRadius, 3.0f);     //�þ� �ݰ游ŭ �� �׸��� //���� �ϳ��� ����
        
        //Vector3 forward = transform.forward;    // ������ ���� ��ġ�� �޾ƿ�
        //forward.y = 0;
        //forward = forward.normalized * sightRange;


        //Handles.DrawDottedLine(transform.position, transform.position + forward, 2.0f); // �߽ɼ� �׸���

        //Quaternion q1 = Quaternion.AngleAxis(-sightHalfAngle, transform.up);// up���͸� ������ �ݽð�������� sightHalfAngle��ŭ ȸ��
        //Quaternion q2 = Quaternion.AngleAxis(sightHalfAngle, transform.up); // up���͸� ������ �ð�������� sightHalfAngle��ŭ ȸ��

        //Handles.DrawLine(transform.position, transform.position + q1 * forward);    // �߽ɼ��� �ݽð�������� ȸ�����Ѽ� �׸���
        //Handles.DrawLine(transform.position, transform.position + q2 * forward);    // �߽ɼ��� �ð�������� ȸ�����Ѽ� �׸���
        //Handles.DrawWireArc(transform.position, transform.up, q1 * forward, sightHalfAngle * 2, sightRange, 5.0f);  // ȣ �׸���
#endif  
    }

}
