using UnityEngine;

public interface IWeapon
{
    void Shoot(Transform shooter, Quaternion direction);
}
