using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ÿ�� �Ŵ���(���� �� Ÿ���� ������ �˷��ִ� Ŭ����)
[CreateAssetMenu(fileName = "New Tower Data", menuName = "Scriptable Object/Tower Data", order = 0)]
public class TowerData : ScriptableObject
{
    [Header("Ÿ�� �⺻ ������")]
    public uint id = 0;                 // Ÿ�� ID
    public string towerName = "Ÿ��";   // Ÿ�� �̸�
    public GameObject modelPrefab;      // Ÿ�� ������ ǥ���� ������(����� �Ǹ� ���� �� ��)
    public Sprite towerIcon;            // Ÿ�� �޴����� ���� ��������Ʈ
    public uint value;                  // Ÿ�� ����
    public string towerDescription;     // Ÿ���� �� ����
}