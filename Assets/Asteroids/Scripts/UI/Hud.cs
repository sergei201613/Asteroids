using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private PlayerLivesPanel _lives;
    [SerializeField] private Text _newRecordText;

    private GameMode _gameMode;

    private void Awake()
    {
        _gameMode = FindObjectOfType<GameMode>();
    }

    private void OnEnable()
    {
        _gameMode.ScoreUpdated += OnScoreUpdated;
        _gameMode.PlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        _gameMode.ScoreUpdated -= OnScoreUpdated;
        _gameMode.PlayerDied -= OnPlayerDied;
    }

    private void OnPlayerDied(int lives, int score, bool isRecord)
    {
        _lives.Refresh(lives);

        if (lives == 0 && isRecord)
        {
            _newRecordText.gameObject.SetActive(true);
            _newRecordText.text = "Новый рекорд: " + score + "!";
        }
    }

    private void OnScoreUpdated(int score)
    {
        _scoreText.text = score.ToString();
    }
}