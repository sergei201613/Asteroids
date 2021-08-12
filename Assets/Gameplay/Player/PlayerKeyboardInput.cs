using UnityEngine;

public class PlayerKeyboardInput : MonoBehaviour, ISpaceshipInput
{
    public float GetDeltaMovement()
    {
        return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? 1 : 0;
    }

    public float GetDeltaRotation()
    {
        float deltaRotation = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) deltaRotation += 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) deltaRotation -= 1;
        
        return deltaRotation;
    }

    public bool IsFireKeyDown()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}