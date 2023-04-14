using TMPro;
using UnityEngine;

public class Progressbar : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro healthValue;

    [SerializeField]
    private SpriteRenderer back;

    [SerializeField]
    private SpriteRenderer fill;

    public void UpdateValues(int healthPoints, int maxValue)
    {
        float scaledValue = Mathf.InverseLerp(0, maxValue, healthPoints);

        fill.size = new Vector2(back.size.x * scaledValue, 1f);

        healthValue.text = healthPoints.ToString();
    }
}