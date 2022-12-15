using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop1 : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private float dampingSpeed = 0.05f;

    private RectTransform newTowerICon;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        newTowerICon = transform as RectTransform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(newTowerICon, eventData.position,
            eventData.pressEventCamera, out var globalMousePostion))
        {
            newTowerICon.position = Vector3.SmoothDamp(newTowerICon.position,
                globalMousePostion, ref velocity, dampingSpeed);
        }
    }
}
