using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    public Action onStartButtonClicked;

    [Foldout("Canvases"), SerializeField]
    private Canvas uiCanvas;

    [Foldout("Canvases"), SerializeField]
    private Button startButton;

    [Foldout("Canvases"), SerializeField]
    private Canvas gameCanvas;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        uiCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
    }

    private void StartGame()
    {
        uiCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
        onStartButtonClicked?.Invoke();
    }
}