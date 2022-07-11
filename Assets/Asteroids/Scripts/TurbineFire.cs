using UnityEngine;

public class TurbineFire : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    private ISpaceshipInput _input;
    private Animator _animator;
    private readonly int _isOnAnimParam = Animator.StringToHash("IsOn");

    private void Awake()
    {
        _input = GetComponentInParent<ISpaceshipInput>()
            ?? GetComponent<ISpaceshipInput>();

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(_isOnAnimParam, _input.GetDeltaMovement() > float.Epsilon);
    }
}