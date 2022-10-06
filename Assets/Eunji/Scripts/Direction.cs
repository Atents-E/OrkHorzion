using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    ////속도, 목적지, 

    //public float initialSpeed = 20.0f;
    //Rigidbody rigid;        //물리량 계산 시, Rigidbody로 사용하는게 편리

    //private void Awake()
    //{
    //    rigid = GetComponent<Rigidbody>();
    //}

    //private void Start()
    //{
    //    rigid.velocity = transform.forward * initialSpeed;
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        IDead PlayerDead = collision.gameObject.GetComponent<IDead>();
    //        if (PlayerDead != null)
    //        {
    //            PlayerDead.Die();
    //        }
    //    }
    //    Destroy(this.gameObject, 2.0f); // 2초 후에 없어져라
    //}
}
