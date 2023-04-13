using System;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Battlefield : Singleton<Battlefield>
{
    [Foldout("Mapping")]
    public MapCard map;

    [Foldout("Mapping")]
    public Transform entityParent;

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
    private Transform cloudPrefab;

    [Foldout("Field"), SerializeField]
    public Tilemap field;

    [Foldout("Roots"), SerializeField]
    private Tilemap entities;

    
    public void GenerateMap()
    {
        Vector2Int mapSize = GetCheckedMapSize();

        for (int x = -1; x <= mapSize.x; x++)
        {
            for (int y = mapSize.y; y >= -1; y--)
            {
                //draws background
                background.SetTile(new Vector3Int(x, y), ruleTile);

                if (x < 0 || y < 0 || x == mapSize.x || y == mapSize.y) continue;

                field.SetTile(new Vector3Int(x, y), GetFieldTile(x, y));

                //draws collider
                DrawFieldEdges(Vector3.zero, mapSize.x, mapSize.y);

                //generates blockers
                GeneratePrefabs(x, y, map.blockers);

                //generates enemies
                GeneratePrefabs(x, y, map.enemies);
            }
        }

        SetupExitPortal(Vector3.zero, mapSize.x, mapSize.y);

        SetupCloud(Vector3.zero, mapSize.x, mapSize.y);

        Observer.Instance.OnMapGeneratedNotify(Vector3.zero, mapSize.x, mapSize.y);
    }

    private void GeneratePrefabs(int x, int y, Texture2D levelMap)
    {
        Color pixelColor = levelMap.GetPixel(x, y);

        if (pixelColor.a == 0) return;

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new(x, y, 0);

                position += field.tileAnchor;

                Instantiate(colorMapping.prefab, position, Quaternion.identity, entityParent);
            }
        }
    }

    private void DrawFieldEdges(Vector3 origin, int width, int height)
    {
        EdgeCollider2D edge = field.AddComponent<EdgeCollider2D>();

        edge.points = new[]
        {
                new Vector2(origin.x, origin.y),
                new Vector2(origin.x, origin.y + height),
                new Vector2(origin.x + width, origin.y + height),
                new Vector2(origin.x + width, origin.y),
                new Vector2(origin.x, origin.y)
        };
    }

    private Vector2Int GetCheckedMapSize()
    {
        if (map == null) throw new NullReferenceException("Map is null");

        if (map.blockers == null || map.enemies == null)
            throw new NullReferenceException("Map textures are null");

        if (map.blockers.width != map.enemies.width ||
            map.blockers.height != map.enemies.height)
            throw new Exception("The size of the map textures is not equal");

        return new Vector2Int(map.blockers.width, map.blockers.height);
    }

    private void SetupExitPortal(Vector3 origin, int width, int height)
    {
        Vector3 position = origin + new Vector3(width * 0.5f, height + 1f, 0f);

        Instantiate(exitPrefab, position, Quaternion.identity, transform);
    }

    private void SetupCloud(Vector3 origin, int width, int height)
    {
        Vector3 position = origin + new Vector3(width * 0.5f, 0, 0);

        Instantiate(cloudPrefab, position, Quaternion.identity, transform);
    }

    private Tile GetFieldTile(int x, int y)
    {
        Tile GetRandomFrom(Tile[] t) => t[Random.Range(0, t.Length)];

        Tile tile;

        if ((y & 1) == 0)
        {
            tile = (x & 1) == 0 ? GetRandomFrom(darkTiles) : GetRandomFrom(lightTiles);
        }
        else
        {
            tile = (x & 1) == 0 ? GetRandomFrom(lightTiles) : GetRandomFrom(darkTiles);
        }

        return tile;
    }
}