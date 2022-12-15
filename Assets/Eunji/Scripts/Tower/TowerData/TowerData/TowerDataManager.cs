using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TowerDataManager�� ���
// ���� �� Ÿ���� ������ �˷��ش�.
public class TowerDataManager : Singleton<TowerDataManager>
{
    public TowerData[] towerDatas;              // Ÿ�� ����
    public ProjectileData[] projectileDatas;    // �߻�ü ����

    TowerData towerData;

    /// <summary>
    /// ������ ������ �޴���(�б�����) ������Ƽ
    /// </summary>
    public TowerData TowerData => towerData;

    // Ÿ�� ���� -------------------------------------------------------------------------------------------------
    /// <summary>
    /// towerDatas Ȯ�ο� �ε���(Indexer)
    /// </summary>
    /// <param name="id">towerDatas �ε����� ����� ����</param>
    /// <returns>towerDatas id��° ������ ������</returns>
    public TowerData this[uint id] => towerDatas[id];

    /// <summary>
    /// towerDatas Ȯ�ο� �ε���
    /// </summary>
    /// <param name="code">Ȯ���� �������� TowerEnum �ڵ�</param>
    /// <returns>code�� ����Ű�� ������</returns>
    public TowerData this[TowerIDCode code] => towerDatas[(int)code];

    /// <summary>
    /// ��ü Ÿ�� ����
    /// </summary>
    public int Length => towerDatas.Length;

    // �߻�ü ���� -------------------------------------------------------------------------------------------------

}

