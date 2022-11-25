using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerInstallUI : MonoBehaviour, IDragHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
{
    // ����� �ǵ��(11.11) 
    //1. InputSystem���� ��ŷ

    public GameObject towerPrefab;


    // ��������Ʈ --------------------------------------------------------------------------------------------------
    public Action OnTowerDragStart; // Ÿ�� �巡�׸� �������� ��


    // �Լ� --------------------------------------------------------------------------------------------------------
    private void Awake()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        // UnityEngine.EventSystems �������̽��� ����ϱ� ���� �Լ�
        OnTowerDragStart?.Invoke();
    }

    //2. ��ũ�� �並 Ư�� ��ġ ������ �������� ��ǥ�� ���
    //  2.1. ��ũ�� ���� Ư�� ��ġ ���ϱ�
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�׸� ���� ���� �� ����
        // �巡�װ� ���۵Ǹ� �������� ���İ� ����
    }

    // ��� ���� ��(�巡�װ� ������ ��) ����
    public void OnEndDrag(PointerEventData eventData)
    {

    }

    bool IsInsideTowerButton(Vector2 screenPos)
    {
        RectTransform rectTransform = (RectTransform)transform;

        Vector2 min = new(rectTransform.position.x - rectTransform.sizeDelta.x, rectTransform.position.y - rectTransform.position.y);
        Vector2 max = new(rectTransform.position.x + rectTransform.sizeDelta.x, rectTransform.position.y + rectTransform.position.y);

        return (min.x < screenPos.x && screenPos.x < max.x && min.y < screenPos.y && screenPos.y < max.y);
    }


    //3. Ÿ�� �������� ������ Ÿ����ư�� ���İ��� 0���� �ϰ�, Ÿ�� �������� �����̴� ������ 1�� ������
    //4. Ÿ�� �������� �����ϰ� �ִٰ� �������� ���İ��� 1�� ������
    //5. 3~4�� ���ÿ� �Ͼ�� �ȴٸ� Ÿ����ư�� ������ Ÿ�� �������� ���������, �̵��Ǵ� �� ó�� ���� �� �̴�.

    // ���콺 Ŀ���� �ش� ���� �´ٸ� ����
    public void OnPointerClick(PointerEventData eventData)
    {

    }
}