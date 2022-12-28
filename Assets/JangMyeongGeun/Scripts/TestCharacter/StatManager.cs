using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    Player player;
    ItemManager itemManager;

    private void Awake()
    {
       itemManager = GetComponent<ItemManager>();
       player = GetComponent<Player>();

    }


}
