using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody2D rb;
    private Controls controls;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        Vector2 move = controls.Player.Move.ReadValue<Vector2>() * speed;
        rb.velocity = move * speed;

        if (move != Vector2.zero)
        {
            float rotation = Vector2.SignedAngle(Vector2.up, move);
            rb.rotation = rotation;
            Debug.Log(rotation);
        }
    }
}
