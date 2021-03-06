using UnityEngine;

namespace TeaGames.Asteroids
{
    public class UfoInput : MonoBehaviour, ISpaceshipInput
    {
        [SerializeField] private float _minFireDelay = 2f;
        [SerializeField] private float _maxFireDelay = 5f;

        private float _fireDelay;
        private float _lastTimeFired;
        private FreeFlightGameMode _gameMode;

        private void Awake()
        {
            _gameMode = FindObjectOfType<FreeFlightGameMode>();

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

        public bool IsFire()
        {
            if (Time.timeSinceLevelLoad > _lastTimeFired + _fireDelay 
                && _gameMode.Player != null)
            {
                _fireDelay = Random.Range(_minFireDelay, _maxFireDelay);
                _lastTimeFired = Time.timeSinceLevelLoad;

                return true;
            }

            return false;
        }

        public void Init(Transform ownerTransform)
        {
        }
    }
}