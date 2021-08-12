using UnityEngine;

public class UFOInput : MonoBehaviour, ISpaceshipInput
{
    [SerializeField] private float _minFireDelay = 2f;
    [SerializeField] private float _maxFireDelay = 5f;

    private float _fireDelay;
    private float _lastTimeFired;
    private GameMode _gameMode;

    private void Awake()
    {
        _gameMode = FindObjectOfType<GameMode>();

        _fireDelay = Random.Range(_minFireDelay, _maxFireDelay);
        _lastTimeFired = Time.timeSinceLevelLoad;
    }

    public float GetDeltaMovement()
    {
        return 0f;
    }

    public float GetDeltaRotation()
    {
        return 0f;
    }

    public bool IsFireKeyDown()
    {
        if (Time.timeSinceLevelLoad > _lastTimeFired + _fireDelay && _gameMode.Player != null)
        {
            _fireDelay = Random.Range(_minFireDelay, _maxFireDelay);
            _lastTimeFired = Time.timeSinceLevelLoad;

            return true;
        }

        return false;
    }
}