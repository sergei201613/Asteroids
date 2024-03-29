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
        _inputType = (int)_inputData.GetInputType();

        UpdateText();
    }

    public void ToggleControlType()
    {
        var inputTypesArray = Enum.GetValues(typeof(InputType));

        _inputType++;
        _inputType %= inputTypesArray.Length;

        //_inputData.GetInputType() = (InputType)_inputType;

        UpdateText();
    }

    private void UpdateText()
    {
        switch (_inputData.GetInputType())
        {
            case InputType.Keyboard:
                _text.text = "����������: ����������";
                break;
            case InputType.KeyboardAndMouse:
                _text.text = "����������: ���������� + ����";
                break;
            default:
                break;
        }
    }
}