using UnityEngine;

public class Utility : MonoBehaviour
{
    public static float GetAngle(Vector3 direction)
    {
        return Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    }
}