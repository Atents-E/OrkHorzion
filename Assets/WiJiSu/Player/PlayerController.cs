using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float currentSpeed;
    public float turnSpeed = 10.0f;

    enum MoveMode
    {
       Walk = 0,
       Run
    }

    MoveMode moveMode = MoveMode.Walk;

    Vector3 inputDir = Vector3.zero;
    Quaternion targetRotation = Quaternion.identity;

    PlayerInputActions inputActions;
    Animator anim;
    Character character;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        anim = GetComponent<Animator>();
        character = GetComponent<Character>();
    }

    private void Start()
    {
        walkSpeed = character.MoveSpeed;
        currentSpeed = character.MoveSpeed;
        runSpeed = walkSpeed + 5.0f;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.MoveModeChange.performed += OnMoveModeChange;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.MoveModeChange.performed -= OnMoveModeChange;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        if (character.isAlive) // 살아있을 때에만 업데이트
        {
            transform.Translate(walkSpeed * Time.deltaTime * inputDir, Space.World);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDir.x = input.x;  
        inputDir.y = 0.0f;
        inputDir.z = input.y;

        if (!context.canceled && character.isAlive)  
        {
            Quaternion cameraYRotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            inputDir = cameraYRotation * inputDir;

            targetRotation = Quaternion.LookRotation(inputDir);


            if (moveMode == MoveMode.Walk)
            {
                anim.SetFloat("Speed", 0.5f);
            }
            else
            {
                anim.SetFloat("Speed", 1.0f);
            }
        }
        else
        {
            inputDir = Vector3.zero;
            anim.SetFloat("Speed", 0.0f);
        }
    }

    private void OnMoveModeChange(InputAction.CallbackContext _)
    {
        Debug.Log("MoveChange");
       if(moveMode == MoveMode.Walk)
        {
            moveMode = MoveMode.Run;
            walkSpeed = runSpeed;
            if(inputDir != Vector3.zero)
            {
                anim.SetFloat("Speed", 1.0f);
            }

        }
        else
        {
            moveMode = MoveMode.Walk;
            walkSpeed = currentSpeed;
            if (inputDir != Vector3.zero)
            {
                anim.SetFloat("Speed", 0.5f);
            }
        }
    }

    private void OnAttack(InputAction.CallbackContext _)
    {
        if (character.isAlive)
        {
            anim.SetTrigger("Attack");
        }
        
    }


}
