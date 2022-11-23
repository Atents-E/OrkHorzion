using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTarget : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            Debug.Log("트리거");
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            Debug.Log("콜라이더");
            Destroy(collision.gameObject);
        }
    }
}
