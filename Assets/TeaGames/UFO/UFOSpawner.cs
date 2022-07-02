using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField] private float _minSpawnDelay = 20f;
    [SerializeField] private float _maxSpawnDelay = 40f;
    [SerializeField] float _horizontalOffset = 2f;
    [SerializeField] float _verticalOffsetPercentage = 20f;
    [SerializeField] private UFO _ufoPrefab;
    [SerializeField] private BoxCollider2D _fieldCollider;

    private UFO _ufo;
    private float _lastTimeUfoDestroyed;
    private float _spawnDelay;

    private void Awake()
    {
        _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
        _lastTimeUfoDestroyed = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > _lastTimeUfoDestroyed + _spawnDelay && _ufo == null)
        {
            Spawn();

            _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
        }
    }

    public void RemoveUFO(UFO ufo)
    {
        if (ufo == _ufo)
        {
            _ufo = null;
            _lastTimeUfoDestroyed = Time.timeSinceLevelLoad;
        }
    }

    private void Spawn()
    {
        int side = Random.Range(0, 2);
        Vector2 position = _fieldCollider.bounds.min;

        float minPosX = _fieldCollider.bounds.min.x + _horizontalOffset;
        float maxPosX = _fieldCollider.bounds.max.x - _horizontalOffset;

        float verticalOffset = _fieldCollider.bounds.size.y / 100 * _verticalOffsetPercentage;
        float minPosY = _fieldCollider.bounds.min.y + verticalOffset;
        float maxPosY = _fieldCollider.bounds.max.y - verticalOffset;

        Vector2 ufoMovDir;

        if (side == 0)
        {
            position.y = Random.Range(minPosY, maxPosY);
            position.x = minPosX;
            ufoMovDir = Vector2.right;
        }
        else
        {
            position.y = Random.Range(minPosY, maxPosY);
            position.x = maxPosX;
            ufoMovDir = Vector2.left;
        }

        _ufo = Instantiate(_ufoPrefab, position, Quaternion.identity);
        _ufo.Init(this, ufoMovDir);
    }
}