using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyCard card;

    [SerializeField] private Progressbar progressbar;
    
    [SerializeField] private SpriteRenderer pointerSprite;
    
    private int _maxHealth;
    
    private int _currentHealth;

    private int _damage;

    private float _attackDistance;

    private bool IsPlayerInAttackDistance 

    {
        get
        {
            bool isAttackDistance = 
                    Vector3.Distance(transform.position, PlayerManager.Instance.Player.position)
                                    <= _attackDistance;
            pointerSprite.color = isAttackDistance ? Color.red : Color.cyan;

            return isAttackDistance;
        }
       
    }

    private void Awake()
    {
        InitializeInstance();
    }
    

    private void Attack(int damage)
    {
        Debug.Log($"Attack: -{damage}");
    }

    private void CheckPlayerPosition()
    {
        if (IsPlayerInAttackDistance)
        {
            Attack(_damage);
        }
    }

    private void InitializeInstance()
    {
        EnemyManager.Instance.Add(this);
        
        _maxHealth = card.health;
        _damage = card.damage;
        _attackDistance = card.attackDistance;
        
        _currentHealth = _maxHealth;
        
        progressbar.UpdateProgressbar(_currentHealth, _maxHealth);
    }

    #region ENABLE / DISABLE

    private void OnEnable()
    {
        Observer.Instance.OnPlayerPositionChanged += CheckPlayerPosition;
    }

    private void OnDisable()
    {
        Observer.Instance.OnPlayerPositionChanged -= CheckPlayerPosition;
    }

    #endregion
}