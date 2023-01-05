using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;
using static UnityEditor.Progress;
using Unity.VisualScripting;

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
        // this.gameObject.SetActive(false);
        currentParent = transform.parent;
        newParent = GameObject.FindWithTag("Canvas");
    }


    /// <summary>
    /// �巡�� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �����ִ� ������ ���� ó��
        if (gameObject.activeSelf)
        {
            //Vector2 screenPos = Mouse.current.position.ReadValue(); // ���콺�� ��ġ(��ũ����ǥ)�� ����
            //if (!IsInsideTowerMenu(screenPos))                      // ���콺�� ��ġ�� Ÿ���޴� �ۿ��� �����ϴ��� Ȯ��
            //{
            //}

            // �巡�װ� ���۵Ǹ� �θ��ڽ� ���� ����
            transform.parent = null;
            // ���ο� �θ���� �ξ��ֱ�
            transform.SetParent(newParent.transform);
     
            // bool isDragging = false;    
            // transform.SetParent(worldPositionStays(isDragging));
        }
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
        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, LayerMask.GetMask("Ground"))) // ���̿� ���� �浹 ���� Ȯ��
        {
            // ���̿� ���� �浹������
            //Debug.Log("���̿� ���� �浹��");
            
            GameObject ground = hit.collider.gameObject;        // �浹�� ������ ������Ʈ�� ã�Ƽ�
            Vector3 creatPos = ground.transform.position;       // ã�� ������Ʈ�� �ǹ��� �������� ��ġ�� ����(������Ʈ�� �߰��� ������)
            creatPos.x += 1;
            creatPos.y = 0;
            creatPos.z -= 1;

            // �� ������ tower ����
            GameObject obj = Instantiate(towerPrefab, creatPos, transform.rotation);

        }

        // �θ� ����, ���� �θ�� �ٽ� �ξ��ֱ�
        transform.parent = null;
        transform.SetParent(currentParent);
        // Ÿ�� UI = ó�� Ÿ�� ��ġ�� �̵�
        transform.position = currentParent.position;
    }

    ///<summary>
    ///TowerMenu�� ũ��
    ///</summary>
    ///<param name = "screenPos" ></ param >
    ///< returns ></ returns >
    bool IsInsideTowerMenu(Vector2 screenPos)
    {
        RectTransform rectTransform = (RectTransform)transform;

        Vector2 min = new(rectTransform.position.x - rectTransform.sizeDelta.x, rectTransform.position.y - rectTransform.position.y);
        Vector2 max = new(rectTransform.position.x + rectTransform.sizeDelta.x, rectTransform.position.y + rectTransform.position.y);

        return (min.x < screenPos.x && screenPos.x < max.x && min.y < screenPos.y && screenPos.y < max.y);
    }
}
