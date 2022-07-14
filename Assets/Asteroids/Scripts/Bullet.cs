using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Owner { get; private set; }

    private Vector2 _velocity;
    private Rigidbody2D _rb;
    private PoolObject _poolObj;
    private Vector2 _prevPos;
    private Vector2 _dir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 position, Vector2 velocity, GameObject owner)
    {
        _velocity = velocity;
        _prevPos = transform.position;

        transform.position = position;
        Owner = owner;

        if (_poolObj == null)
            _poolObj = GetComponent<PoolObject>();
    }

    private void FixedUpdate()
    {
        _rb.position += _velocity * Time.fixedDeltaTime;

        UpdateRotation();

        _prevPos = transform.position;
    }

    private void UpdateRotation()
    {
        Vector2 pos = transform.position;
        _dir = (_prevPos - pos).normalized;

        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        angle += 90f;

        transform.eulerAngles = new Vector3(0, 0, angle);
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
