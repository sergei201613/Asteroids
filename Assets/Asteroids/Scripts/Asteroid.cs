using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TeaGames.Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        [field: FormerlySerializedAs("<Type>k__BackingField")]
        [field: SerializeField]
        public AsteroidSize Size { get; private set; } = AsteroidSize.Big;
        public AsteroidColor Color { get; private set; } = AsteroidColor.Grey;

        [FormerlySerializedAs("_sprite")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _greySprite;
        [SerializeField] private Sprite _brownSprite;
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
            AsteroidColor color, AsteroidSpawner spawner)
        {
            transform.position = position;
            _velocity = velocity;
            Color = color;
            _spawner = spawner;
            _spriteRenderer.sprite = GetSpriteByColor(color);

            if (_poolObj == null) 
                _poolObj = GetComponent<PoolObject>();
        }

        private Sprite GetSpriteByColor(AsteroidColor color)
        {
            return color switch
            {
                AsteroidColor.Grey => _greySprite,
                AsteroidColor.Brown => _brownSprite,
                _ => throw new ArgumentException("Not valid color!"),
            };
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
                        _spawner.SpawnPieces(this, AsteroidSize.Medium, 
                            _velocity.normalized);
                        break;
                    case AsteroidSize.Medium:
                        _spawner.SpawnPieces(this, AsteroidSize.Small, 
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

            if (collision.TryGetComponent<Ufo>(out var ufo))
            {
                Instantiate(_explosionSoundPrefab);
                _poolObj.ReturnToPool();
            }
        }
    }
}