using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ArcHero/Weapon")]
public class WeaponCard : ScriptableObject, IWeapon
{
    public Transform weaponPrefab;

    [Header("Weapon Setup")]
    public float speed;

    public float damage;

    public float distance;

    //public Vector3[] fraction;

    //[Header("Weapon Sound")]
    //public Transform onlaunchVFXPrefab;

    //public AudioClip onRicochetVFXPrefab;

    //public AudioClip onHitVFXPrefab;

    //[Header("Weapon Sound")]
    //public AudioClip onLaunchSound;

    //public AudioClip onRicochetSound;

    //public AudioClip onHitSound;

    public void Shoot(Transform shooter, Quaternion direction)
    {
        Transform prefab = PoolManager.Instance.Pool(weaponPrefab.ToString(), shooter.transform.position, direction);
        //Transform prefab = Instantiate(weaponPrefab, shooter.transform.position, shooter.transform.rotation);
    }
}
