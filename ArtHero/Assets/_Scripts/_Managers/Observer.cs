using System;
using UnityEngine;

public delegate void MapGeneratedCallback(Vector3 origin, int width, int height);

public delegate void PlayerCreatedCallback(Transform player);

public class Observer : Singleton<Observer>
{
    public event Action OnApplicationLaunched;

    public event Action OnStartButtonClick;

    public event MapGeneratedCallback OnMapGenerated;

    public event PlayerCreatedCallback OnPlayerCreated;

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
}