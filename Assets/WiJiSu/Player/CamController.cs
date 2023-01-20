using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    private Vector3 offset;
    Transform target = null;

    void Start()
    {
        target = GameManager.Inst.Character.transform; // 캐릭터 위치를 타겟으로 설정
        offset = transform.position - target.transform.position; 
    }

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
