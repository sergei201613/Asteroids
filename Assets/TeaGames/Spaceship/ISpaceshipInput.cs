using UnityEngine;

public interface ISpaceshipInput
{
    float GetDeltaRotation();
    float GetDeltaMovement();
    bool IsFireKeyDown();
}