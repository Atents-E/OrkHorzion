using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Character character;
    Warrior warrior;
    Wizard wizard;
    //Camera mainCamera; 
    EnemySpawner enemySpawner;

    public Character Character => character;
    public Warrior Warrior => warrior;   
    public Wizard Wizard => wizard;
    //public Camera MainCamera => mainCamera;
    public EnemySpawner EnemySpawner => enemySpawner;

    protected override void Initialize()
    {
        character = FindObjectOfType<Character>();
        warrior = FindObjectOfType<Warrior>();
        wizard = FindObjectOfType<Wizard>();
        //mainCamera = FindObjectOfType<Camera>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
}
