using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    public float speed;

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Field")
        {            
            //Destroy(this.gameObject);
            PoolManager.Instance.Push(this.gameObject.transform);
        }
    }

}
