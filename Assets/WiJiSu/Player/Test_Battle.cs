using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Battle : TestBase
{
    Warrior warrior;
    Wizard wizard;

    private void Start()
    {
     warrior = GameManager.Inst.Warrior;
     wizard = GameManager.Inst.Wizard;
    }

    protected override void Test1(InputAction.CallbackContext _)
    { 
        warrior.TakeDamage(50);
       
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        wizard.TakeDamage(50);
    }


}
