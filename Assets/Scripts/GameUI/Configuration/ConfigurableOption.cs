using System;
using UnityEngine;

namespace GameUI.Configuration
{
    public abstract class ConfigurableOption : MonoBehaviour
    {
        public event Action OnTryChangeConfiguration;
        public event Action OnBackToSavedConfiguration;

        public bool Changed { get; protected set; }

        public abstract void ApplyChanges();
        
        protected void ActivateChangeConfigurationEvent() => OnTryChangeConfiguration?.Invoke();
        protected void ActivateBackToSavedConfigurationEvent() => OnBackToSavedConfiguration?.Invoke();
    }
}
