using UnityEngine;

public interface ISpaceshipInput
{
    void Init(Transform ownerTransform);
    float GetDeltaRotation();
    float GetDeltaMovement();
    bool IsFire();
}