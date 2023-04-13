using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ArcHero/Enemy")]
public class EnemyCard : ScriptableObject
{
    public string id = "Enemy Name";

    [Min(0)]
    public int health;

    [Min(0)]
    public int damage;

    [Min(0)]
    public float attackDistance;
    
}