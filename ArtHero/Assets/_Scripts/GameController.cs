using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private void StartGame()
    {
        Battlefield.Instance.GenerateMap();
    }

    #region - Enable / Disable -

    private void OnEnable()
    {
        UIController.Instance.onStartButtonClicked += StartGame;
    }

    private void OnDisable()
    {
        UIController.Instance.onStartButtonClicked -= StartGame;
    }

    #endregion
}