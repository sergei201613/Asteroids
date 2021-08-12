using UnityEngine;
using MonoBehaviourExtensions;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject _asteroidPrefab;
    [SerializeField] float _minAsteroidSpeed = 4f;
    [SerializeField] float _maxAsteroidSpeed = 6f;
    [SerializeField] int _asteroiPoolSize = 15;
    [SerializeField] float _asteroidSideOffset = 5f;
    [SerializeField] private BoxCollider2D _fieldCollider;

    private int _asteroidsSpawnCount = 2;
    private float _waveDelay = 2;
    private Pool _pool;
    private List<Asteroid> _asteroids = new List<Asteroid>();

    private void Start()
    {
        _pool = new Pool(_asteroidPrefab, _asteroiPoolSize);

        this.Delay(_waveDelay, () => { SpawnWave(); });
    }

    public void Remove(Asteroid asteroid)
    {
        _asteroids.Remove(asteroid);

        if (_asteroids.Count == 0)
            this.Delay(_waveDelay, () => { SpawnWave(); });
    }

    public void SpawnPieces(AsteroidType type, Vector2 position, Vector2 dir)
    {
        GameObject piece1 = _pool.GetObject();
        GameObject piece2 = _pool.GetObject();

        Vector2 dir1 = dir;
        Vector2 dir2 = dir;

        float degrees1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
        float degrees2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;

        degrees1 += 45;
        degrees2 -= 45;

        degrees1 *= Mathf.Deg2Rad;
        degrees2 *= Mathf.Deg2Rad;

        dir1.x = Mathf.Cos(degrees1);
        dir1.y = Mathf.Sin(degrees1);

        dir2.x = Mathf.Cos(degrees2);
        dir2.y = Mathf.Sin(degrees2);

        piece1.GetComponent<PoolObject>().IgnorePoolArea = true;
        piece2.GetComponent<PoolObject>().IgnorePoolArea = true;

        float speed = Random.Range(_minAsteroidSpeed, _maxAsteroidSpeed);

        if (piece1.TryGetComponent<Asteroid>(out var asteroid1))
        {
            asteroid1.Init(type, position, dir1 * speed, this);
            _asteroids.Add(asteroid1);
        }

        if (piece2.TryGetComponent<Asteroid>(out var asteroid2))
        {
            asteroid2.Init(type, position, dir2 * speed, this);
            _asteroids.Add(asteroid2);
        }
    }

    private void Spawn()
    {
        GameObject asteroidObj = _pool.GetObject();

        int side = Random.Range(0, 4);
        Vector2 position = _fieldCollider.bounds.min;

        float minPosX = _fieldCollider.bounds.min.x + _asteroidSideOffset;
        float maxPosX = _fieldCollider.bounds.max.x - _asteroidSideOffset;
        float minPosY = _fieldCollider.bounds.min.y + _asteroidSideOffset;
        float maxPosY = _fieldCollider.bounds.max.y - _asteroidSideOffset;

        if (side == 0)
        {
            position.x = Random.Range(minPosX, maxPosX);
            position.y = minPosY;
        }
        else if (side == 1)
        {
            position.y = Random.Range(minPosY, maxPosY);
            position.x = minPosX;
        }
        else if (side == 2)
        {
            position.y = Random.Range(minPosY, maxPosY);
            position.x = maxPosX;
        }
        else if (side == 3)
        {
            position.x = Random.Range(minPosX, maxPosX);
            position.y = maxPosY;
        }

        Vector2 dir;
        dir.x = Random.Range(-1f, 1f);
        dir.y = Random.Range(-1f, 1f);
        dir.Normalize();

        Vector2 velocity = dir * Random.Range(_minAsteroidSpeed, _maxAsteroidSpeed);

        asteroidObj.GetComponent<PoolObject>().IgnorePoolArea = true;

        if (asteroidObj.TryGetComponent<Asteroid>(out var asteroid))
        {
            asteroid.Init(AsteroidType.Large, position, velocity, this);
            _asteroids.Add(asteroid);
        }
    }

    private void SpawnWave()
    {
        for (int i = 0; i < _asteroidsSpawnCount; i++)
            this.Delay(i, () => { Spawn(); });

        _asteroidsSpawnCount++;
    }
}