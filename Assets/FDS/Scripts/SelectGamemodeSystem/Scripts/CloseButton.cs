using FDS.UI;
using UnityEngine;
using UnityEngine.UI;

namespace FDS.SelectGamemodeSystem
{
    [RequireComponent(typeof(Button))]
    public class CloseButton : MonoBehaviour
    {
        #region Fields

        private Button _button;

        #endregion

        #region Properties

        public int SceneIndex { get; set; }

        public LevelLoader LvlLoader { get; set; }

        public UISceneMonitor UIMonitorController { get; set; }

        #endregion

        #region LifeCycle

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(UnloadGamemodeScene);

        }

        #endregion

        #region PrivateMethods

        private void UnloadGamemodeScene()
        {
            LvlLoader.UnloadMap(SceneIndex);
            UIMonitorController.HideGamemodeScreen();
        }

        #endregion

    }
}

