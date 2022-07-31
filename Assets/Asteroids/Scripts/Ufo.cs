using TeaGames.MonoBehaviourExtensions;
using TeaGames.ServiceLocator;
using UnityEngine;

namespace TeaGames.Asteroids
{
    public class Ufo : MonoBehaviour
    {
        [SerializeField] private SpaceshipGun _gun;
        [SerializeField] private float _updateGunAimingRate = .2f;
        [SerializeField] private GameObject _explosionSoundPrefab;

        private UfoSpawner _spawner;
        private UfoMovement _movement;
        private FreeFlightGameMode _gameMode;

        private void Awake()
        {
            _movement = this.RequireComponent<UfoMovement>();
            _gameMode = SceneServices.Get<FreeFlightGameMode>();
        }

        public void Init(UfoSpawner spawner, Vector2 movementDir)
        {
            _spawner = spawner;
            _movement.Init(movementDir);

            InvokeRepeating(nameof(UpdateGunAiming), _updateGunAimingRate, _updateGunAimingRate);
        }

        private void UpdateGunAiming()
        {
            if (_gameMode.Player == null) 
                return;

            Vector3 dir = _gameMode.Player.transform.position - transform.position;
            float degrees = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _gun.transform.rotation = Quaternion.AngleAxis(degrees - 90f, Vector3.forward);
        }

        private void OnDestroy()
        {
            _spawner.RemoveUFO(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Bullet>(out var bullet))
            {
                if (bullet.Owner != gameObject)
                {
                    Instantiate(_explosionSoundPrefab);
                    
                    Destroy(gameObject);
                }
            }
        }
    }
}