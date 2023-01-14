using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

// Shield 기능
// 1. 실드는 체력만큼 공격을 받는다.
// 2. 실드의 남은 체력보다 큰 공격을 받으면, 남은 데미지를 저장하여 타워에게 공격을 돌려준다.
// 3. HP가 0과 같거나 같으면, 삭제 대기 시간만큼 크기가 작아지고, 삭제된다.

// 4. 공격 당한거 어떻게 보여주나.. 실드 위의 체력 UI를 만들까..

// 1. 생성 위치는 거점타워를 중점으로 한다  -> 생성만 해주는 클래스는 따로 필요
public class Shield : MonoBehaviour
{
    /// <summary>
    /// 실드 가격
    /// </summary>
    public int price = 100;

    public float maxHP = 50.0f;

    /// <summary>
    /// 실드가 생성되자마자 가지는 총 HP
    /// </summary>
    public float shildHP = 50.0f;

    /// <summary>
    /// 실드 크기
    /// </summary>
    public float size = 1.0f;

    /// <summary>
    /// 실드의 남은 체력보다 더 큰 공격이 들어왔을 때 남은 데미지를 저장
    /// </summary>
    float remainingAttack = 0.0f;

    /// <summary>
    /// 거점 타워
    /// </summary>
    Castle castle;

    /// <summary>
    /// 실드로 보이는 부분
    /// </summary>
    GameObject glow;

    /// <summary>
    /// 삭제 대기 시간
    /// </summary>
    WaitForSeconds delayTime = new WaitForSeconds(1);

    public float ShildHP        
    {
        get => shildHP;
        set
        {
            if(shildHP != value)
            {
                shildHP = value;

                if (shildHP <= 0)               // 실드 체력 0이면
                {
                    if(shildHP < 0)             // 0보다 작으면  
                    {
                        remainingAttack = shildHP - value;    // 남은 데미지를 저장
                        castle.HP -= remainingAttack;         // 남은 데미지만큼 거점타워 체력을 줄이기
                    }

                    // 컬라이더 기능 끄기
                    Destroy(glow.GetComponent<Collider>());
                    RemoveDelay();                              // 실드가 사라짐
                }
            }
        }
    }

    private void Awake()
    {
        gameObject.SetActive(false);                             // 해당 오브젝트 끄기
    }

    private void Start()
    {
        castle = GameManager.Inst.Castle;
        glow = transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// 실드의 체력이 0이 되면 일어나는 일
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator RemoveDelay()
    {
        size += Time.deltaTime;                                 // 사이즈는 시간만큼 줄어들고,
        transform.localScale = new Vector3(size, size, size);
        yield return delayTime;                                // 대기 시간만큼 줄어드는 모습이 보임

        gameObject.SetActive(false);                             // 해당 오브젝트 끄기
    }

}
