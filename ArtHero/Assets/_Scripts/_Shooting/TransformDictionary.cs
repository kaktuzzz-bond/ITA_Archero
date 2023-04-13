using System;
using UnityEngine;

[Serializable]
public struct TransformDictionary
{
    [SerializeField]
    private Transform _prefab;
    [SerializeField]
    private int _instancesInScene;

    public Transform prefab
    {
        get
        {
            return _prefab;
        }
    }
    public int InstancesInScene
    {
        get
        {
            return _instancesInScene;
        }
    }
}
