using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New RelicData", menuName = "ScriptableObject/RelicData", order = 1)]
public class RelicData : ScriptableObject
{
    [Header("유물 기본 데이터")]
    public int RelicTypeID;
    public Sprite RelicTypeIcon;
    public string relicName;
    public Sprite relicIcon;
    public int maxStackCount = 3;
}

