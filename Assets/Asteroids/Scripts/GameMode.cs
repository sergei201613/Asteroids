using UnityEngine;
using UnityEngine.SceneManagement;
using MonoBehaviourExtensions;
using TeaGames.Asteroids;

public class GameMode : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _field;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Hud _hud;
    [SerializeField] private MainMenu _menu;
    [SerializeField] private UfoSpawner _ufoSpawner;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private PlayerInputData _inputData;
    [SerializeField] private int _lives = 4;

    public int Lives => _lives;
    public int Score => _score;
    public GameState GameState => _gameState;
    public Player Player => _player;

    public event System.Action<int, int, bool> PlayerDied;
    public event System.Action<int> ScoreUpdated;
    
    private Player _player;
    private int _score = 0;
    private GameState _gameState = GameState.Playing;

    private void Awake()
    {
        SpawnPlayer();
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
                SpawnPlayer();
            else
                SceneManager.LoadScene("MainMenu");
        });

        // TODO: Record to PlayerData.cs
        bool isRecord = !PlayerPrefs.HasKey("Record") || 
            _score > PlayerPrefs.GetInt("Record");

        SavePlayerData();

        if (isRecord && _lives == 1)
            PlayerPrefs.SetInt("Record", _score);

        PlayerDied?.Invoke(--_lives, _score, isRecord);
    }

    private void SavePlayerData()
    {
        _playerData.AddCoins(_score);
    }

    public void TogglePause()
    {
        bool isPaused = _gameState == GameState.Playing;

        Time.timeScale = isPaused ? 0 : 1;
        _gameState = isPaused ? GameState.Paused : GameState.Playing;

        _menu.gameObject.SetActive(isPaused);
    }

    public int AddScoreForAsteroid(Asteroid ast)
    {
        int score = 0;

        switch (ast.Size)
        {
            case AsteroidSize.Big:
                score = 20;
                break;
            case AsteroidSize.Medium:
                score = 50;
                break;
            case AsteroidSize.Small:
                score = 100;
                break;
            default:
                break;
        }

        if (ast.Color == AsteroidColor.Brown)
            score /= 4;

        AddScore(score);

        return score;
    }

    public int AddScoreForUfo()
    {
        int score = 200;
        AddScore(score);

        return score;
    }

    private void AddScore(int value)
    {
        if (value < 1) return;

        _score = Mathf.Clamp(_score + value, 0, int.MaxValue);

        ScoreUpdated?.Invoke(_score);
    }

    private void SpawnPlayer()
    {
        Vector2 position = Vector2.zero;

        position.x = _field.bounds.center.x;
        position.y = _field.bounds.center.y;

        var prefab = _playerData.CurrentSpaceship.Prefab;
        _player = Instantiate(prefab, position, Quaternion.identity);
    }
}