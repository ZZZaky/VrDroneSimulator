using UnityEngine;

namespace GameSettings
{
    public class ShowSettingsUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject gameSettings;

        #endregion

        #region PublicMethods

        public void OnShowSettings()
        {
            gameSettings.SetActive(!gameSettings.activeSelf);
        }

        #endregion
    }
}

