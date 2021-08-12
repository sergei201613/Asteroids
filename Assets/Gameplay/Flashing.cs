using UnityEngine;

[DisallowMultipleComponent]
public class Flashing : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private float _repeatRate;

    public void Init(SpriteRenderer sprite, float repeatRate)
    {
        _repeatRate = repeatRate;
        _sprite = sprite;

        InvokeRepeating(nameof(Flash), 0f, _repeatRate);
    }

    private void Flash()
    {
        _sprite.enabled = !_sprite.enabled;
    }

    private void OnDestroy()
    {
        _sprite.enabled = true;
    }
}