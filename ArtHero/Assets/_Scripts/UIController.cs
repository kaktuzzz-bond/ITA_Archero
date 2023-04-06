using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
        public Action onStartButtonClicked;

        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private Button startButton;

        private void Awake()
        {
                startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
                
                canvas.gameObject.SetActive(false);
                onStartButtonClicked?.Invoke();
        }
}