using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_audio.isPlaying)
            Destroy(gameObject);
    }
}