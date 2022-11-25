using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class TestSlot : MonoBehaviour
{
    public GameObject itemEmptySlot;


    public void AddNewItemEmptySlot()
    {
        Instantiate(itemEmptySlot,transform);
    }

}
