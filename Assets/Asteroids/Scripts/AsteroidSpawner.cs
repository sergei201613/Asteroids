using UnityEngine;
using MonoBehaviourExtensions;
using System.Collections.Generic;
using UnityEngine.Serialization;
using System;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [FormerlySerializedAs("_asteroidPrefab")]
    [SerializeField] Asteroids _prefabs;
    [SerializeField] float _minAsteroidSpeed = 4f;
    [SerializeField] float _maxAsteroidSpeed = 6f;
    [SerializeField] float _asteroidSideOffset = 5f;
    [SerializeField] private BoxCollider2D _fieldCollider;
    [SerializeField] private int _asteroidsSpawnCount = 2;
    [SerializeField] private float _waveDelay = 2;

    private const int AsteroidPoolsCapacity = 4;
    private const int AsteroidsCapacity = 16;

    private readonly List<Pool> _bigAsteroidPools = 
        new(AsteroidPoolsCapacity);
    private readonly List<Pool> _mediumAsteroidPools = 
        new(AsteroidPoolsCapacity);
    private readonly List<Pool> _smallAsteroidPools = 
        new(AsteroidPoolsCapacity);

    private readonly List<Asteroid> _asteroids = new(AsteroidsCapacity);

    private void Awake()
    {
        CreatePools();

        this.Delay(_waveDelay, () => 
        { 
            SpawnWave(); 
        });
    }

    private void CreatePools()
    {
        foreach (var prefab in _prefabs.BigAsteroids)
            _bigAsteroidPools.Add(new Pool(prefab.gameObject, 2));

        foreach (var prefab in _prefabs.MediumAsteroids)
            _mediumAsteroidPools.Add(new Pool(prefab.gameObject, 2));

        foreach (var prefab in _prefabs.SmallAsteroids)
            _smallAsteroidPools.Add(new Pool(prefab.gameObject, 2));
    }

    public void Remove(Asteroid asteroid)
    {
        _asteroids.Remove(asteroid);

        if (_asteroids.Count == 0)
            this.Delay(_waveDelay, () => { SpawnWave(); });
    }

    public void SpawnPieces(AsteroidSize size, Vector2 position, Vector2 dir)
    {
        GameObject piece1 = GetRandomPool(size).GetObject();
        GameObject piece2 = GetRandomPool(size).GetObject();

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
            // TODO: Change type
            asteroid1.Init(position, dir1 * speed, this);
            _asteroids.Add(asteroid1);
        }

        if (piece2.TryGetComponent<Asteroid>(out var asteroid2))
        {
            asteroid2.Init(position, dir2 * speed, this);
            _asteroids.Add(asteroid2);
        }
    }

    private void Spawn()
    {
        GameObject asteroidObj = GetRandomPool(AsteroidSize.Big).GetObject();

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
            asteroid.Init(position, velocity, this);
            _asteroids.Add(asteroid);
        }
    }

    private void SpawnWave()
    {
        for (int i = 0; i < _asteroidsSpawnCount; i++)
            this.Delay(i, () => { Spawn(); });

        _asteroidsSpawnCount++;
    }

    private Pool GetRandomPool(AsteroidSize type)
    {
        int idx;

        switch (type)
        {
            case AsteroidSize.Big:
                idx = Random.Range(0, _bigAsteroidPools.Count);
                return _bigAsteroidPools[idx];
            case AsteroidSize.Medium:
                idx = Random.Range(0, _mediumAsteroidPools.Count);
                return _mediumAsteroidPools[idx];
            case AsteroidSize.Small:
                idx = Random.Range(0, _smallAsteroidPools.Count);
                return _smallAsteroidPools[idx];
            default:
                throw new ArgumentException("Can't find pool by type: " 
                    + type.ToString());
        }
    }
}