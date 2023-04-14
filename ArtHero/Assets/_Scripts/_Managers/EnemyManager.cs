using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<Enemy> _alive;

    private List<Enemy> _dead;

    public Enemy Nearest => _alive
            .OrderBy((enemy) => Vector3.Distance(enemy.transform.position, PlayerManager.Instance.Player.position))
            .FirstOrDefault();

    public void Add(Enemy enemy)
    {
        _alive ??= new List<Enemy>();

        _alive.Add(enemy);
    }

    public void Remove(Enemy enemy)
    {
        _alive.Remove(enemy);

        _dead ??= new List<Enemy>();

        _dead.Add(enemy);
    }



    #region ENABLE /DISABLE

    private void OnEnable()
    {
        Observer.Instance.OnEnemyDie += Remove;
    }

    private void OnDisable()
    {
        Observer.Instance.OnEnemyDie -= Remove;
    }

    #endregion
}