using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemData_Base[] ItemDatas;

    public ItemData_Base this[uint id] => ItemDatas[id];

    public ItemData_Base this[ItemIDCode code] => ItemDatas[(int)code];

    public int Length => ItemDatas.Length;

}
