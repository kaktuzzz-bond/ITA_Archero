using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<Enemy> Alive { get; private set; }

    private List<Enemy> _dead;

    public Enemy Nearest => Alive
            .OrderBy((enemy) => Vector3.Distance(enemy.transform.position, PlayerManager.Instance.Player.position))
            .FirstOrDefault();
    
    public void Add(Enemy enemy)
    {
        Alive ??= new List<Enemy>();

        Alive.Add(enemy);
    }

    public void Remove(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        
        Alive.Remove(enemy);

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