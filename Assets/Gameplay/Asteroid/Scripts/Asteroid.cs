using UnityEngine;
using UnityEngine.Serialization;

public class Asteroid : MonoBehaviour
{
    [field: FormerlySerializedAs("<Type>k__BackingField")]
    [field: SerializeField]
    public AsteroidSize Size { get; private set; } = AsteroidSize.Big;
    public AsteroidSize Color { get; private set; } = AsteroidSize.Big;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private GameObject _explosionSoundPrefab;

    private Vector2 _velocity;
    private Rigidbody2D _rb;
    private AsteroidSpawner _spawner;
    private PoolObject _poolObj;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 position, Vector2 velocity, 
        AsteroidSpawner spawner)
    {
        transform.position = position;

        _velocity = velocity;
        _spawner = spawner;

        if (_poolObj == null) 
            _poolObj = GetComponent<PoolObject>();
    }

    private void OnDisable()
    {
        if (_spawner == null) 
            return;

        _spawner.Remove(this);
    }

    private void FixedUpdate()
    {
        _rb.position += _velocity * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out var bullet))
        {
            if (_poolObj != null)
                _poolObj.ReturnToPool();

            switch (Size)
            {
                case AsteroidSize.Big:
                    _spawner.SpawnPieces(AsteroidSize.Medium, transform.position, 
                        _velocity.normalized);
                    break;
                case AsteroidSize.Medium:
                    _spawner.SpawnPieces(AsteroidSize.Small, transform.position, 
                        _velocity.normalized);
                    break;
                case AsteroidSize.Small:
                    break;
                default:
                    break;
            }

            Instantiate(_explosionSoundPrefab);
        }

        if (collision.TryGetComponent<Player>(out var player))
        {
            Instantiate(_explosionSoundPrefab);
            _poolObj.ReturnToPool();
        }

        if (collision.TryGetComponent<UFO>(out var ufo))
        {
            Instantiate(_explosionSoundPrefab);
            _poolObj.ReturnToPool();
        }
    }
}