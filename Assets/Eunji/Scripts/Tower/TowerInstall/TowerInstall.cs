using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TowerInstall : MonoBehaviour
{
    // 12.15

    //static int itemCount = 0;   // 생성된 아이템 총 갯수. 아이템 생성 아이디의 역할도 함.

    ///// <summary>
    ///// ItemIDCode로 아이템 생성
    ///// </summary>
    ///// <param name="code">생성할 아이템 코드</param>
    ///// <returns>생성 결과</returns>
    //public static GameObject TowerOjbectInstall(TowerIDCode code)
    //{
    //    GameObject tower = new GameObject();

    //    // TowerData// towerData = tower.AddComponent<TowerData>();           // Item 컴포넌트 추가하기
    //    TowerData towerData = TowerDataManager.Inst.TowerData[code];   // towerData 할당

    //    string[] itemName = towerData.name.Split("_");      // 00_Ruby => 00 Ruby로 분할
    //    tower.name = $"{itemName[1]}_{itemCount++}";        // 오브젝트 이름 설정하기
    //    tower.layer = LayerMask.NameToLayer("Tower");       // 레이어 설정

    //    SphereCollider sc = tower.AddComponent<SphereCollider>(); // 컬라이더 추가
    //    sc.isTrigger = true;
    //    sc.radius = 0.5f;
    //    sc.center = Vector3.up;

    //    return tower;
    //}




    ///// <summary>
    ///// 아이템 코드를 이용해 특정 위치에 아이템을 생성하는 함수
    ///// </summary>
    ///// <param name="code">생성할 아이템 코드</param>
    ///// <param name="position">생성할 위치</param>
    ///// <param name="randomNoise">위치에 랜덤성을 더할지 여부. true면 약간의 랜덤성을 더한다. 기본값은 false</param>
    ///// <returns>생성된 아이템</returns>
    //public static GameObject TowerOjbectInstall(TowerIDCode code, Vector3 position, bool randomNoise = false)
    //{
    //    GameObject tower = TowerOjbectInstall(code);    // 만들고
    //    if (randomNoise)                    // 위치에 랜덤성을 더하면
    //    {
    //        Vector2 noise = Random.insideUnitCircle * 0.5f; // 반지름 0.5인 원의 안쪽에서 랜덤한 위치 구함
    //        position.x += noise.x;          // 구한 랜덤함을 파라메터로 받은 기존 위치에 추가
    //        position.z += noise.y;
    //    }
    //    tower.transform.position = position;  // 위치지정
    //    return tower;
    //}


    ///// <summary>
    ///// 아이템 id로 아이템 생성
    ///// </summary>
    ///// <param name="id">생성할 아이템 ID</param>
    ///// <returns>생성한 아이템</returns>
    //public static GameObject TowerOjbectInstall(int id)
    //{
    //    if (id < 0)
    //        return null;

    //    return TowerOjbectInstall((TowerIDCode)id);
    //}

    ///// <summary>
    ///// 아이템 id를 이용해 특정 위치에 아이템을 생성하는 함수
    ///// </summary>
    ///// <param name="id">생성할 아이템 아이디</param>
    ///// <param name="position">생성할 위치</param>
    ///// <returns>생성된 아이템</returns>
    //public static GameObject TowerOjbectInstall(int id, Vector3 position, bool randomNoise = false)
    //{
    //    GameObject tower = TowerOjbectInstall(id);      // 만들고
    //    if (randomNoise)                    // 위치에 랜덤성을 더하면
    //    {
    //        Vector2 noise = Random.insideUnitCircle * 0.5f; // 반지름 0.5인 원의 안쪽에서 랜덤한 위치 구함
    //        position.x += noise.x;          // 구한 랜덤함을 파라메터로 받은 기존 위치에 추가
    //        position.z += noise.y;
    //    }
    //    tower.transform.position = position;  // 위치지정
    //    return tower;
    //}

}
