using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Owner { get; private set; }

    private Vector2 _velocity;
    private Rigidbody2D _rb;
    private PoolObject _poolObj;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 position, Vector2 velocity, GameObject owner)
    {
        _velocity = velocity;
        transform.position = position;
        Owner = owner;

        if (_poolObj == null)
            _poolObj = GetComponent<PoolObject>();
    }

    private void FixedUpdate()
    {
        _rb.position += _velocity * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Asteroid>(out var asteroid))
        {
            if (_poolObj != null)
                _poolObj.ReturnToPool();
        } 
    }
}
