using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_PlayerController : MonoBehaviour, IBattle, IHealth
{
    public float playerHp;
    public float maxPlayerHp;
    public float attackPower = 10;
    public float defencePower = 3;
    public bool isAlive = true;

    Test_PlayerInputActions inputActions;

    Vector3 inputDir = Vector3.zero;

    public Transform damageTextPos;
    public GameObject damageTextPrefab;

    public float speed = 8.0f;

    IBattle target;
    public float HP
    {
        get => playerHp;
        set
        {
            if (playerHp != value)
            {
                playerHp = value;

                if (playerHp < 0)
                {
                    playerHp = 0;
                    Die();
                }

                onHealthChange?.Invoke(playerHp / maxPlayerHp);

                playerHp = Mathf.Clamp(playerHp, 0.0f, maxPlayerHp);
            }
        }
    }

    public float MaxHP
    {
        get => maxPlayerHp;
        set => maxPlayerHp = value;
    }


    public Action<float> onHealthChange { get; set; }

    public Action onDie { get; set; }

    public float AttackPower => attackPower;

    public float DefencePower => defencePower;

    private void Awake()
    {
        inputActions = new Test_PlayerInputActions();
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * inputDir);
    }

    private void OnEnable()
    {
        inputActions.Test_Player.Enable();
        inputActions.Test_Player.Move.performed += OnMove;
        inputActions.Test_Player.Move.canceled += OnMove;
        inputActions.Test_Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Test_Player.Attack.performed -= OnAttack;
        inputActions.Test_Player.Move.performed -= OnMove;
        inputActions.Test_Player.Move.canceled -= OnMove;
        inputActions.Test_Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;
    }

    public void Die()
    {
        isAlive = false;
        Debug.Log("사망");
        speed = 0;
    }

    private void OnAttack(InputAction.CallbackContext _)
    {
        if (isAlive)
        {
            Attack(target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isAlive)
        {
            target = other.GetComponent<IBattle>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && isAlive)
        {
            target = null;
        }
    }

    /// <summary>
    /// 공격용 함수
    /// </summary>
    /// <param name="target">공격할 대상</param>
    public void Attack(IBattle target)
    {
        target?.TakeDamage(AttackPower);
    }

    public void TakeDamage(float damage)
    {
        if (isAlive)
        {
            GameObject obj = Instantiate(damageTextPrefab, damageTextPos);

            obj.GetComponent<DamageText>().Damage = (int)(damage - defencePower);

            HP -= (damage - defencePower);
        }
    }
}
