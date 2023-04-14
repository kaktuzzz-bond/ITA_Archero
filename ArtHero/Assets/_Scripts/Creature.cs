using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class Creature : MonoBehaviour
{
    [Header("Creature Options")]
    [SerializeField]
    private Progressbar progressbar;
    
    private int _maxHealth;
    
    private int _currentHealth;

    protected int CurrentHealth
    {
        get => _currentHealth;

        private set
        {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            
            progressbar.UpdateValues(_currentHealth, _maxHealth);
        }
    }

    protected void Init(int maxHealth, int healthOnStart)
    {
        _maxHealth = maxHealth;
        CurrentHealth = healthOnStart;
    }
}