using System;
using UnityEngine;

public delegate void MapGeneratedCallback(Vector3 origin, int width, int height);

public delegate void PlayerCreatedCallback(Transform player);

public delegate void EnemyDieCallback(Enemy enemy);

public class Observer : Singleton<Observer>
{
    public event Action OnApplicationLaunched;

    public event Action OnStartButtonClick;

    public event Action OnPlayerPositionChanged;

    public event MapGeneratedCallback OnMapGenerated;

    public event PlayerCreatedCallback OnPlayerCreated;

    public event EnemyDieCallback OnEnemyDie;

    public void OnMapGeneratedNotify(Vector3 origin, int width, int height)
    {
        OnMapGenerated?.Invoke(origin, width, height);
    }

    public void OnPlayerCreatedNotify(Transform player)
    {
        OnPlayerCreated?.Invoke(player);
    }

    public void OnStartButtonClickNotify()
    {
        OnStartButtonClick?.Invoke();
    }

    public void OnApplicationLaunchedNotify()
    {
        OnApplicationLaunched?.Invoke();
    }

    public void OnPlayerPositionChangedNotify()
    {
        OnPlayerPositionChanged?.Invoke();
    }

    public void OnCreatureDieNotify(Enemy enemy)
    {
        Debug.Log("Creature's dead");
        OnEnemyDie?.Invoke(enemy);
    }
}