using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "ArcHero/Map")]
public class MapCard : ScriptableObject
{
    public string ID = "Map ID";

    public Texture2D blockers;

    public Texture2D enemies;
}