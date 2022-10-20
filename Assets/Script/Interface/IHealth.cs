using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float HP { get; set; }  // HP를 확인하고 설정할 수 있다.
    float MaxHP { get; }    // 최대HP를 확인할 수 있다.

    Action<float> onHealthChange { get; set; }  // 체력변화가 생길 때 사용하는 델리게이트

    void Die(); // 캐릭터가 사망할 때 사용할 함수

}
