using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TowerInstall : MonoBehaviour
{
    // 12.15

    //static int itemCount = 0;   // ������ ������ �� ����. ������ ���� ���̵��� ���ҵ� ��.

    ///// <summary>
    ///// ItemIDCode�� ������ ����
    ///// </summary>
    ///// <param name="code">������ ������ �ڵ�</param>
    ///// <returns>���� ���</returns>
    //public static GameObject TowerOjbectInstall(TowerIDCode code)
    //{
    //    GameObject tower = new GameObject();

    //    // TowerData// towerData = tower.AddComponent<TowerData>();           // Item ������Ʈ �߰��ϱ�
    //    TowerData towerData = TowerDataManager.Inst.TowerData[code];   // towerData �Ҵ�

    //    string[] itemName = towerData.name.Split("_");      // 00_Ruby => 00 Ruby�� ����
    //    tower.name = $"{itemName[1]}_{itemCount++}";        // ������Ʈ �̸� �����ϱ�
    //    tower.layer = LayerMask.NameToLayer("Tower");       // ���̾� ����

    //    SphereCollider sc = tower.AddComponent<SphereCollider>(); // �ö��̴� �߰�
    //    sc.isTrigger = true;
    //    sc.radius = 0.5f;
    //    sc.center = Vector3.up;

    //    return tower;
    //}




    ///// <summary>
    ///// ������ �ڵ带 �̿��� Ư�� ��ġ�� �������� �����ϴ� �Լ�
    ///// </summary>
    ///// <param name="code">������ ������ �ڵ�</param>
    ///// <param name="position">������ ��ġ</param>
    ///// <param name="randomNoise">��ġ�� �������� ������ ����. true�� �ణ�� �������� ���Ѵ�. �⺻���� false</param>
    ///// <returns>������ ������</returns>
    //public static GameObject TowerOjbectInstall(TowerIDCode code, Vector3 position, bool randomNoise = false)
    //{
    //    GameObject tower = TowerOjbectInstall(code);    // �����
    //    if (randomNoise)                    // ��ġ�� �������� ���ϸ�
    //    {
    //        Vector2 noise = Random.insideUnitCircle * 0.5f; // ������ 0.5�� ���� ���ʿ��� ������ ��ġ ����
    //        position.x += noise.x;          // ���� �������� �Ķ���ͷ� ���� ���� ��ġ�� �߰�
    //        position.z += noise.y;
    //    }
    //    tower.transform.position = position;  // ��ġ����
    //    return tower;
    //}


    ///// <summary>
    ///// ������ id�� ������ ����
    ///// </summary>
    ///// <param name="id">������ ������ ID</param>
    ///// <returns>������ ������</returns>
    //public static GameObject TowerOjbectInstall(int id)
    //{
    //    if (id < 0)
    //        return null;

    //    return TowerOjbectInstall((TowerIDCode)id);
    //}

    ///// <summary>
    ///// ������ id�� �̿��� Ư�� ��ġ�� �������� �����ϴ� �Լ�
    ///// </summary>
    ///// <param name="id">������ ������ ���̵�</param>
    ///// <param name="position">������ ��ġ</param>
    ///// <returns>������ ������</returns>
    //public static GameObject TowerOjbectInstall(int id, Vector3 position, bool randomNoise = false)
    //{
    //    GameObject tower = TowerOjbectInstall(id);      // �����
    //    if (randomNoise)                    // ��ġ�� �������� ���ϸ�
    //    {
    //        Vector2 noise = Random.insideUnitCircle * 0.5f; // ������ 0.5�� ���� ���ʿ��� ������ ��ġ ����
    //        position.x += noise.x;          // ���� �������� �Ķ���ͷ� ���� ���� ��ġ�� �߰�
    //        position.z += noise.y;
    //    }
    //    tower.transform.position = position;  // ��ġ����
    //    return tower;
    //}

}
