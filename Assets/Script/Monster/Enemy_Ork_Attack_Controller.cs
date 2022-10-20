using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ork_Attack_Controller : MonoBehaviour
{
    public Animator anim;
    float temp = 1;

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0f)
        {
            if (temp >= 0)
            {
                temp -= Time.deltaTime;
            }
            anim.SetLayerWeight(1, temp);
        }
    }
}
