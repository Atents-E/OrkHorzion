using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Character character;
    Warrior warrior;
    Wizard wizard;
    //Camera mainCamera; 

    public Character Character => character;
    public Warrior Warrior => warrior;   
    public Wizard Wizard => wizard;


    protected override void Initialize()
    {
       character = FindObjectOfType<Character>();
        warrior = FindObjectOfType<Warrior>();
        wizard = FindObjectOfType<Wizard>();
        //mainCamera = FindObjectOfType<Camera>();
    }
}
