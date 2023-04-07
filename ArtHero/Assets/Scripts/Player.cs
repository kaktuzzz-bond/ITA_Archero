using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody2D rb;
    private Controls controls;
    public bool canMove;
    public GameObject target;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        rb = GetComponent<Rigidbody2D>();

        canMove = true;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 move = controls.Player.Move.ReadValue<Vector2>();
            rb.velocity = move * speed * Time.fixedDeltaTime;

            if (move != Vector2.zero)
            {
                float rotation = Vector2.SignedAngle(Vector2.up, move);
                rb.rotation = rotation;
                Debug.Log(rotation);
            }
            else
            {
                Aim();
                Shoot();
            }
        }
        
    }

    public void Aim()
    {

    }

    public void Shoot()
    {

    }

    public void CantMove(float time)
    {
        canMove = false;
    }

    public void Death()
    {
        controls.Disable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }
}
