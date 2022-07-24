using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    public Vector2 Force { get; set; }

    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _maxMovementSpeed = 5f;
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _forceDeceleration = 5f;

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
        HandleForce();
    }

    private void HandleForce()
    {
        Force = Vector2.Lerp(Force, Vector2.zero, _forceDeceleration 
            * Time.fixedDeltaTime);
    }

    private void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        transform.Rotate(new Vector3(0, 0, 1), _input.GetDeltaRotation() 
            * _rotationSpeed * Time.deltaTime);
    }

    private void HandleMovement()
    {
        var deltaTime = Time.fixedDeltaTime;

        Vector2 forwardDir = new(transform.up.x, transform.up.y);
        Vector2 deltaMov = _input.GetDeltaMovement() * deltaTime
            * forwardDir;

        _velocity += deltaMov * _acceleration;
        _velocity = Vector2.ClampMagnitude(_velocity, _maxMovementSpeed);
        _velocity += Force * deltaTime;

        _rb.position += _velocity * deltaTime;
    }
}