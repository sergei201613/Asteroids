using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Stack<GameObject> _pool = new Stack<GameObject>();
    private GameObject _prefab;
    private Transform _parent;

    public Pool(GameObject prefab, int size, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < size; i++)
        {
            var obj = CreateObject();
            AddObject(obj);
        }
    }

    public GameObject GetObject()
    {
        GameObject freeObj;

        if (_pool.Count > 0)
        {
            freeObj = _pool.Pop();
            freeObj.SetActive(true);
        }
        else
        {
            freeObj = CreateObject();
        }

        return freeObj;
    }

    public void AddObject(GameObject obj)
    {
        obj.SetActive(false);

        if (!obj.TryGetComponent<PoolObject>(out var poolObj))
        {
            obj.AddComponent<PoolObject>().Init(this);
        }

        if (!_pool.Contains(obj))
            _pool.Push(obj);
    }

    private GameObject CreateObject()
    {
        GameObject obj;

        if (_parent != null)
            obj = Object.Instantiate(_prefab, _parent);
        else
            obj = Object.Instantiate(_prefab);

        obj.AddComponent<PoolObject>().Init(this);

        return obj;
    }
}