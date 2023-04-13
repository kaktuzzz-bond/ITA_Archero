using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyCard card;

    [SerializeField] private Progressbar progressbar;
    
    [SerializeField] private SpriteRenderer pointerSprite;
    
    private int _health;

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
        _health = card.health;
        _damage = card.damage;
        _attackDistance = card.attackDistance;
        
        progressbar.UpdateProgressbar(_health);
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