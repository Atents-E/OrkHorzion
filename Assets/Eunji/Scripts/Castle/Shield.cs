using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

// ShieldBase 기능
// 1. 생성 위치는 거점타워를 중점으로 한다
// 2. 생성 되자마자 회전
// 3. HP가 0과 같거나 적으면 해당 오브젝트를 삭제한다.
// 4. 공격 당하면 그 부위에 타격 이펙트를 보여준다.

public class ShieldBase : MonoBehaviour
{
    protected int goal = 100;               // 실드 금액
    protected float shildHP = 300.0f;       // 실드 HP
    public float rotateSpeed = 30.0f;       // 쉴드 회전 속도
    public float removeSpeed = 30.0f;       // 쉴드 삭제 스피드
    public float removeTime = 5.0f;       // 쉴드 삭제 시간

    public float remaining = 0.0f;

    Transform shield;       // 실스 위치
    Castle castle;          // 거점타워
    SphereCollider collider;

    public float ShildHP        
    {
        get => shildHP;
        set
        {
            ShildHP = value;
            if (shildHP == 0)               // 실드 체력 0이면
            {
                if(shildHP < 0)             // 0보다 작으면  
                {
                    remaining = shildHP - value;    // 남은 데미지를 저장
                    castle.HP -= remaining;         // 남은 데미지만큼 거점타워 체력을 줄이기
                }

                RemoveDelay();              // 실드 크기가 줄어들고
                Destroy(this.gameObject, removeTime);   // 5초 뒤 실드 삭제
            }
        }
    }

    private void Awake()
    {
        shield = GetComponent<Transform>();
        castle = FindObjectOfType<Castle>();
        collider = GetComponent<SphereCollider>();

        transform.position = castle.transform.position + new Vector3(0, 0.5f, 0);   // 타워 위치
    }

    private void Update()
    {
        shield .Rotate(rotateSpeed, rotateSpeed, rotateSpeed * Time.deltaTime);
    }

    protected virtual IEnumerator RemoveDelay()    // 실드가 삭제되는데 걸리는 시간
    {
        while (true)            
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)              // 실드와 충돌한게 적의 공격이라면 
    {
        if (collision.gameObject.CompareTag("MonsterAttack"))
        {
            MonsterBase monster = GetComponent<MonsterBase>();      // 
            ShildHP -= monster.attackPower;                         // 적의 공격력만큼 hp감소
        }
    }

}
