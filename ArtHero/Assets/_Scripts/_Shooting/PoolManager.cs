using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField]
    private List<TransformDictionary> transforms;

    private List<List<Transform>> transformGenerator;

    private void Awake()
    {
        transformGenerator = new();

        for (int i = 0; i < transforms.Count; i++)
        {
            transformGenerator.Add(new());

            for (int j = 0; j < transforms[i].InstancesInScene; j++)
            {
                Add(transformGenerator[i], transforms[i].prefab);
            }
        }
    }

    public void Add(List<Transform> list, Transform @object)
    {
        var temp = Instantiate(@object, transform);
        //temp.OnEnd = Push;
        temp.gameObject.SetActive(false);
        list.Add(temp);
    }

    public Transform Pool(string bullet, Vector3 position, Quaternion rotaton)
    {
        Transform result = null;

        int type = transforms.FindIndex(b => b.prefab.ToString() == bullet);

        result = transformGenerator[type].FirstOrDefault(b => b.gameObject.activeSelf == false);

        if (result == null)
        {
            Add(transformGenerator[type], transforms[type].prefab);
            result = transformGenerator[type].Last();
        }

        result.transform.SetParent(null);
        result.transform.position = position;
        result.transform.rotation = rotaton;
        result.gameObject.SetActive(true);

        return result;
    }

    public void Push(Transform transform)
    {
        transform.gameObject.SetActive(false);
        //transform.body.velocity = Vector3.zero;
        transform.transform.position = Vector3.zero;
        transform.transform.rotation = Quaternion.identity;
        transform.transform.SetParent(this.transform);
    }
}
