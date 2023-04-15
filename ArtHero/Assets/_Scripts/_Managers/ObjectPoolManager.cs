using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private readonly Dictionary<WeaponCard, ObjectPool> _weaponPools = new();

    public Weapon GetWeapon(WeaponCard key)
    {
        // WeaponCard card = PlayerManager.Instance.WeaponCard;

        if (!_weaponPools.ContainsKey(key))
        {
            _weaponPools.Add(key, new ObjectPool(key.weaponPrefab));
        }

        return _weaponPools[key].Get().GetComponent<Weapon>();
    }

    public void ReleaseWeapon(WeaponCard card, Transform obj)
    {
        _weaponPools[card].Release(obj);
    }
}