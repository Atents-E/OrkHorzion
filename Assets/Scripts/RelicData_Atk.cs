using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RelicData Atk", menuName = "ScriptableObject/RelicData_Atk", order = 2)]
public class RelicData_Atk : RelicData
{
    [Header("추가 능력치")]
    public float damageBonus = 0.75f;
    public float criticalBonus = 0.05f;
}
