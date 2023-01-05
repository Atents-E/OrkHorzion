using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Rock : MonoBehaviour
{
    public float speed = 5.0f;

    private void Start()
    {
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * 180.0f * Time.deltaTime);
        transform.Rotate(Vector3.forward * 120.0f * Time.deltaTime);
        transform.Rotate(Vector3.right * 60.0f * Time.deltaTime);
    }
}
