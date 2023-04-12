using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
    }
}
