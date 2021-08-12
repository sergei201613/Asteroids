using UnityEngine;
using UnityEngine.SceneManagement;
using MonoBehaviourExtensions;

public class GameMode : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _field;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private HUD _hud;
    [SerializeField] private MainMenu _menu;
    [SerializeField] private UFOSpawner _ufoSpawner;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] PlayerInputData _inputData;
    [SerializeField] private int _lives = 4;

    public int Lives => _lives;
    public int Score => _score;
    public GameState GameState => _gameState;
    public Player Player => _player;

    public event System.Action<int> GameOvered;
    public event System.Action<int> ScoreUpdated;
    
    private Player _player;
    private int _score = 0;
    private GameState _gameState = GameState.Playing;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_inputData.PauseKey))
            TogglePause();
    }

    public void GameOver()
    {
        this.Delay(2f, () =>
        {
            if (_lives > 0)
                RespawnPlayer();
            else
                SceneManager.LoadScene("MainMenu");
        });

        GameOvered?.Invoke(--_lives);
    }

    public void TogglePause()
    {
        bool isPaused = _gameState == GameState.Playing;

        Time.timeScale = isPaused ? 0 : 1;
        _gameState = isPaused ? GameState.Paused : GameState.Playing;

        _menu.gameObject.SetActive(isPaused);
    }

    public void AddScoreForAsteroid(AsteroidType type)
    {
        switch (type)
        {
            case AsteroidType.Large:
                AddScore(20);
                break;
            case AsteroidType.Medium:
                AddScore(50);
                break;
            case AsteroidType.Small:
                AddScore(100);
                break;
            default:
                break;
        }
    }

    public void AddScoreForUFO()
    {
        AddScore(200);
    }

    private void AddScore(int value)
    {
        if (value < 1) return;

        _score = Mathf.Clamp(_score + value, 0, int.MaxValue);

        ScoreUpdated?.Invoke(_score);
    }

    private void RespawnPlayer()
    {
        Vector2 position = Vector2.zero;

        position.x = _field.bounds.center.x;
        position.y = _field.bounds.center.y;

        _player = Instantiate(_playerPrefab, position, Quaternion.identity);
    }
}