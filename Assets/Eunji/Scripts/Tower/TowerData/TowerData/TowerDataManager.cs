using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TowerDataManager의 기능
// 생성 할 타워의 종류를 알려준다.
public class TowerDataManager : Singleton<TowerDataManager>
{
    public TowerData[] towerDatas;              // 타워 종류
    public ProjectileData[] projectileDatas;    // 발사체 종류

    TowerData towerData;

    /// <summary>
    /// 아이템 데이터 메니저(읽기전용) 프로퍼티
    /// </summary>
    public TowerData TowerData => towerData;

    // 타워 관련 -------------------------------------------------------------------------------------------------
    /// <summary>
    /// towerDatas 확인용 인덱서(Indexer)
    /// </summary>
    /// <param name="id">towerDatas 인덱스로 사용할 변수</param>
    /// <returns>towerDatas id번째 아이템 데이터</returns>
    public TowerData this[uint id] => towerDatas[id];

    /// <summary>
    /// towerDatas 확인용 인덱서
    /// </summary>
    /// <param name="code">확인할 아이템의 TowerEnum 코드</param>
    /// <returns>code가 가르키는 아이템</returns>
    public TowerData this[TowerIDCode code] => towerDatas[(int)code];

    /// <summary>
    /// 전체 타워 갯수
    /// </summary>
    public int Length => towerDatas.Length;

    // 발사체 관련 -------------------------------------------------------------------------------------------------

}

