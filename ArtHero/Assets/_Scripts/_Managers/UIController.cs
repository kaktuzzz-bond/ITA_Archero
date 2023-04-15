using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    [Foldout("Canvases"), SerializeField]
    private Canvas uiCanvas;

    [Foldout("Screens"), SerializeField]
    private GameObject splashScreen;

    [Foldout("Screens"), SerializeField]
    private GameObject gameScreen;

    [Foldout("Screens"), SerializeField]
    private GameObject pauseScreen;

    [Foldout("Buttons"), SerializeField]
    private Button startButton;

    [Foldout("Buttons"), SerializeField]
    private Button resumeButton;

    private GameObject _activeScreen;

    private void Awake()
    {
        splashScreen.SetActive(false);
        gameScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);

        startButton.onClick.AddListener(Observer.Instance.OnStartButtonClickNotify);

        //resumeButton.onClick.AddListener(Observer.Instance.OnStartButtonClickNotify);
    }

    private void StartSplashScreen() => MakeActive(splashScreen);

    private void StartGameScreen() => MakeActive(gameScreen);

    private void StartPauseScreen() => MakeActive(pauseScreen);

    private void MakeActive(GameObject screen)
    {
        if (_activeScreen != null)
        {
            _activeScreen.SetActive(false);
        }

        _activeScreen = screen;

        _activeScreen.SetActive(true);
    }

    #region ENABLE / DISABLE

    private void OnEnable()
    {
        Observer.Instance.OnApplicationLaunched += StartSplashScreen;
        Observer.Instance.OnStartButtonClick += StartGameScreen;
        Observer.Instance.OnPlayerDead += StartPauseScreen;
    }

    private void OnDisable()
    {
        Observer.Instance.OnApplicationLaunched -= StartSplashScreen;
        Observer.Instance.OnStartButtonClick -= StartGameScreen;
        Observer.Instance.OnPlayerDead -= StartPauseScreen;
    }

    #endregion
}