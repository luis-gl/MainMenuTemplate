using GameUI.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameUI
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Menu Windows")]
        [SerializeField] private UIWindow[] menuWindows;

        [Header("Properties")]
        [SerializeField] private string mainSceneName;
        [SerializeField] private int defaultWindowIndex;

        private UIWindow _activeWindow;

        private void Awake()
        {
            DisableAllWindows();
            ActivateWindow(defaultWindowIndex);
        }

        private void DisableAllWindows()
        {
            foreach (var window in menuWindows)
            {
                window.EnableWindow(false);
            }
        }

        public void ActivateWindow(int windowIndex)
        {
            if (_activeWindow == menuWindows[windowIndex]) return;
            
            if (_activeWindow) _activeWindow.EnableWindow(false);

            _activeWindow = menuWindows[windowIndex];
            _activeWindow.EnableWindow(true);
        }

        #region MenuButtonsCallbacks

        public void StartGame()
        {
            if (string.IsNullOrEmpty(mainSceneName)) return;
            
            SceneManager.LoadScene(mainSceneName);
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }

        #endregion
    }
}
