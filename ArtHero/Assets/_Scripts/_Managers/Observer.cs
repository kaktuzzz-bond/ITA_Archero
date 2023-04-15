using System;
using UnityEngine;

public delegate void MapGeneratedCallback(Vector3 origin, int width, int height);

public delegate void PlayerCreatedCallback(Transform player);

public delegate void CreatureDieCallback(Creature creature);

public class Observer : Singleton<Observer>
{
    public event Action OnApplicationLaunched;

    public event Action OnStartButtonClick;

    public event Action OnPlayerDead;

    //public event Action OnPlayerPositionChanged;

    public event MapGeneratedCallback OnMapGenerated;

    public event PlayerCreatedCallback OnPlayerCreated;

    public event CreatureDieCallback OnCreatureDie;

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

    // public void OnPlayerPositionChangedNotify()
    // {
    //     OnPlayerPositionChanged?.Invoke();
    // }

    public void OnCreatureDieNotify(Creature creature)
    {
        if (creature.transform.CompareTag("Enemy"))
        {
            Debug.Log("ENEMY Creature's dead");
        }
        else if (creature.transform.CompareTag("Player"))
        {
            Debug.Log("PLAYER Creature's dead");
            
            OnPlayerDead?.Invoke();
        }
        else
        {
            Debug.Log("Unknown Creature's dead");
        }

        creature.Die();

        OnCreatureDie?.Invoke(creature);
    }
    
}