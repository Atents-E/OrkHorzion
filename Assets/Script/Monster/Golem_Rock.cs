using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Rock : MonoBehaviour
{
    public Transform target;    // ������ ���� ��ġ
    public float journeyTime = 10.0f;    // ������ġ���� ������ġ���� �����ϴ� �ð�, ���� �������� ����
    private float startTime;
    public float reduceHeight = 5f;   // ���� ���� ���̱�, �ش� ���� �������� �������� ���̴� ��������.
    //float rotateSpeed = 180.0f;
    //float speed;
    //bool isthrow = true;

    private void Start()
    {
        startTime = Time.time;
        
    }
    private void Update()
    {
        Vector3 center = (transform.position + target.position) * 0.05f; //Center ����ŭ ���� �ö󰣴�.
        center -= new Vector3(0, 1f * reduceHeight, 0);                  //y���� ���̸� ���̰� ��������.

        Vector3 riseRelCenter = transform.position - center;
        Vector3 setRelCenter = target.position - center;

        float fracComplete = (Time.time - startTime) / journeyTime;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;

    }
}
