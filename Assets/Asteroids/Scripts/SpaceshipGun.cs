using UnityEngine;
using System.Collections.Generic;

public class SpaceshipGun : MonoBehaviour
{
    [Tooltip("Shots per second.")]
    [SerializeField] private float _fireRate = 3f;
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private List<Transform> _bulletSpawnPositions = new();
    [SerializeField] private GameObject _owner;
    [SerializeField] private int _bulletPoolSize = 10;
    [SerializeField] private AudioSource _fireSound;

    private ISpaceshipInput _input;
    private float _lastTimeFired;
    private int _fires = 0;
    private float MinDelayBetweenFires => (1 / _fireRate);
    private bool CanFire => Time.timeSinceLevelLoad > _lastTimeFired 
        + MinDelayBetweenFires;
    private Pool _bulletPool;

    private void Awake()
    {
        _input = GetComponent<ISpaceshipInput>();

        // When fire first time, we dont need wait.
        _lastTimeFired = -MinDelayBetweenFires;
        _bulletPool = new Pool(_bulletPrefab, _bulletPoolSize);
    }

    private void Update()
    {
        if (_input.IsFire() && CanFire)
        {
            Fire();
            _lastTimeFired = Time.timeSinceLevelLoad;
        }
    }

    private void Fire()
    {
        var bulletObj = _bulletPool.GetObject();

        if (bulletObj.TryGetComponent<Bullet>(out var bullet))
        {
            var pos = GetNextBulletPosition();
            bullet.Init(pos, _bulletSpeed * transform.up, _owner);
        }

        _fireSound.Play();
        _fires++;
    }

    private Vector2 GetNextBulletPosition()
    {
        if (_bulletSpawnPositions.Count == 0)
            return transform.position;

        int idx = _fires % _bulletSpawnPositions.Count;
        return _bulletSpawnPositions[idx].position;
    }
}
