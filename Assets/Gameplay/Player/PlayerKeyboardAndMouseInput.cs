using UnityEngine;

public class PlayerKeyboardAndMouseInput : MonoBehaviour, ISpaceshipInput
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public float GetDeltaMovement()
    {
        return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(1)) ? 1 : 0;
    }

    public float GetDeltaRotation()
    {
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mouseWorldPosition - transform.position;

        return Mathf.Clamp(Vector3.Dot(transform.right, dir.normalized) * -4, -1, 1);
    }

    public bool IsFireKeyDown()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
    }
}