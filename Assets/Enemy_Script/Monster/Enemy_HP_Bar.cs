using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HP_Bar : MonoBehaviour
{
    Transform fill;

    private void Awake()
    {
        fill = transform.Find("Fill");          // fill ã��
        //fill = transform.GetChild(1);

        IHealth target = GetComponentInParent<IHealth>();
        target.onHealthChange += Refresh;       // ��������Ʈ�� �Լ� �����ϱ�
    }

    private void Refresh(float ratio)
    {

        fill.localScale = new Vector3(ratio, 1, 1); // �Է¹��� ������� ������ ����
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
