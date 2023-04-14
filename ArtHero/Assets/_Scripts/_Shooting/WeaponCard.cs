using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ArcHero/Weapon")]
public class WeaponCard : ScriptableObject
{
    public Transform weaponPrefab;

    public string id = "ID";

    [Header("Weapon Setup")]
    public float speed;

    public int damage;

    public float distance;

    // public Weapon GetWeapon()
    // {
    //     if (weaponPrefab.TryGetComponent(out Weapon weapon))
    //     {
    //         return weapon;
    //     }
    //
    //     Debug.LogError("Prefab doesn't have Weapon Component");
    //
    //     return null;
    // }
}