using SceneController;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FDS.SelectGamemodeSystem
{
    public class LevelLoader : MonoBehaviour
    {
        #region Fields

        private SceneLoader _sceneLoader;
        private GameObject _loadingScreen;
        private GameObject _menuScreen;

        #endregion

        #region Constructors

        [Inject]
        private void Construct(SceneLoader sceneLoader, PlayerChildrenObjLinker playerChildrenObjLinker)
        {
            _loadingScreen = playerChildrenObjLinker.LoadingScreen;
            _menuScreen = playerChildrenObjLinker.MenuScreen;
            _sceneLoader = sceneLoader;
        }

        #endregion

        #region LifeCycle
        public void OnEnable()
        {
            _loadingScreen.SetActive(false);
        }

        #endregion

        #region PublicMethods

        public void LoadGamemode(int sceneIndex)
        {
            _sceneLoader.LoadGameModeScene(sceneIndex);
        }

        public void UnloadMap(int sceneIndex)
        {            
            _sceneLoader.UnloadScene(sceneIndex);
        }

        public void LoadMap(int sceneIndex)
        {
            _sceneLoader.LoadGameMapScene(sceneIndex);
            _loadingScreen.SetActive(true);
        }


        public void ExitGame()
        {
            Application.Quit();
        }

        #endregion
    }
}

