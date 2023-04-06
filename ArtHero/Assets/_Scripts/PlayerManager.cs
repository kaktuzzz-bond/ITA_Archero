using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    private Transform playerPrefab;

    private void CreatePlayerIn(Vector3 origin, int width, int height)
    {
        Vector3 position = origin + new Vector3(width * 0.5f, 0.5f, 0);

        Instantiate(playerPrefab, position, Quaternion.identity, transform);
    }

    private void OnEnable()
    {
        Battlefield.Instance.onMapGenerated += CreatePlayerIn;
    }

    private void OnDisable()
    {
        Battlefield.Instance.onMapGenerated += CreatePlayerIn;
    }
}