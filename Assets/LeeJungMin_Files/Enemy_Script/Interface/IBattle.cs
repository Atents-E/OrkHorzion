using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public interface IBattle
{
    float AttackPower { get; }  // 공격력 읽는 프로퍼티
    float DefencePower { get; }

    void Attack(IBattle target);    // 공격 함수

    // 인터페이스를 상속받을 클래스의 함수
    // 플레이어의 경우

    /*
    public void Attack(IBattle target)
    {
        if (target! = null)
        { 
            float damage = ATK; // 데미지 = 해당 클래스의 공격력
            if (Random.Range(0.0f, 1.0f) < ciriticalChance) // criticalChance : 치명타 확률, 기본적으로 플레이어는 기본 치명타확률 3%를 갖는다( float ciriticalChance = 0.03f; )
            {
                damage *= 1.5f;
            }
            target.TakeDamage(damage);  // 플레이어는 공격력만큼 데미지를 주지만 치명타가 뜨면 1.5배의 데미지를 준다.
        }
            
    }

    // 몬스터의 경우
    // 치명타 기능 없음
    public void Attack(IBattle target)
    {
        if (target! = null)
        {
            float damage = ATK; // 몬스터는 치명타확률을 갖지 않음
            target.TakeDamage(damage);  // 몬스터는 공격력만큼 데미지를 준다.
        }
    }
    */

    void TakeDamage(float damage);  // 피해 함수

    
    // 인터페이스를 상속받을 클래스의 함수
    // 플레이어의 경우
    /*
    public void TakeDamage(float damage)
    {
        float finalDamage = damage * (1.0f - DEF / (DEF + 100.0f));
        HP -= finalDamage
        if (HP < 0.0f)
        {
            Die();  
        }
    }

    // 몬스터의 경우
    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP < 0.0f)
        {
            Die();
        }
    }
    */

}
