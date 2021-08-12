using UnityEngine;

/// <summary>
/// If the object leaves this zone, it will be automatically destroyed or returned to the pool.
/// </summary>
public class PoolArea : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PoolObject>(out var poolObject))
        {
            if (!poolObject.IgnorePoolArea)
                poolObject.ReturnToPool();
        }
        else if (collision.TryGetComponent<Player>(out var player))
        {
            return; // TODO: shit.
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}