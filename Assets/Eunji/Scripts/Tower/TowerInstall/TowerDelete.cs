using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Ÿ�� ������ Ŭ���� �Ǿ����� Ȯ��
public class TowerDelete : MonoBehaviour
{
    bool ok = false;    // ���� Ȯ�ο� ����

    public bool OK
    {
        get => ok;
        private set => OK = value;
    }

    Button okButton;    // ok��ư

    public  void Awake()
    {
        // gameObject.SetActive(false);
        okButton.onClick.AddListener(DeleteCheck);      // ������ ��ư�� ������ DeleteCheck�Լ� ����
    }

    void DeleteCheck()
    {
        OK = true;      // ���� �³�
    }

}
