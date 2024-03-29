using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HP_Bar : MonoBehaviour
{
    Transform fill;

    private void Awake()
    {
        fill = transform.Find("Fill");          // fill 찾기
        //fill = transform.GetChild(1);

        // 부모가 가진 컴포넌트를 참조
        IHealth target = GetComponentInParent<IHealth>();
        target.onHealthChange += Refresh;       // 델리게이트에 함수 연결하기
    }

    private void Refresh(float ratio)
    {
        fill.localScale = new Vector3(ratio, 1, 1); // 입력받은 비율대로 스케일 조절
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
        // 카메라가 바라보는 방향을 향해서 HP바가 보이게 한다.
    }
}
