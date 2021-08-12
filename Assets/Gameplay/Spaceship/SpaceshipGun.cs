using UnityEngine;

public class SpaceshipGun : MonoBehaviour
{
    [Tooltip("Shots per second.")]
    [SerializeField] private float _fireRate = 3f;
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _owner;
    [SerializeField] private int _bulletPoolSize = 10;
    [SerializeField] private AudioSource _fireSound;

    private ISpaceshipInput _input;
    private float _lastTimeFired;
    private float MinDelayBetweenFires => (1 / _fireRate);
    private bool CanFire => Time.timeSinceLevelLoad > _lastTimeFired + MinDelayBetweenFires;
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
        if (_input.IsFireKeyDown() && CanFire)
        {
            Fire();
            _lastTimeFired = Time.timeSinceLevelLoad;
        }
    }

    private void Fire()
    {
        var bulletObj = _bulletPool.GetObject();

        if (bulletObj.TryGetComponent<Bullet>(out var bullet))
            bullet.Init(transform.position, _bulletSpeed * transform.up, _owner);

        _fireSound.Play();
    }
}