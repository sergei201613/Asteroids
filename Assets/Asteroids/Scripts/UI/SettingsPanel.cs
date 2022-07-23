using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class SettingsPanel : Panel
    {
        [SerializeField]
        private AudioMixerGroup _audioMasterGroup;
        [SerializeField]
        private PlayerInputData _inputData;

        protected override void Awake()
        {
            base.Awake();

            SetupVolumeOption();
            SetupInputOption();
        }

        private void SetupInputOption()
        {
            var input = root.Q<DropdownField>("input-mode");
            input.choices = new List<string>(_inputData.GetInputNames());
            input.value = _inputData.GetInputName();
            input.RegisterCallback<ChangeEvent<string>>(OnInputChanged);
        }

        private void OnInputChanged(ChangeEvent<string> evt)
        {
            var inputType = _inputData.GetInputTypeByName(evt.newValue);
            _inputData.SetInputType(inputType);
        }

        private void SetupVolumeOption()
        {
            var volume = root.Q<Slider>("volume");
            volume.RegisterCallback<ChangeEvent<float>>(OnVolumeChanged);

            var mixer = _audioMasterGroup.audioMixer;

            if (mixer.GetFloat("MasterVolume", out var value))
            {
                volume.value = Mathf.InverseLerp(-80, 0, value);
            }
        }

        private void OnVolumeChanged(ChangeEvent<float> evt)
        {
            _audioMasterGroup.audioMixer.SetFloat("MasterVolume", 
                Mathf.Lerp(-80, 0, evt.newValue));
        }
    }
}
