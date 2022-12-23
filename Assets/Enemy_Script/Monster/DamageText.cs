using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    public float moveSpeed = 1.0f;      // �����̴� �ӵ�
    public float alphaSpeed = 1.0f;     // ������� �ӵ�
    public float destroyTime = 2.0f;    // ������Ʈ ����
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
        // ī�޶� �ٶ󺸴� ������ ���ؼ� HP�ٰ� ���̰� �Ѵ�.
    }
}
