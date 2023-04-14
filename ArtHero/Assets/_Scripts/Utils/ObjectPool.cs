using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Transform _prefab;

    private Queue<T> _queue = new();

    public void Init(Transform t)
    {
        _prefab = t;
    }

    public T Get()
    {
        if (_queue.Count == 0)
        {
            Add();
        }

        return _queue.Dequeue();
    }

    private void Add(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            Object.Instantiate(_prefab).TryGetComponent(out T obj);

            obj.gameObject.SetActive(false);

            _queue.Enqueue(obj);
        }
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);

        _queue.Enqueue(obj);
    }
}