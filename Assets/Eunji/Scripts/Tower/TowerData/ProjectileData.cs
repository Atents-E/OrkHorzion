using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �߻�ü �Ŵ���(���� �� �߻�ü�� ������ �˷��ִ� Ŭ����)
[CreateAssetMenu(fileName = "New Projectile Data ", menuName = "Scriptable Object/projectile Data", order = 1)]
public class ProjectileData : ScriptableObject
{
    [Header("�߻�ü �⺻ ������")]
    public uint id = 1;                         // �߻�ü ID
    public string projectileName = "�߻�ü";     // �߻�ü �̸�
    public GameObject modelPrefab;              // �߻�ü ������ ǥ���� ������(������ �Ǹ� ���ư� �߻�ü)
    public string projectileDescription;        // �߻�ü�� �� ����
}