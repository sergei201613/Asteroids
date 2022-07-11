using UnityEngine;

[DisallowMultipleComponent]
public class Flashing : MonoBehaviour
{
    private SpriteRenderer[] _sprites;
    private float _repeatRate;

    public void Init(SpriteRenderer[] sprites, float repeatRate)
    {
        _repeatRate = repeatRate;
        _sprites = sprites;

        InvokeRepeating(nameof(Flash), 0f, _repeatRate);
    }

    private void Flash()
    {
        foreach (var sprite in _sprites)
            sprite.enabled = !sprite.enabled;
    }

    private void OnDestroy()
    {
        foreach (var sprite in _sprites)
            sprite.enabled = true;
    }
}