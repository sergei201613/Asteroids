using UnityEngine;
using TeaGames.MonoBehaviourExtensions;
using TeaGames.Asteroids;

// This script adds a component to the object,
// other components can get it, so the component must be added before that.
[DefaultExecutionOrder(-100)]
public class Player : MonoBehaviour
{
    [SerializeField] private float _invulnerableFlashingRate = 0.5f;
    [SerializeField] private SpriteRenderer[] _sprites;
    [SerializeField] private PlayerInputData _inputData;
    [SerializeField] private GameObject _explosionSoundPrefab;

    private FreeFlightGameMode _gameMode;
    private SpaceshipInput _input;
    private bool _isInvulnerable = true;

    private void Awake()
    {
        _gameMode = FindObjectOfType<FreeFlightGameMode>();
        _input = GetComponent<SpaceshipInput>();

        UpdateInput(_inputData.GetInputType());

        gameObject.AddComponent<Flashing>().Init(_sprites, _invulnerableFlashingRate);

        this.Delay(3f, () =>
        {
            _isInvulnerable = false;

            if (TryGetComponent<Flashing>(out var flashing))
            {
                Destroy(flashing);
            }
        });
    }

    private void OnEnable()
    {
        _inputData.InputChanged += UpdateInput;
        _gameMode.PlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        _inputData.InputChanged -= UpdateInput;
        _gameMode.PlayerDied -= OnPlayerDied;
    }

    private void UpdateInput(InputType type)
    {
        var input = _inputData.GetInput(type);
        input.Init(transform);
        _input.SetInput(input);
    }

    private void OnPlayerDied(int lives, int score, bool isRecord)
    {
        Instantiate(_explosionSoundPrefab);

        _isInvulnerable = true;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Asteroid>(out _))
        {
            if (_isInvulnerable) return;

            _gameMode.GameOver();
        }

        if (collision.TryGetComponent<Bullet>(out var bullet))
        {
            if (_isInvulnerable) return;

            if (bullet.Owner != gameObject)
                _gameMode.GameOver();
        }

        if (collision.TryGetComponent<Ufo>(out _))
        {
            if (_isInvulnerable) return;

            _gameMode.GameOver();
        }
    }
}