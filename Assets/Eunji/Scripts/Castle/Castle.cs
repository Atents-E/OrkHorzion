using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    const float maxHP = 500;
    float hp = maxHP;

    //Action isGameOver;

    public float HP
    {
        get => hp;              
        set                     
        {
            hp -= value;                          
        }
    }


}
