using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

//3D 오브젝트
public class DragAndDrop : MonoBehaviour
{
    [SerializeField]
    InputAction mouseClick;

    [SerializeField]
    float mouseDragPhysicsSpeed = 10.0f;

    [SerializeField]
    float mouseDragSpeed = 0.1f;

    Camera mainCamera;

    Vector3 velocity = Vector3.zero;

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext _)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if( Physics.Raycast(ray, out hit))
        {
            if(hit.collider != null)
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody>(out var rigid);
        while ( mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(rigid != null)
            {
                Vector3 dir = ray.GetPoint(initialDistance) - clickedObject.transform.position; 
                rigid.velocity = dir * mouseDragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position,
                    ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);
                yield return null;
            }
        }
    }
}
