using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 타워 매니저(생성 될 타워의 종류를 알려주는 클래스)
[CreateAssetMenu(fileName = "New Tower Data", menuName = "Scriptable Object/Tower Data", order = 0)]
public class TowerData : ScriptableObject
{
    [Header("타워 기본 데이터")]
    public uint id = 0;                 // 타워 ID
    public string towerName = "타워";   // 타워 이름
    public GameObject modelPrefab;      // 타워 외형을 표시할 프리팹(드랍이 되면 생성 될 모델)
    public Sprite towerIcon;            // 타워 메뉴에서 보일 스프라이트
    public uint value;                  // 타워 가격
    public string towerDescription;     // 타워의 상세 설명
}