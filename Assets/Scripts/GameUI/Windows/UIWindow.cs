using UnityEngine;
using UnityEngine.Events;

namespace GameUI.Windows
{
    public class UIWindow : MonoBehaviour
    {
        public UnityEvent onWindowEnabled;
        public UnityEvent onWindowDisabled;

        public void EnableWindow(bool activate)
        {       
            gameObject.SetActive(activate);
            
            if (activate) onWindowEnabled?.Invoke();
            else onWindowDisabled?.Invoke();
        }
    }
}
