using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Rock : MonoBehaviour
{
    public Transform target;    // 포물선 종료 위치
    public float journeyTime = 10.0f;    // 시작위치에서 종료위치까지 도달하는 시간, 값이 높을수록 느림
    private float startTime;
    public float reduceHeight = 5f;   // 센터 값을 줄이기, 해당 값이 높을수록 포물선의 높이는 낮아진다.
    //float rotateSpeed = 180.0f;
    //float speed;
    //bool isthrow = true;

    private void Start()
    {
        startTime = Time.time;
        
    }
    private void Update()
    {
        Vector3 center = (transform.position + target.position) * 0.05f; //Center 값만큼 위로 올라간다.
        center -= new Vector3(0, 1f * reduceHeight, 0);                  //y값을 높이면 높이가 낮아진다.

        Vector3 riseRelCenter = transform.position - center;
        Vector3 setRelCenter = target.position - center;

        float fracComplete = (Time.time - startTime) / journeyTime;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;

    }
}
