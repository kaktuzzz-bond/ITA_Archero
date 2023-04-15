using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController3D : Creature
{
    [Header("Player Options")]
    [SerializeField] private float speed;

    [SerializeField] private float shootingInterval;

    [SerializeField] private Transform model;

    private Controls _controls;

    private Rigidbody2D _rb;

    private Animator _animator;

    private bool _isDead;

    #region ANIMATIONS

    private static readonly int Move = Animator.StringToHash("Move");

    private static readonly int Shoot = Animator.StringToHash("Shoot");

    private static readonly int Stop = Animator.StringToHash("Stop");

    private static readonly int Death = Animator.StringToHash("Death");

    #endregion

    private void Awake()
    {
        _controls = new Controls();

        _rb = GetComponent<Rigidbody2D>();

        _animator = model.GetComponentInChildren<Animator>();

        SetupCreature(250, 250);
    }

    private void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    private void Moving(InputAction.CallbackContext context)
    {
        if (_isDead) return;
        
        _animator.SetTrigger(Move);

        StopAllCoroutines();

        StartCoroutine(MoveRoutine(context));
    }

    private IEnumerator MoveRoutine(InputAction.CallbackContext context)
    {
        while (context.ReadValue<Vector2>() != Vector2.zero)
        {
            if (_isDead) yield break;

            Vector3 direction = context.ReadValue<Vector2>();

            RotateModel(direction, Vector3.back);

            // _rb.SetRotation(Quaternion.AngleAxis(angle, Vector3.back));

            _rb.MovePosition(transform.position + direction * (speed * Time.deltaTime));

            //Observer.Instance.OnPlayerPositionChangedNotify();

            yield return null;
        }
    }

    private void Shooting(InputAction.CallbackContext context)
    {
        if (_isDead) return;

        _animator.SetTrigger(Stop);

        StopAllCoroutines();

        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        InputAction move = _controls.Player.Move;

        WaitForSeconds wait = new(shootingInterval);

        while (move.ReadValue<Vector2>() == Vector2.zero &&
               EnemyManager.Instance.Alive.Count > 0)
        {
            if (_isDead) yield break;
            
            Vector3 nearestEnemyPos = EnemyManager.Instance.Nearest.transform.position;

            Vector3 direction = nearestEnemyPos - transform.position;

            if (Vector3.Distance(transform.position, nearestEnemyPos) <=
                PlayerManager.Instance.WeaponCard.distance)
            {
                RotateModel(direction, Vector3.back);

                MakeShot(direction);
            }

            yield return wait;
        }
    }

    private void MakeShot(Vector3 direction)
    {
        _animator.SetTrigger(Shoot);

        ObjectPoolManager.Instance.GetWeapon(PlayerManager.Instance.WeaponCard)
                .SetPosition(transform.position)
                .SetRotation(model.rotation)
                .SetParent(Battlefield.Instance.entityParent)
                .SetTargetTag(WeaponTarget.Enemy)
                .Shoot(direction);
    }

    public override void Die()
    {
        if (_isDead) return;
        
        _animator.SetTrigger(Death);

        _isDead = true;
    }

    private void RotateModel(Vector3 direction, Vector3 axis)
    {
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        model.rotation = Quaternion.AngleAxis(angle, Vector3.back);
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