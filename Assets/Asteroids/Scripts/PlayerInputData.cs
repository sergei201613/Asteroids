using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputData", menuName = "ScriptableObjects/PlayerInputData", order = 1)]
public class PlayerInputData : ScriptableObject
{
    [SerializeField]
    private List<InputType> _inputTypes = new();
    [SerializeField]
    private List<string> _inputNames = new();
    [SerializeField]
    private InputType _type;

    public event System.Action<InputType> InputChanged;

    public KeyCode PauseKey = KeyCode.Escape;

    public InputType GetInputType()
    {
        return _type;
    }

    public void SetInputType(InputType type)
    {
        _type = type;
        InputChanged?.Invoke(_type);
    }

    public string GetInputName()
    {
        return GetInputNameByType(_type);
    }

    public string GetInputNameByType(InputType type)
    {
        int idx = _inputTypes.IndexOf(type);
        return _inputNames[idx];
    }

    public InputType GetInputTypeByName(string name)
    {
        int idx = _inputNames.IndexOf(name);
        return _inputTypes[idx];
    }
    
    public IReadOnlyList<InputType> GetInputTypes()
    {
        return _inputTypes;
    }
    
    public IReadOnlyList<string> GetInputNames()
    {
        return _inputNames;
    }

    public ISpaceshipInput GetInput(InputType type)
    {
        switch (type)
        {
            case InputType.Keyboard:
                return new PlayerKeyboardInput();
            case InputType.KeyboardAndMouse:
                return new PlayerKeyboardAndMouseInput();
            default:
                throw new ArgumentException();
        }
    }
}

public enum InputType
{
    Keyboard, KeyboardAndMouse
}