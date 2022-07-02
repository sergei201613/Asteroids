using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputData", menuName = "ScriptableObjects/PlayerInputData", order = 1)]
public class PlayerInputData : ScriptableObject
{
    public InputType Type;

    public KeyCode PauseKey = KeyCode.Escape;
}

public enum InputType
{
    Keyboard, KeyboardAndMouse
}