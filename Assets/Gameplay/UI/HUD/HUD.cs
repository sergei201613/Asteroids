using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private PlayerLives _lives;
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
            _newRecordText.text = "Новый рекорд: " + score + "!";
    }

    private void OnScoreUpdated(int score)
    {
        _scoreText.text = score.ToString();
    }
}