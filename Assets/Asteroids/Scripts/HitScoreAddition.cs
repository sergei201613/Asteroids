using UnityEngine;

public class HitScoreAddition : MonoBehaviour
{
    [SerializeField] ScoreAddingEffect _scoreAddingEffect;

    private GameMode _gameMode;

    private void Awake()
    {
        _gameMode = FindObjectOfType<GameMode>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Asteroid>(out var asteroid))
        {
            int scoreAdded = _gameMode.AddScoreForAsteroid(asteroid);

            Instantiate(_scoreAddingEffect, transform.position, 
                Quaternion.identity).Init(scoreAdded);
        }

        if (collision.TryGetComponent<Ufo>(out var ufo))
        {
            int scoreAdded = _gameMode.AddScoreForUfo();

            Instantiate(_scoreAddingEffect, transform.position, 
                Quaternion.identity).Init(scoreAdded);
        }
    }
}