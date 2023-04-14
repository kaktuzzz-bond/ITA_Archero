using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Weapon : MonoBehaviour
{
    public WeaponCard card;

    protected Rigidbody2D Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public abstract void Shoot(Vector3 direction);

 
    public Weapon SetPosition(Vector3 position)
    {
        transform.position = position;

        return this;
    }

    public Weapon SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;

        return this;
    }
    
    public Weapon SetParent(Transform parent)
    {
        transform.SetParent(parent);

        return this;
    }
    public void Activate() => gameObject.SetActive(true);

}