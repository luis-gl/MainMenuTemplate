using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GameUI.Configuration
{
    public class VolumeControllerConfigurableOption : ConfigurableOption
    {
        [Header("Volume Configuration")]
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private string mixerPropertyName;

        private float _savedVolume;
        private float _currentVolume;
        
        private const float AudioDBMultiplier = 30f;
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _savedVolume = PlayerPrefs.GetFloat(mixerPropertyName, volumeSlider.value);
            
            _currentVolume = _savedVolume;
            
            SetMixerVolume();
            
            volumeSlider.SetValueWithoutNotify(_savedVolume);
            volumeSlider.onValueChanged.AddListener(VerifyChanges);
        }

        private void VerifyChanges(float value)
        {
            _currentVolume = value;

            Changed = !Mathf.Approximately(_currentVolume, _savedVolume);
            
            if (Changed) ActivateChangeConfigurationEvent();
            else ActivateBackToSavedConfigurationEvent();
        }

        private void SetMixerVolume()
        {
            audioMixer.SetFloat(mixerPropertyName, Mathf.Log10(_currentVolume) * AudioDBMultiplier);
        }

        public override void ApplyChanges()
        {
            SetMixerVolume();
            PlayerPrefs.SetFloat(mixerPropertyName, _currentVolume);
            _savedVolume = _currentVolume;
        }
    }
}
