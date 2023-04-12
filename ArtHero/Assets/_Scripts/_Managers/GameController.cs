using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    
    private void Start()
    {
        Observer.Instance.OnApplicationLaunchedNotify();
    }

    
    private void StartGame()
    {
        Battlefield.Instance.GenerateMap();
    }
    
    
    
    #region ENABLE / DISABLE

    private void OnEnable()
    {
        Observer.Instance.OnStartButtonClick += StartGame;
    }

    private void OnDisable()
    {
        Observer.Instance.OnStartButtonClick -= StartGame;
    }

    #endregion

  
}