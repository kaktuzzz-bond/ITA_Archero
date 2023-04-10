using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController3D : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Transform rotator;

    [SerializeField] private Transform model;

    [SerializeField] private float shootingInterval;

    [SerializeField] private SpriteRenderer shootingIndicator;

    private WaitForSecondsRealtime _shootingWait;

    private readonly WaitForSecondsRealtime _indicatorWait = new(0.1f);

    private Controls _controls;

    private Rigidbody2D _rb;

    private Animator _animator;

    private static readonly int Move = Animator.StringToHash("Move");

    private static readonly int Shoot = Animator.StringToHash("Shoot");

    private static readonly int Stop = Animator.StringToHash("Stop");

    private static readonly int Death = Animator.StringToHash("Death");

    private void Awake()
    {
        _controls = new Controls();
        _rb = GetComponent<Rigidbody2D>();
        _shootingWait = new WaitForSecondsRealtime(shootingInterval);
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    private void Moving(InputAction.CallbackContext context)
    {
        _animator.SetTrigger(Move);

        StartCoroutine(MoveRoutine(context));
    }

    private IEnumerator MoveRoutine(InputAction.CallbackContext context)
    {
        while (context.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector3 direction = context.ReadValue<Vector2>();

            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            rotator.rotation = Quaternion.AngleAxis(angle, Vector3.back);

            model.rotation = rotator.rotation;

            //_rb.SetRotation(Quaternion.AngleAxis(angle, Vector3.back));

            _rb.MovePosition(transform.position + direction * (speed * Time.deltaTime));

            yield return null;
        }
    }

    private void Shooting(InputAction.CallbackContext context)
    {
        _animator.SetTrigger(Stop);

        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        InputAction move = _controls.Player.Move;

        while (move.ReadValue<Vector2>() == Vector2.zero)
        {
            MakeShot();

            yield return _shootingWait;
        }
    }

    private void MakeShot()
    {
        _animator.SetTrigger(Shoot);
    }

    private void Die()
    {
        _animator.SetTrigger(Death);
    }
    
    #region - Enable / Disable -

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Player.Move.started += Moving;
        _controls.Player.Move.canceled += Shooting;
    }

    private void OnDisable()
    {
        _controls.Player.Move.started -= Moving;
        _controls.Player.Move.canceled -= Shooting;
        _controls.Disable();
    }

    #endregion
}