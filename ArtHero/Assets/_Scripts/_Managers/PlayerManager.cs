using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Setup")]
    [SerializeField]
    public Transform playerPrefab;
    
    public int MaxPlayerHealth { get; private set; } = 100;

    private Action<int, int> _healthValueChangedCallback;

    private Progressbar _progressbar;

    public Transform Player { get; private set; }

    private int _startHealth;

    private int _currentHealth;

    public int Health
    {
        get => _currentHealth;

        private set
        {
            _currentHealth = value;

            _healthValueChangedCallback.Invoke(_currentHealth, MaxPlayerHealth);
        }
    }

    private void Start()
    {
        _startHealth = MaxPlayerHealth;
        
        Invoke(nameof(TestProgress), 3f);
    }

    private void TestProgress()
    {
        Health -= 30;
        
        Invoke(nameof(TestProgress2), 3f);
    }
    
    private void TestProgress2()
    {
        Health += 10;
    }

    private void CreatePlayerIn(Vector3 origin, int width, int height)
    {
        Vector3 position = origin + new Vector3(width * 0.5f, 0.5f, 0);

        Player = Instantiate(playerPrefab, position, Quaternion.identity, transform);

        _progressbar = Player.GetComponentInChildren<Progressbar>();

        if (_progressbar == null)
            Debug.LogError("Progressbar is not found");

        _healthValueChangedCallback += _progressbar.UpdateProgressbar;

        Health = _startHealth;

        Observer.Instance.OnPlayerCreatedNotify(Player);
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