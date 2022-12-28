using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Base : MonoBehaviour
{
    TestInputActions inputActions;

    private void Awake()
    {
        inputActions = new TestInputActions();
    }

    private void OnEnable()
    {
        inputActions.Test.Enable();
        inputActions.Test.Test1.performed += OnTest1;
        inputActions.Test.Test2.performed += OnTest2;
        inputActions.Test.Test3.performed += OnTest3;
        inputActions.Test.Test4.performed += OnTest4;
        inputActions.Test.Test5.performed += OnTest5;
        inputActions.Test.Test6.performed += OnTest6;
        inputActions.Test.Test7.performed += OnTest7;
        inputActions.Test.Test8.performed += OnTest8;
        inputActions.Test.Test9.performed += OnTest9;
    }

    private void OnDisable()
    {
        inputActions.Test.Test9.performed -= OnTest9;
        inputActions.Test.Test8.performed -= OnTest8;
        inputActions.Test.Test7.performed -= OnTest7;
        inputActions.Test.Test6.performed -= OnTest6;
        inputActions.Test.Test5.performed -= OnTest5;
        inputActions.Test.Test4.performed -= OnTest4;
        inputActions.Test.Test3.performed -= OnTest3;
        inputActions.Test.Test2.performed -= OnTest2;
        inputActions.Test.Test1.performed -= OnTest1;
        inputActions.Test.Disable();
    }

    protected virtual void OnTest1(InputAction.CallbackContext obj)
    {
    }

    protected virtual void OnTest2(InputAction.CallbackContext obj)
    {
    }
    protected virtual void OnTest3(InputAction.CallbackContext obj)
    {
    }
    protected virtual void OnTest4(InputAction.CallbackContext obj)
    {
    }
    protected virtual void OnTest5(InputAction.CallbackContext obj)
    {
    }
    protected virtual void OnTest6(InputAction.CallbackContext obj)
    {
    }
    protected virtual void OnTest7(InputAction.CallbackContext obj)
    {
    }
    protected virtual void OnTest8(InputAction.CallbackContext obj)
    {
    }
    protected virtual void OnTest9(InputAction.CallbackContext obj)
    {
    }
}
