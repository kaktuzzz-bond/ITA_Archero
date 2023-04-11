using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Tooltip("Player's movement speed")]
    [SerializeField] private float speed;

    [Tooltip("Player's weapon")]
    [SerializeField] private WeaponCard weaponCard;

    [Tooltip("An interval between shots in seconds")]
    [SerializeField] private float shootingInterval;

    [SerializeField] private SpriteRenderer shootingIndicator;

    private WaitForSecondsRealtime _shootingWait;

    private readonly WaitForSecondsRealtime _indicatorWait = new(0.1f);

    private Controls _controls;

    private Rigidbody2D _rb;


    private void Awake()
    {
        _controls = new Controls();
        _rb = GetComponent<Rigidbody2D>();
        _shootingWait = new WaitForSecondsRealtime(shootingInterval);
    }

    private void Move(InputAction.CallbackContext context)
    {
        StartCoroutine(MoveRoutine(context));
    }

    private IEnumerator MoveRoutine(InputAction.CallbackContext context)
    {
        while (context.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector3 direction = context.ReadValue<Vector2>();

            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            _rb.SetRotation(Quaternion.AngleAxis(angle, Vector3.back));

            _rb.MovePosition(transform.position + direction * (speed * Time.deltaTime));

            yield return null;
        }
    }

    private void Shooting(InputAction.CallbackContext context)
    {
        StartCoroutine(ShootRoutine(context));
    }

    private IEnumerator ShootRoutine(InputAction.CallbackContext context)
    {
        while (context.ReadValue<Vector2>() == Vector2.zero)
        {
            MakeShot();

            yield return _shootingWait;
        }
    }

    private void MakeShot()
    {
        //здесь нужно будет дописать логику - если задетектили врага - стреляем
        //если нет - стоим спокойно

        weaponCard.Shoot(this.transform); //только стрельба

        Debug.Log("Shot");

        StartCoroutine(IndicatorRoutine());
    }

    private IEnumerator IndicatorRoutine()
    {
        shootingIndicator.color = Color.red;

        yield return _indicatorWait;

        shootingIndicator.color = Color.green;
    }

    #region - Enable / Disable -

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Player.Move.started += Move;
        _controls.Player.Move.canceled += Shooting;
    }

    private void OnDisable()
    {
        _controls.Player.Move.started -= Move;
        _controls.Player.Move.canceled -= Shooting;
        _controls.Disable();
    }

    #endregion
}