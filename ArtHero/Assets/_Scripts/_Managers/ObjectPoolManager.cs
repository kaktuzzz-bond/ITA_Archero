using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private readonly Dictionary<WeaponCard, ObjectPool> _weaponPools = new();

    public Weapon GetWeapon()
    {
        WeaponCard card = PlayerManager.Instance.WeaponCard;

        if (!_weaponPools.ContainsKey(card))
        {
            _weaponPools.Add(card, new ObjectPool(card.weaponPrefab));
        }

        return _weaponPools[card].Get().GetComponent<Weapon>();
    }

    public void ReleaseWeapon(WeaponCard card, Transform obj)
    {
        _weaponPools[card].Release(obj);
    }
}