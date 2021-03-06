using UnityEngine;

public class UfoMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private Vector2 _dir;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 dir)
    {
        _dir = dir;
    }

    private void FixedUpdate()
    {
        _rb.position += _speed * Time.fixedDeltaTime * _dir;
    }
}