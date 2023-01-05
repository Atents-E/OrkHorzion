using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

// �������̽� ���
public class TowerCreat : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    // Ư�� Ÿ�� ���� Ŀ��(�հ�����) �ö�Դ��� Ȯ��
    // �ö������ Ư�� Ÿ���� ���İ� 0
    // �⺻������ ��� Ÿ�� �������� ���İ� 1

    // ���� ------------------------------------------------------------------------------------------------------------
    // Ÿ�� ������ ����
    public GameObject towerPrefab;    // ���� �� Ÿ���� ������


    // ��������Ʈ --------------------------------------------------------------------------------------------------
    // public Action OnTowerDragStart; // Ÿ�� �巡�׸� �������� ��

    private void Awake()
    {
        towerPrefab = GetComponent<GameObject>();
        // OnTowerDragStart += IsInsideTowerIcon();
    }

    private void Start()
    {
        //towerPrefab.onClick.AddListener(MakeTower);
    }

    // ������ Ÿ�� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        // towerPrefab = Instantiate(towerPrefab, transform);
        Debug.Log("Ÿ�� ����");
    }


    public void OnDrag(PointerEventData eventData)
    {
        // �巡�� �������̽��� ����ϱ� ���� �Լ�
    }


    // �巡�װ� ���۵Ǹ� Ÿ�� �������� Ȱ��ȭ �ǵ��� �ϱ�
    //2. ��ũ�� �並 Ư�� ��ġ ������ �������� ��ǥ�� ���
    public void OnBeginDrag(PointerEventData eventData)
    {
        towerPrefab.SetActive(true);
    }

    // ��� ���� �� ����. Ÿ�� ��ġ�� ���콺�� ��� �� ��ġ
    public void OnEndDrag(PointerEventData eventData)
    {
        // Ÿ�� ���� ��ġ�� ��� ��ġ
        towerPrefab.transform.position = eventData.position;
    }


    //  2.1. ��ũ�� ���� Ư�� ��ġ ���ϱ�
    bool IsInsideTowerIcon(Vector2 screenPos)
    {
        RectTransform rectTransform = (RectTransform)transform;

        Vector2 min = new(rectTransform.position.x - rectTransform.sizeDelta.x, rectTransform.position.y - rectTransform.position.y);
        Vector2 max = new(rectTransform.position.x + rectTransform.sizeDelta.x, rectTransform.position.y + rectTransform.position.y);

        return (min.x < screenPos.x && screenPos.x < max.x && min.y < screenPos.y && screenPos.y < max.y);

       // Debug.Log($"{screenPos}");
    }


    //3. Ÿ�� �������� ������ Ÿ����ư�� ���İ��� 0���� �ϰ�, Ÿ�� �������� �����̴� ������ 1�� ������
    //4. Ÿ�� �������� �����ϰ� �ִٰ� �������� ���İ��� 1�� ������
    //5. 3~4�� ���ÿ� �Ͼ�� �ȴٸ� Ÿ����ư�� ������ Ÿ�� �������� ���������, �̵��Ǵ� �� ó�� ���� �� �̴�.


}
