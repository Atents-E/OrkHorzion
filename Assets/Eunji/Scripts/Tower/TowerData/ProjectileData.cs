using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 발사체 매니저(생성 될 발사체의 종류를 알려주는 클래스)
[CreateAssetMenu(fileName = "New Projectile Data ", menuName = "Scriptable Object/projectile Data", order = 1)]
public class ProjectileData : ScriptableObject
{
    [Header("발사체 기본 데이터")]
    public uint id = 1;                         // 발사체 ID
    public string projectileName = "발사체";     // 발사체 이름
    public GameObject modelPrefab;              // 발사체 외형을 표시할 프리팹(공격이 되면 날아갈 발사체)
    public string projectileDescription;        // 발사체의 상세 설명
}