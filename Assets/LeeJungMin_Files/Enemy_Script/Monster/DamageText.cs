using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    public float moveSpeed = 1.0f;      // 움직이는 속도
    public float alphaSpeed = 1.0f;     // 사라지는 속도
    public float destroyTime = 2.0f;    // 오브젝트 삭제
    TextMeshPro text;
    Color alpha;

    public int Damage;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = Damage.ToString();
        alpha = text.color;
        Invoke("DestoryObject", destroyTime);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    private void DestoryObject()
    {
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
        // 카메라가 바라보는 방향을 향해서 HP바가 보이게 한다.
    }
}

