using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameUI.Configuration
{
    public class ScreenResolutionConfigurableOption : ConfigurableOption
    {
        [Header("Resolution Configuration")]
        [SerializeField] private TMP_Dropdown resolutionDropdown;

        private int _savedResolutionIndex;
        private int _currentResolutionIndex;
        
        private Resolution _currentResolution;
        
        private readonly List<string> _windowSizes = new List<string>();
        private readonly List<Resolution> _systemResolutions = new List<Resolution>();

        private void Awake()
        {
            GetWindowSizes();
            PopulateDropdownOptions();
        }

        private void GetWindowSizes()
        {
            var resolutions = Screen.resolutions;
            _currentResolution = Screen.currentResolution;

            var i = 0;

            foreach (var resolution in resolutions)
            {
                if (ResolutionExistsInList(resolution)) continue;

                _systemResolutions.Add(resolution);
                _windowSizes.Add(FormatResolution(resolution));

                if (IsCurrentResolution(resolution))
                {
                    _savedResolutionIndex = i;
                    _currentResolutionIndex = i;
                    Changed = false;
                }
                
                i++;
            }
        }
        
        private void PopulateDropdownOptions()
        {
            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(_windowSizes);
            resolutionDropdown.SetValueWithoutNotify(_currentResolutionIndex);
            
            resolutionDropdown.onValueChanged.AddListener(VerifyChanges);
        }

        private string FormatResolution(Resolution resolution)
        {
            return $"{resolution.width} x {resolution.height}";
        }

        private bool ResolutionExistsInList(Resolution resolution)
        {
            return _windowSizes.Contains(FormatResolution(resolution));
        }

        private bool IsCurrentResolution(Resolution resolution)
        {
            return resolution.width == _currentResolution.width
                && resolution.height == _currentResolution.height;
        }

        private void VerifyChanges(int index)
        {
            _currentResolutionIndex = index;
            Changed = index != _savedResolutionIndex;

            if (Changed)
                ActivateChangeConfigurationEvent();
            else
                ActivateBackToSavedConfigurationEvent();
        }

        public override void ApplyChanges()
        {
            if (!Changed) return;
            
            var resolution = _systemResolutions[_currentResolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            _savedResolutionIndex = _currentResolutionIndex;
        }
    }
}
