using FDS.Interfaces;
using System.Text;
using TMPro;
using UnityEngine;

namespace Tools.Timer
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TimerTextIndicator : MonoBehaviour, IResetable
    {
        #region Fields

        private TextMeshProUGUI _label;
        private Timer _timer;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _timer = new Timer();
            _label = GetComponent<TextMeshProUGUI>();
            _timer.StartTimer();
            _label.text = "";
        }

        private void Update()
        {
            if (_timer.IsRunning)
            {
                _label.text = _timer.GetTime().ToString();
            }
        }

        #endregion

        #region Public Methods

        public void Reset()
        {
            _label.text = "";
            //this method isn't working
            //_timer.ResetTimer();
            //_timer.StartTimer(); 
        }

        #endregion
    }
}