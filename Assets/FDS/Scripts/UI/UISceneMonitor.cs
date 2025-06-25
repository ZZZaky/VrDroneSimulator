using FDS.SelectGamemodeSystem;
using TMPro;
using UnityEngine;

namespace FDS.UI
{
    public class UISceneMonitor : MonoBehaviour
    {
        #region Fileds

        [SerializeField] private GameObject mainScreen;
        [SerializeField] private GameObject gamemodeListScreen;
        [SerializeField] private GameObject gamemodeScreen;
        [SerializeField] private GameObject settingsScreen;
        [SerializeField] private CloseButton closeButton;
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] private TextMeshProUGUI gamemodeName;

        #endregion

        #region LifeCycle

        private void Start()
        {
            closeButton.UIMonitorController = this;
            closeButton.LvlLoader = levelLoader;
        }

        #endregion

        #region PublicMethods

        public void ExitToMenu(int sceneIndex)
        {
            levelLoader.LoadMap(sceneIndex);
        }

        public void ShowGamemodeList()
        {
            gamemodeListScreen.SetActive(true);
            mainScreen.SetActive(false);
            CloseSettingsMenu();
        }

        public void ShowGamemodeScreen(string name, int sceneIndex)
        {
            gamemodeListScreen.SetActive(false);
            gamemodeScreen.SetActive(true);
            gamemodeName.text = name;
            closeButton.SceneIndex = sceneIndex;
        }

        public void HideGamemodeScreen()
        {
            gamemodeScreen.SetActive(false);
            gamemodeListScreen.SetActive(true);
        }

        public void HideGamemodeList()
        {
            gamemodeListScreen.SetActive(false);
            mainScreen.SetActive(true);
        }

        public void ShowSettingsScreen()
        {
            settingsScreen.SetActive(true);
        }

        #endregion

        #region PrivateMethods

        private void CloseSettingsMenu()
        {
            settingsScreen.SetActive(false);
        }

        #endregion

    }
}

