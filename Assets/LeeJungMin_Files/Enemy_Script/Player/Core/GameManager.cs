using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Character character;
    Warrior warrior;
    Wizard wizard;
    PlayerGold playerGold;
    Canvas canvas;

    public Character Character => character;
    public Warrior Warrior => warrior;   
    public Wizard Wizard => wizard;
    public PlayerGold PlayerGold => playerGold;
    public Canvas Canvas => canvas;
    
    protected override void Initialize()
    {
        character = FindObjectOfType<Character>();
        warrior = FindObjectOfType<Warrior>();
        wizard = FindObjectOfType<Wizard>();
        playerGold = FindObjectOfType<PlayerGold>();
        canvas = FindObjectOfType<Canvas>();
    }
}
