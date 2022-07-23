using UnityEngine;

public class PlayerKeyboardAndMouseInput : ISpaceshipInput
{
    private Camera _camera;
    private Transform _owner;

    public void Init(Transform owner)
    {
        _camera = Camera.main;
        _owner = owner;
    }

    public float GetDeltaMovement()
    {
        return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(1)) ? 1 : 0;
    }

    public float GetDeltaRotation()
    {
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mouseWorldPosition - _owner.position;

        return Mathf.Clamp(Vector3.Dot(_owner.right, dir.normalized) * -4, -1, 1);
    }

    public bool IsFire()
    {
        return Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
    }
}