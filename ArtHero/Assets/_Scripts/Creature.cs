using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class Creature : MonoBehaviour
{
    [Header("Creature Options")]
    [SerializeField]
    private Progressbar progressbar;

    private int _maxHealth;

    private int _currentHealth;

    private int CurrentHealth
    {
        get => _currentHealth;

        set
        {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);

            if (_currentHealth <= 0)
            {
                Observer.Instance.OnCreatureDieNotify(this);
            }

            progressbar.UpdateValues(_currentHealth, _maxHealth);
        }
    }

    protected void SetupCreature(int maxHealth, int healthOnStart)
    {
        _maxHealth = maxHealth;
        CurrentHealth = healthOnStart;
    }

    public virtual void Hit(int damage)
    {
        CurrentHealth -= damage;
    }

    public abstract void Die();
}