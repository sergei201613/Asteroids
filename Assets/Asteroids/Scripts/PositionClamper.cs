using UnityEngine;

public class PositionClamper : MonoBehaviour
{
    [SerializeField] private GameFieldData _data;

    private void LateUpdate()
    {
        var position = transform.position;

        position.x %= _data.MaxX;
        position.y %= _data.MaxY;

        if (position.x < 0) position.x = _data.MaxX - .1f;
        if (position.y < 0) position.y = _data.MaxY - .1f;

        transform.position = position;
    }
}