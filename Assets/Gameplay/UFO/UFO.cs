using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField] private SpaceshipGun _gun;
    [SerializeField] private float _updateGunAimingRate = .2f;

    private UFOSpawner _spawner;
    private UFOMovement _movement;
    private GameMode _gameMode;

    private void Awake()
    {
        _movement = GetComponent<UFOMovement>();

        _gameMode = FindObjectOfType<GameMode>();
    }

    public void Init(UFOSpawner spawner, Vector2 movementDir)
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
                Destroy(gameObject);
                _gameMode.AddScoreForUFO();
            }
        }

        if (collision.TryGetComponent<Asteroid>(out var asteroid))
        {
            Destroy(gameObject);
        }
    }
}