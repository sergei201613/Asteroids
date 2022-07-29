using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Owner { get; private set; }

    private Vector2 _velocity;
    private Rigidbody2D _rb;
    private PoolObject _poolObj;
    private Vector2 _dir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 position, Vector2 velocity, GameObject owner)
    {
        Owner = owner;
        transform.position = position;

        _velocity = velocity;
        _dir = _velocity.normalized;

        if (_poolObj == null)
            _poolObj = GetComponent<PoolObject>();

        LookAtDir(_dir);
    }

    private void FixedUpdate()
    {
        _rb.position += _velocity * Time.fixedDeltaTime;

        UpdateRotation();
    }

    private void UpdateRotation()
    {
        LookAtDir(-_velocity.normalized);
    }

    private void LookAtDir(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 90f;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Asteroid>(out _))
        {
            if (_poolObj != null)
                _poolObj.ReturnToPool();
        } 
    }
}
