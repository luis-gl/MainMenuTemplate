using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Configuration
{
    public class ConfigurationController : MonoBehaviour
    {
        [Header("Configuration Controllers")]
        [SerializeField] private ConfigurableOption[] configurableOptions;

        [Header("Scene Objects")]
        [SerializeField] private Button applyButton;

        private void OnEnable() => RegisterToEvents();

        private void OnDisable() => UnregisterToEvents();

        private void Awake() => DeactivateApplyButton();

        private void RegisterToEvents()
        {
            foreach (var configurable in configurableOptions)
            {
                configurable.OnTryChangeConfiguration += OnTryChangeConfiguration;
                configurable.OnBackToSavedConfiguration += OnBackToSavedConfiguration;
            }
        }

        private void UnregisterToEvents()
        {
            foreach (var configurable in configurableOptions)
            {
                configurable.OnTryChangeConfiguration -= OnTryChangeConfiguration;
                configurable.OnBackToSavedConfiguration -= OnBackToSavedConfiguration;
            }
        }

        private void DeactivateApplyButton()
        {
            applyButton.interactable = false;
        }

        private void OnBackToSavedConfiguration()
        {
            foreach (var configurable in configurableOptions)
            {
                if (configurable.Changed) return;
            }

            DeactivateApplyButton();
        }

        private void OnTryChangeConfiguration()
        {
            applyButton.interactable = true;
        }

        // Called from Apply button...
        public void ApplyChanges()
        {
            foreach (var configurable in configurableOptions)
            {
                configurable.ApplyChanges();
            }
            
            DeactivateApplyButton();
        }
    }
}
