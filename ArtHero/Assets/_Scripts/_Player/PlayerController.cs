using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("Player's movement speed")]
    [SerializeField] private float speed;

    [Tooltip("An interval between shots in seconds")]
    [SerializeField] private float shootingInterval;

    [SerializeField] private SpriteRenderer shootingIndicator;

    private WaitForSecondsRealtime _shootingWait;

    private readonly WaitForSeconds _indicatorWait = new(0.1f);

    private Controls _controls;

    private Rigidbody2D _rb;

    //awake - это аналог шарпового конструктора; поэтому все инициализации лучше делать в этом методе
    private void Awake()
    {
        _controls = new Controls();
        _rb = GetComponent<Rigidbody2D>();
        _shootingWait = new WaitForSecondsRealtime(shootingInterval);
    }

    //при инпуте игрока запускаем этот метод, который в свою очередь запускает рутину движения
    private void Move(InputAction.CallbackContext context)
    {
        StartCoroutine(MoveRoutine(context));
    }

    //рутина движения работает в цикле - null означает, что обновление происмходит в каждом апдейте
    private IEnumerator MoveRoutine(InputAction.CallbackContext context)
    {
        //останавливаем рутину стрельбы
        StopCoroutine(ShootRoutine(context));

        while (context.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector3 direction = context.ReadValue<Vector2>();

            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            _rb.SetRotation(Quaternion.AngleAxis(angle, Vector3.back));

            _rb.MovePosition(transform.position + direction * (speed * Time.deltaTime));

            yield return null;
        }

        StartCoroutine(ShootRoutine(context));
    }

    //рутина стрельбы
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
    }

    private void OnDisable()
    {
        _controls.Player.Move.started -= Move;
        _controls.Disable();
    }

    #endregion
}