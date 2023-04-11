using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ArcHero/Weapon")]
public class WeaponCard : ScriptableObject
{
    public Transform weaponPrefab;

    [Header("Weapon Setup")]
    public float speed;

    public float damage;

    public float reloadTime;

    public int hitTimes; //при контакте с противником летит насквозь

    public bool flyOverObstacles;

    public Vector3[] fraction; //если только один, ставим Vector3.forward

    [Header("Weapon Sound")]
    public Transform onlaunchVFXPrefab;

    public AudioClip onRicochetVFXPrefab;

    public AudioClip onHitVFXPrefab;

    [Header("Weapon Sound")]
    public AudioClip onLaunchSound;

    public AudioClip onRicochetSound;

    public AudioClip onHitSound;
}
