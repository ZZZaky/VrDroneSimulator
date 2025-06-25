using FDS.UI;
using UnityEngine;
using UnityEngine.UI;

namespace FDS.SelectGamemodeSystem
{
    [RequireComponent(typeof(Button))]
    public class BackButton : MonoBehaviour
    {
        #region Fields

        private Button _button;

        #endregion

        #region Properties

        public UISceneMonitor UIController
        {
            get;
            set;
        }

        #endregion

        #region LifeCycle

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HideCurrentScreen);
        }

        #endregion

        #region PrivateMethods

        private void HideCurrentScreen()
        {
            UIController.HideGamemodeList();
        }

        #endregion

    }
}

