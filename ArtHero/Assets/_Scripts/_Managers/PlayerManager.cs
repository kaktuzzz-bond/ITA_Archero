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

    public WeaponCard WeaponCard => weaponCard;

    public Transform Player { get; private set; }

    private void CreatePlayerIn(Vector3 origin, int width, int height)
    {
        Vector3 position = origin + new Vector3(width * 0.5f, 0.5f, 0);

        Player = Instantiate(playerPrefab, position, Quaternion.identity, transform);

        Observer.Instance.OnPlayerCreatedNotify(Player);
    }

    public void ChangeWeapon(WeaponCard card) => weaponCard = card;

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