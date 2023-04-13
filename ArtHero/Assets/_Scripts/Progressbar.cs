using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Progressbar : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro healthValue;

    [SerializeField]
    private SpriteRenderer progressbar;

    private float _maxProgressbarWidth;

    private void Awake()
    {
        _maxProgressbarWidth = progressbar.size.x;
    }
    
    public void UpdateProgressbar(int healthPoints, int maxValue)
    {
        float scaledValue = Mathf.InverseLerp(0, maxValue, healthPoints);
        progressbar.size = new Vector2(_maxProgressbarWidth * scaledValue, 1f);
        healthValue.text = healthPoints.ToString();
    }
}