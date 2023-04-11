using System;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    public Transform playerPrefab;

    //на попозже:
    //здесь будем хранить атрибуты игрока - его усиления и эффекты
    //private int attributes = 0;
    //тут его уровень здоровья будет
    //public int Health{get; private set;}
    
    private void CreatePlayerIn(Vector3 origin, int width, int height)
    {
        Vector3 position = origin + new Vector3(width * 0.5f, 0.5f, 0);

        Transform player = Instantiate(playerPrefab, position, Quaternion.identity, transform);

        Observer.Instance.OnPlayerCreatedNotify(player);
    }


    #region ENABLE / DISABLE

    private void OnEnable()
    {
        Observer.Instance.OnMapGenerated += CreatePlayerIn;
    }

    private void OnDisable()
    {
        Observer.Instance.OnMapGenerated += CreatePlayerIn;
    }

    #endregion
  
}