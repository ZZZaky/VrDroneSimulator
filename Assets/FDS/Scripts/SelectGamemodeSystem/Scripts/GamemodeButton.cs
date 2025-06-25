using FDS.UI;
using TMPro;
using Trisibo;
using UnityEngine;
using UnityEngine.UI;

namespace FDS.SelectGamemodeSystem
{
    [RequireComponent(typeof(Button))]
    public class GamemodeButton : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TextMeshProUGUI recordTimeText;

        private Button _button;
        private string _modeName;

        #endregion

        #region Properties

        public SceneField ButtonScene { get; set; }

        public string ButtonText
        {
            set
            {
                _modeName = value;
                buttonText.text = _modeName;
            }
            get
            {
                return _modeName;
            }
        }

        public string RecordText
        {
            set
            {
                recordTimeText.text = value;
            }
        }

        public int sceneID { get; set; }

        public UISceneMonitor UIController { get; set; }

        public LevelLoader LvlLoader { get; set; }

        #endregion

        #region LifeCycle

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(LoadScene);
        }

        #endregion

        #region PrivateMethods

        private void LoadScene()
        {
            LvlLoader.LoadGamemode(ButtonScene.BuildIndex);
            UIController.ShowGamemodeScreen(ButtonText, ButtonScene.BuildIndex);
        }

        #endregion

    }
}

