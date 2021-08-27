using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private AsteroidSizes _sizes;
    [SerializeField] private GameObject _explosionSoundPrefab;

    private AsteroidType _type;
    private Vector2 _velocity;
    private Rigidbody2D _rb;
    private AsteroidSpawner _spawner;
    private PoolObject _poolObj;
    private GameMode _gameMode;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _gameMode = FindObjectOfType<GameMode>();
    }

    public void Init(AsteroidType type, Vector2 position, Vector2 velocity, AsteroidSpawner spawner)
    {
        _type = type;
        transform.position = position;
        _velocity = velocity;
        _spawner = spawner;

        float scale = 1;

        switch (_type)
        {
            case AsteroidType.Large:
                scale = _sizes.AsteroidLargeScale;
                break;
            case AsteroidType.Medium:
                scale = _sizes.AsteroidMediumScale;
                break;
            case AsteroidType.Small:
                scale = _sizes.AsteroidSmallScale;
                break;
            default:
                break;
        }

        transform.localScale = new Vector3(scale, scale, scale);

        if (_poolObj == null) 
            _poolObj = GetComponent<PoolObject>();
    }

    private void OnDisable()
    {
        if (_spawner == null) return;

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

            switch (_type)
            {
                case AsteroidType.Large:
                    _spawner.SpawnPieces(AsteroidType.Medium, transform.position, _velocity.normalized);
                    break;
                case AsteroidType.Medium:
                    _spawner.SpawnPieces(AsteroidType.Small, transform.position, _velocity.normalized);
                    break;
                case AsteroidType.Small:
                    break;
                default:
                    break;
            }

            _gameMode.AddScoreForAsteroid(_type);

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