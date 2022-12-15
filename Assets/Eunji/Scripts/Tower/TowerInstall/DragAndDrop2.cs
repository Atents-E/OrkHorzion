using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragAndDrop2 : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// ���� �θ� ������Ʈ
    /// </summary>
    Transform parentPosition;

    /// <summary>
    /// (�巡�� ���� ��)���� �� �θ� ������Ʈ
    /// </summary>
    GameObject canvas;

    private void Awake()
    {
        parentPosition = transform.parent;
        canvas = GameObject.FindWithTag("Canvas");
    }

    /// <summary>
    /// �巡�� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�װ� ���۵Ǹ� �θ��ڽ� ���� ����
        transform.parent = null;
        // ���ο� �θ���� �ξ��ֱ�
        transform.SetParent(canvas.transform);
    }

    /// <summary>
    /// �巡�� ��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        // Ÿ�� UI = ���콺 ��ġ�� �޾Ƽ� �̵�
        transform.position = eventData.position;
    }

    /// <summary>
    /// �巡�� ��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        // �θ� ����, ���� �θ�� �ٽ� �ξ��ֱ�
        transform.parent = null;
        transform.SetParent(parentPosition);
        // Ÿ�� UI = ó�� Ÿ�� ��ġ�� �̵�
        transform.position = parentPosition.position;


        // towerDataManager.cs���� ���� �� Ÿ���� ������ �޾ƿ���
        // tower ����
        // Instantiate(
    }

    static int itemCount = 0;   // ������ ������ �� ����. ������ ���� ���̵��� ���ҵ� ��.

    /// <summary>
    /// ItemIDCode�� ������ ����
    /// </summary>
    /// <param name="code">������ ������ �ڵ�</param>
    /// <returns>���� ���</returns>
    public static GameObject TowerOjbectInstall(TowerIDCode code)
    {
        GameObject tower = new GameObject();

        // TowerData// towerData = tower.AddComponent<TowerData>();           // Item ������Ʈ �߰��ϱ�
        // TowerData towerData = TowerDataManager.Inst.TowerData[code];   // towerData �Ҵ�

        //string[] itemName = towerData.name.Split("_");      // 00_Ruby => 00 Ruby�� ����
        // tower.name = $"{itemName[1]}_{itemCount++}";        // ������Ʈ �̸� �����ϱ�
        tower.layer = LayerMask.NameToLayer("Tower");       // ���̾� ����

        SphereCollider sc = tower.AddComponent<SphereCollider>(); // �ö��̴� �߰�
        sc.isTrigger = true;
        sc.radius = 0.5f;
        sc.center = Vector3.up;

        return tower;
    }
}
