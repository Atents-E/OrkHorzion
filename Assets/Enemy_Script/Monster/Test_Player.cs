using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player : MonoBehaviour, IBattle
{
    public float AttackPower => 10;

    public float DefencePower => 3;

    public void Attack(IBattle target)
    {
        
    }

    public void TakeDamage(float damage)
    {
        
    }
}
