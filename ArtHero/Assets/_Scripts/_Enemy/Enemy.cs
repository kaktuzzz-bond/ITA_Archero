using System;
using UnityEngine;

public abstract class Enemy : Creature
{
    [Header("Enemy Options")]
    [SerializeField] private EnemyCard card;

    [SerializeField] private SpriteRenderer pointerSprite;

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

    public override void Die()
    {
        Debug.LogWarning("EnemyDie");
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
        Init(card.health, card.health / 2);
        
        EnemyManager.Instance.Add(this);

        _damage = card.damage;
        _attackDistance = card.attackDistance;
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