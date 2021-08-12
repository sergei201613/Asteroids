using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private PlayerLives _lives;

    private GameMode _gameMode;

    private void Awake()
    {
        _gameMode = FindObjectOfType<GameMode>();
    }

    private void OnEnable()
    {
        _gameMode.ScoreUpdated += OnScoreUpdated;
        _gameMode.GameOvered += OnGameOvered;
    }

    private void OnDisable()
    {
        _gameMode.ScoreUpdated -= OnScoreUpdated;
        _gameMode.GameOvered -= OnGameOvered;
    }

    private void OnGameOvered(int lives)
    {
        _lives.Refresh(lives);
    }

    private void OnScoreUpdated(int score)
    {
        _scoreText.text = score.ToString();
    }
}