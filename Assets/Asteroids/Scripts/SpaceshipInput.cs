using UnityEngine;

namespace TeaGames.Asteroids
{
    public class SpaceshipInput : MonoBehaviour, ISpaceshipInput
    {
        private ISpaceshipInput _input;

        private void Awake()
        {
            Init(transform);
        }

        public ISpaceshipInput SetInput(ISpaceshipInput input)
        {
            _input = input;
            return this;
        }

        public void Init(Transform ownerTransform)
        {
            _input.Init(ownerTransform);
        }

        public float GetDeltaMovement()
        {
            return _input.GetDeltaMovement();
        }

        public float GetDeltaRotation()
        {
            return _input.GetDeltaRotation();
        }

        public bool IsFire()
        {
            return _input.IsFire();
        }
    }
}
