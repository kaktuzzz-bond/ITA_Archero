using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Weapon : MonoBehaviour
{
    public WeaponCard card;

    protected Rigidbody2D Rigidbody { get; private set; }

    private string _targetTag;

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

    public Weapon SetTargetTag(WeaponTarget target)
    {
        _targetTag = target.ToString();

        return this;
    }

    protected void Activate() => gameObject.SetActive(true);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag(_targetTag))
        {
            Debug.LogError("Weapon Collision");

            if (other.TryGetComponent(out Creature creature))

            {
                creature.Hit(card.damage);
            }
        }
    }
}