using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _maxMovementSpeed = 5f;
    [SerializeField] private float _acceleration = 10f;

    private ISpaceshipInput _input;
    private Rigidbody2D _rb;
    private Vector2 _velocity = Vector2.zero;

    private void Awake()
    {
        _input = GetComponent<ISpaceshipInput>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        transform.Rotate(new Vector3(0, 0, 1), _input.GetDeltaRotation() * _rotationSpeed * Time.deltaTime);
    }

    private void HandleMovement()
    {
        Vector2 forwardDir = new Vector2(transform.up.x, transform.up.y);
        Vector2 deltaMov = forwardDir * _input.GetDeltaMovement() * Time.fixedDeltaTime;

        _velocity += deltaMov * _acceleration;
        _velocity = Vector2.ClampMagnitude(_velocity, _maxMovementSpeed);

        _rb.position += _velocity * Time.fixedDeltaTime;
    }
}