using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("Player's movement speed")]
    [SerializeField] private float speed;

    [Tooltip("An interval between shots in seconds")]
    [SerializeField] private float shootingInterval;
    
    //на попозже:
    //здесь будем хранить атрибуты игрока - его усиления и эффекты
    //private int attributes = 0;
    //тут его уровень здоровья будет
    //public int Health{get; private set;}
    
    private Controls controls;

    private Rigidbody2D rb;

    //awake - это аналог шарпового конструктора; поэтому все инициализации лучше делать в этом методе
    private void Awake()
    {
        controls = new Controls();
        rb = GetComponent<Rigidbody2D>();
    }

    //при старте запускаем рутину стрельбы
    private void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    //при инпуте игрока запускаем этот метод, который в свою очередь запускает рутину движения
    private void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Start Moving");
        StartCoroutine(MoveRoutine(context));
    }

    //рутина движения работает в цикле - null означает, что обновление происмходит в каждом апдейте
    private IEnumerator MoveRoutine(InputAction.CallbackContext context)
    {
        //останавливаем рутину стрельбы
        StopCoroutine(ShootRoutine());

        //пока значение вектора не нулевое - персонаж движется.
        //вместо sqrMagnitude можно использовать Vector2.zero.
        //Надо спросить у Артема как это с точки зрения производительности
        while (context.ReadValue<Vector2>().sqrMagnitude != 0)
        {
            Vector3 direction = context.ReadValue<Vector2>();

            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

            transform.position += direction * (speed * Time.deltaTime);

            yield return null;
        }

        //возобновляем рутину стрельбы
        StartCoroutine(ShootRoutine());
    }

    //рутина стрельбы
    private IEnumerator ShootRoutine()
    {
        Debug.Log("Start Shooting");

        //здесь тоже можно рассмотреть замену на Vector2.zero
        while (controls.Player.Move.ReadValue<Vector2>().sqrMagnitude == 0)
        {
            MakeShot();

            yield return new WaitForSeconds(shootingInterval);
        }
    }

    private void MakeShot()
    {
        //здесь нужно будет дописать логику - если задетектили врага - стреляем
        //если нет - стоим спокойно
        Debug.Log("Shot");
    }

    #region - Enable / Disable -

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Move.started += Move;
    }

    private void OnDisable()
    {
        controls.Player.Move.started -= Move;
        controls.Disable();
    }

    #endregion
}