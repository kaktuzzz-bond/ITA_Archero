using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Battlefield : Singleton<Battlefield>
{
    public Action<Vector2> OnMapGenerated;

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
    private Tilemap field;

    [Foldout("Roots"), SerializeField]
    private Tilemap entities;

    public void GenerateMap()
    {
        for (int x = -1; x <= levelMap.width; x++)
        {
            for (int y = -1; y <= levelMap.height; y++)
            {
                DrawBackgroundTileIn(x, y);

                if (x >= 0 &&
                    y >= 0 &&
                    x < levelMap.width &&
                    y < levelMap.height)
                {
                    GeneratePrefabs(x, y);
                }
            }
        }

        Vector2 camPos = new((float)levelMap.width / 2f, (float)levelMap.height / 2f);
        
        OnMapGenerated?.Invoke(camPos);
    }

    private void DrawBackgroundTileIn(int x, int y)
    {
        Vector3Int position = new(x, y);
        background.SetTile(position, ruleTile);
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