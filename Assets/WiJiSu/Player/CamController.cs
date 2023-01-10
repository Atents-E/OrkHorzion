using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    private Vector3 offset;
    Transform target = null;

    void Start()
    {
        target = GameManager.Inst.Character.transform;
        offset = transform.position - target.transform.position;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
