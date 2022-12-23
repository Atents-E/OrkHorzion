using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;
using static UnityEditor.Progress;

public class DragAndDrop2 : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    static int itemCount = 0;   // ������ ������ �� ����. ������ ���� ���̵��� ���ҵ� ��.
    
    /// <summary>
    /// ���� �θ� ������Ʈ
    /// </summary>
    Transform currentParent;

    /// <summary>
    /// (�巡�� ���� ��)���� �� �θ� ������Ʈ
    /// </summary>
    GameObject newParent;


    /// <summary>
    /// ���� �� Ÿ�� ������
    /// </summary>
    public GameObject towerPrefab;

    private void Awake()
    {
        currentParent = transform.parent;
        newParent = GameObject.FindWithTag("Canvas");
    }

    /// <summary>
    /// �巡�� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        bool isDragging = false;    
        // �巡�װ� ���۵Ǹ� �θ��ڽ� ���� ����
        transform.parent = null;
        // ���ο� �θ���� �ξ��ֱ�
        transform.SetParent(newParent.transform);
        // transform.SetParent(worldPositionStays(isDragging));
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
        Ray ray =  Camera.main.ScreenPointToRay(eventData.position);   // ��ũ�� ��ǥ�� ���� ����
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, LayerMask.GetMask("Ground"))) // ���̿� ���� �浹 ���� Ȯ��
        {
            // ���̿� ���� �浹������
            Debug.Log("���̿� ���� �浹��");

            Vector3 dropPos = hit.point;    // ��ŷ�� ���� ���� ����
            
            // �� ������ tower ����
            Instantiate(towerPrefab, dropPos, transform.rotation);
            
        }

        // �θ� ����, ���� �θ�� �ٽ� �ξ��ֱ�
        transform.parent = null;
        transform.SetParent(currentParent);
        // Ÿ�� UI = ó�� Ÿ�� ��ġ�� �̵�
        transform.position = currentParent.position;
    }

}
