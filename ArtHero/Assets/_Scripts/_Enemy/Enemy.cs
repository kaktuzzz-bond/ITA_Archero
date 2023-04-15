using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Creature
{
    [Header("Enemy Options")]
    [SerializeField] private EnemyCard card;

    [SerializeField] private SpriteRenderer pointerSprite;

    private bool IsPlayerInAttackDistance

    {
        get
        {
            bool isAttackDistance =
                    Vector3.Distance(transform.position, PlayerManager.Instance.Player.position)
                    <= card.weaponCard.distance;

            pointerSprite.color = isAttackDistance ? Color.red : Color.cyan;

            return isAttackDistance;
        }
    }

    
    private float _timer;

    private bool _isAttackAllowed;
    
    private void Awake()
    {
        InitializeInstance();
    }

    private void Attack(Vector3 direction)
    {
        if (!_isAttackAllowed) return;
        
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        ObjectPoolManager.Instance.GetWeapon()
                .SetPosition(transform.position)
                .SetRotation(Quaternion.AngleAxis(angle, Vector3.back))
                .SetParent(Battlefield.Instance.entityParent)
                .SetTargetTag(WeaponTarget.Player)
                .Shoot(direction);

        StartCoroutine(TimerRoutine());
    }

    public override void Die()
    {
        Debug.LogWarning("EnemyDie");
    }

    private void Update()
    {
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        if (IsPlayerInAttackDistance)
        {
            Vector3 playerPos = PlayerManager.Instance.Player.transform.position;

            Attack(playerPos - transform.position);
        }
    }

    private void InitializeInstance()
    {
        SetupCreature(card.health, card.health);

        _isAttackAllowed = true;
        
        EnemyManager.Instance.Add(this);
    }

    private IEnumerator TimerRoutine()
    {
        _isAttackAllowed = false;

        yield return new WaitForSeconds(card.attackInterval);

       _isAttackAllowed = true;
    }

    // #region ENABLE / DISABLE
    //
    // private void OnEnable()
    // {
    //     Observer.Instance.OnPlayerPositionChanged += CheckPlayerPosition;
    // }
    //
    // private void OnDisable()
    // {
    //     Observer.Instance.OnPlayerPositionChanged -= CheckPlayerPosition;
    // }
    //
    // #endregion
}