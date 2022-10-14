using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RelicSO", menuName = "ScriptableObject/Relic", order = int.MaxValue)]
public class RelicSO : ScriptableObject
{
    public Sprite RelicTypeIcon;
    public string[] relicName;
    public string[] relicID;
    public Sprite[] relicIcon;
    public bool stackUp;
}
