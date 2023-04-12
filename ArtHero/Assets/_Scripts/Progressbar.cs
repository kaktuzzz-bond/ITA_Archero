using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Progressbar : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro healthValue;

    private int _maxHealthValue = 100;

    [Range(0, 100)]
    public int playerHealth = 50;

    private const float _smoothTime = 0.2f;

    public int HealthValue
    {
        set => StartCoroutine(ChangeHealthValueRoutine(value));
    }

    private void Start()
    {
        Invoke(nameof(Test), 3f);
    }



    private void Test()
    {
        HealthValue = 0;
    }
    
    [SerializeField]
    private SpriteRenderer progressbar;

    private float _maxProgressbarWidth;

    private void Awake()
    {
        _maxProgressbarWidth = progressbar.size.x;
    }

    private IEnumerator ChangeHealthValueRoutine(int targetValue)
    {
        int currentHealth = playerHealth;
        
        while (playerHealth > targetValue)
        {
            float smoothValue = Mathf.Lerp(currentHealth, targetValue, _smoothTime * Time.deltaTime);
            
            float scaledValue = Mathf.InverseLerp(0, _maxHealthValue, smoothValue);

            progressbar.size = new Vector2(_maxProgressbarWidth * scaledValue, 1f);

            healthValue.text = targetValue.ToString();

            yield return null;
        }
      
    }

    public void SetupMaxHealthValue(int health) => _maxHealthValue = health;
}