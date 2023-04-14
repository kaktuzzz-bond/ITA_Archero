using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Setup")]
    [SerializeField]
    private Transform playerPrefab;

    [SerializeField] private WeaponCard weaponCard;

    private readonly Dictionary<string, ObjectPool<Weapon>> _weaponPools = new();
    public int MaxPlayerHealth { get; private set; } = 100;
    

    public Transform Player { get; private set; }
    
    public Weapon GetWeapon()
    {
        if (weaponCard == null) throw new NullReferenceException("Card is empty");

        string cardID = weaponCard.id;

        if (_weaponPools.TryAdd(cardID, new ObjectPool<Weapon>()))
        {
            _weaponPools[cardID].Init(weaponCard.weaponPrefab);
        }

        return _weaponPools[cardID].Get();
    }

    public void ReleaseWeapon(Weapon weapon)
    {
        _weaponPools[weapon.card.id].Release(weapon);
    }

    private void CreatePlayerIn(Vector3 origin, int width, int height)
    {
        Vector3 position = origin + new Vector3(width * 0.5f, 0.5f, 0);

        Player = Instantiate(playerPrefab, position, Quaternion.identity, transform);

        Observer.Instance.OnPlayerCreatedNotify(Player);
    }

    #region ENABLE / DISABLE

    private void OnEnable()
    {
        Observer.Instance.OnMapGenerated += CreatePlayerIn;
    }

    private void OnDisable()
    {
        Observer.Instance.OnMapGenerated += CreatePlayerIn;
    }

    #endregion
}