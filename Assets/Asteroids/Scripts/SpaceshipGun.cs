using UnityEngine;
using System.Collections.Generic;

namespace TeaGames.Asteroids
{
    public class SpaceshipGun : MonoBehaviour
    {
        [field: SerializeField] 
        public bool FireForceEnabled { get; set; } = true;

        [SerializeField] private float _fireForce = 1f;
        [SerializeField] private float _fireRate = 3f;
        [SerializeField] private float _bulletSpeed = 20f;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private List<Transform> _bulletSpawnPositions = new();
        [SerializeField] private GameObject _owner;
        [SerializeField] private int _bulletPoolSize = 10;
        [SerializeField] private AudioSource _fireSound;

        private ISpaceshipInput _input;
        private float _lastTimeFired;
        private Pool _bulletPool;
        private SpaceshipMovement _movement;
        private int _fires = 0;

        private float FireCooldown => 1 / _fireRate;

        private float CooldownEndTime => _lastTimeFired + FireCooldown;

        private bool CanFire => Time.time > CooldownEndTime;

        private void Awake()
        {
            _input = GetComponent<ISpaceshipInput>();
            _movement = GetComponent<SpaceshipMovement>();

            _lastTimeFired = -FireCooldown;
            _bulletPool = new Pool(_bulletPrefab, _bulletPoolSize);
        }

        private void Update()
        {
            if (_input.IsFire() && CanFire)
            {
                Fire();
                _lastTimeFired = Time.time;
            }
        }

        private void Fire()
        {
            var bulletObj = _bulletPool.GetObject();

            if (bulletObj.TryGetComponent<Bullet>(out var bullet))
            {
                var position = GetNextBulletPosition();
                bullet.Init(position, _bulletSpeed * transform.up, _owner);
            }

            _fireSound.Play();
            _fires++;

            if (_movement && FireForceEnabled)
            {
                _movement.Force = _fireForce * -transform.up;
            }
        }

        private Vector2 GetNextBulletPosition()
        {
            if (_bulletSpawnPositions.Count == 0)
                return transform.position;

            int index = _fires % _bulletSpawnPositions.Count;
            return _bulletSpawnPositions[index].position;
        }
    }
}
