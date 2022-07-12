using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public event System.Action ReturnedToPool;

    public bool IgnorePoolArea = false;

    private Pool _pool;

    public void Init(Pool pool)
    {
        _pool = pool;
    }

    public void ReturnToPool()
    {
        _pool.AddObject(gameObject);
        ReturnedToPool?.Invoke();
    }
}