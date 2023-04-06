using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Battlefield : Singleton<Battlefield>
{
    public Action<Vector3, int, int> onMapGenerated;

    [Foldout("Mapping")]
    public Texture2D levelMap;

    [Foldout("Mapping")]
    public ColorToPrefab[] colorMappings;

    [Foldout("Background"), SerializeField]
    private RuleTile ruleTile;

    [Foldout("Background"), SerializeField]
    private Tilemap background;

    [Foldout("Field"), SerializeField]
    private Tile[] darkTiles;

    [Foldout("Field"), SerializeField]
    private Tile[] lightTiles;

    [Foldout("Field"), SerializeField]
    private Transform exitPrefab;

    [Foldout("Field"), SerializeField]
    private Tilemap field;

    [Foldout("Roots"), SerializeField]
    private Tilemap entities;

    public void GenerateMap()
    {
        for (int x = -1; x <= levelMap.width; x++)
        {
            for (int y = levelMap.height; y >= -1; y--)
            {
                DrawBackgroundTileIn(x, y);
                
                if (x < 0 || y < 0 ||
                    x == levelMap.width ||
                    y == levelMap.height)
                {
                    continue;
                }

                DrawFieldTileIn(x, y);
                GeneratePrefabs(x, y);
            }
        }

        SetupExitPortal(Vector3.zero, levelMap.width, levelMap.height);

        onMapGenerated?.Invoke(Vector3.zero, levelMap.width, levelMap.height);
    }

    
    
    
    
    private void SetupExitPortal(Vector3 origin, int width, int height)
    {
        Vector3 position = origin + new Vector3(width * 0.5f, height + 1f, 0f);

        Instantiate(exitPrefab, position, Quaternion.identity, transform);
    }

    private void DrawBackgroundTileIn(int x, int y)
    {
        Vector3Int position = new(x, y);
        background.SetTile(position, ruleTile);
    }

    private void DrawFieldTileIn(int x, int y)
    {
        Tile tile;

        Vector3Int position = new(x, y);

        if ((y & 1) == 0)
        {
            tile = (x & 1) == 0 ? GetRandomFrom(darkTiles) : GetRandomFrom(lightTiles);
        }
        else
        {
            tile = (x & 1) == 0 ? GetRandomFrom(lightTiles) : GetRandomFrom(darkTiles);
        }

        field.SetTile(position, tile);
    }

    private Tile GetRandomFrom(Tile[] tiles)
    {
        int i = Random.Range(0, tiles.Length);

        return tiles[i];
    }

    private void GeneratePrefabs(int x, int y)
    {
        Color pixelColor = levelMap.GetPixel(x, y);

        if (pixelColor.a == 0) return;

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new(x, y, 0);

                position += entities.GetComponent<Tilemap>().tileAnchor;

                Transform parent = colorMapping.tilemap.transform;

                Instantiate(colorMapping.prefab, position, Quaternion.identity, parent);
            }
        }
    }
}