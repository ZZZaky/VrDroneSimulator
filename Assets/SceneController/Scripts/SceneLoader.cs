using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneController
{
    public class SceneLoader : MonoBehaviour
    {
        #region Fields

        private bool _hasLoadSceneProcess = false;
        private Coroutine _loadGameMapSceneCoroutine;
        private Coroutine _loadGameModeSceneCoroutine;

        #endregion

        #region Properties

        public bool HasLoadSceneProcess => _hasLoadSceneProcess;
        public Scene CurrentMapScene { get; private set; }
        public Scene CurrentSceneGameMode { get; private set; }

        #endregion

        #region Events

        public event Action OnStartLoadScene;
        public event Action OnFinishLoadScene;
        public event Action OnGameModeExited;

        private event Action callbackFinishLoadGameMode;

        #endregion

        #region PublicMethods

        public void UnloadScene(int sceneBuildIndex)
        {
            StartCoroutine(UnloadSceneCoroutine(sceneBuildIndex));
        }
        /// <summary>
        /// Загружает сцену с игровой картой без игровых сценариев  
        /// </summary>
        /// <param name="buildIndex"></param>
        public void LoadGameMapScene(int buildIndex)
        {
            if (_loadGameMapSceneCoroutine != null)
            {
                Debug.LogError("Нельзя загрузить сцену во время загрузки другой!");
                return;
            }

            _loadGameMapSceneCoroutine = StartCoroutine(LoadGameMap(buildIndex));
        }

        /// <summary>
        /// Загружает сцену с игровым сценарием. Предварительно должна быть загружена 
        /// сцена с игровой картой
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadGameModeScene(int sceneBuildIndex, Action callbackLoadFinish = null)
        {
            if (_loadGameModeSceneCoroutine != null)
            {
                Debug.LogError("Нельзя загрузить игровой мод во время загрузки другого мода!");
                return;
            }

            callbackFinishLoadGameMode = callbackLoadFinish;
            _loadGameModeSceneCoroutine = StartCoroutine(LoadGameMode(sceneBuildIndex));
        }

#if UNITY_EDITOR

        public void SetGameMapScene(Scene scene)
        {
            CurrentMapScene = scene;
        }

#endif

        #endregion

        #region PrivateMethods

        private IEnumerator UnloadSceneCoroutine(int sceneBuildIndex)
        {
            yield return new WaitForEndOfFrame();
            if (!SceneManager.GetSceneByBuildIndex(sceneBuildIndex).isLoaded)
                yield break;
            OnGameModeExited?.Invoke();
            yield return SceneManager.UnloadSceneAsync(sceneBuildIndex);
        }

        private IEnumerator LoadGameMode(int sceneBuildIndex)
        {
            yield return new WaitForEndOfFrame();

            if (SceneManager.GetSceneByBuildIndex(sceneBuildIndex).isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(sceneBuildIndex);

                if (CurrentSceneGameMode.buildIndex == sceneBuildIndex)
                    CurrentSceneGameMode = default;
            }

            if (CurrentSceneGameMode.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(CurrentSceneGameMode);
            }

            yield return SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);
            CurrentSceneGameMode = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);

            callbackFinishLoadGameMode?.Invoke();
            _loadGameModeSceneCoroutine = null;
        }

        private IEnumerator LoadGameMap(int buildIndex)
        {

            yield return new WaitForEndOfFrame();

            if (CurrentSceneGameMode.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(CurrentSceneGameMode.buildIndex);
            }
            if (SceneManager.GetSceneByBuildIndex(buildIndex).isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(buildIndex);

                if (CurrentMapScene.buildIndex == buildIndex)
                    CurrentMapScene = default;
            }

            if (CurrentMapScene.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(CurrentMapScene);
                CurrentMapScene = default;
            }

            if (_hasLoadSceneProcess)
            {
                Debug.LogError("Нельзя загрузить сцену во время загрузки другой!");
                yield break;
            }

            if (CurrentSceneGameMode.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(CurrentSceneGameMode);
                CurrentSceneGameMode = default;
            }

            _hasLoadSceneProcess = true;
            yield return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);

            CurrentMapScene = SceneManager.GetSceneByBuildIndex(buildIndex);

            LoadSceneCompleted();
        }

        private void LoadSceneCompleted()
        {

            _loadGameMapSceneCoroutine = null;
            _hasLoadSceneProcess = false;
            OnFinishLoadScene?.Invoke();
        }

        #endregion
    }
}
