using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicAtk : Relic
{
    string[] relicsName = { "으악", "우엑", "뜨악" };

    override void GenerateRelics(relicsName[0],1,true)
    { 
        
    };


    private void Start()
    {
        Debug.Log($"{relicName}");
    }

}
