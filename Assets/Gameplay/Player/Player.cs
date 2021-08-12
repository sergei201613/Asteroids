using UnityEngine;
using MonoBehaviourExtensions;

// This script adds a component to the object,
// other components can get it, so the component must be added before that.
[DefaultExecutionOrder(-100)]
public class Player : MonoBehaviour
{
    [SerializeField] private float _invulnerableFlashingRate = 0.5f;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private PlayerInputData _inputData;

    private GameMode _gameMode;
    private bool _isInvulnerable = true;

    private void Awake()
    {
        _gameMode = FindObjectOfType<GameMode>();

        switch (_inputData.Type)
        {
            case InputType.Keyboard:
                gameObject.AddComponent<PlayerKeyboardInput>();
                break;
            case InputType.KeyboardAndMouse:
                gameObject.AddComponent<PlayerKeyboardAndMouseInput>();
                break;
            default:
                break;
        }

        gameObject.AddComponent<Flashing>().Init(_sprite, _invulnerableFlashingRate);

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
        _gameMode.GameOvered += OnGameOvered;
    }

    private void OnDisable()
    {
        _gameMode.GameOvered -= OnGameOvered;
    }

    private void OnGameOvered(int lives)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Asteroid>(out var asteroid))
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
    }
}