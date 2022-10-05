using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    //����===========================================================================
    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    public int goal = 10;

    /// <summary>
    /// Ÿ���� ���� �ӵ�
    /// </summary>
    public float attackSpeed = 2.0f;

    /// <summary>
    /// Ÿ���� ���� ����
    /// </summary>
    public float fireAngle = 10.0f;     
    
    /// <summary>
    /// �ѱ��� ó�� ����
    /// </summary>
    float currentAngle = 0.0f;
    
    /// <summary>
    /// Ÿ���� ����
    /// </summary>
    public float sightRadius = 5.0f;

    /// <summary>
    /// Ÿ���� ȸ���ӵ�
    /// </summary>
    public float turnSpeed = 5.0f;

    /// <summary>
    ///  �Ѿ��� Ʈ������
    /// </summary>
    Transform ShotTransform;

    /// <summary>
    /// Ÿ��x
    /// </summary>
    Transform targetTransform = null;        
    Vector3 initialForward;

    public GameObject bulletPrefab;     // �Ѿ� ������ //
    

    //������Ʈ
    // ������ٵ�
    // �ö��̴�
    // �ִϸ�����
    Rigidbody body;

    //���
    // ��ġ�� ������ ������ Ȯ��
    // ��ġ
    // ���� üũ
    // ���� ���� ���� ����


    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }




}
