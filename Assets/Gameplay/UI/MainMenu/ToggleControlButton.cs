using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControlButton : MonoBehaviour
{
    [SerializeField] private PlayerInputData _inputData;
    [SerializeField] private Text _text;

    private int _inputType = 0;

    private void Awake()
    {
        _inputType = (int)_inputData.Type;

        UpdateText();
    }

    public void ToggleControlType()
    {
        var inputTypesArray = Enum.GetValues(typeof(InputType));

        _inputType++;
        _inputType %= inputTypesArray.Length;

        _inputData.Type = (InputType)_inputType;

        UpdateText();
    }

    private void UpdateText()
    {
        switch (_inputData.Type)
        {
            case InputType.Keyboard:
                _text.text = "”правление: клавиатура";
                break;
            case InputType.KeyboardAndMouse:
                _text.text = "”правление: клавиатура + мышь";
                break;
            default:
                break;
        }
    }
}