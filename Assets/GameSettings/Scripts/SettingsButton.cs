using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GameSettings
{
    public class SettingsButton : MonoBehaviour
    {
        #region Fields

        [SerializeField] private string[] buttonStates;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private OnValueChanged onValueChanged = new();
        
        private int _index;
        private int _maxIndex;

        #endregion

        #region LifeCycle

        private void Start()
        {
            _maxIndex = buttonStates.Length - 1;
        }

        #endregion

        #region PublicMethods

        public void SetParameterIndex(int index)
        {
            _index = index;
            infoText.text = buttonStates[_index];
        }

        public void ChangeIndexValue(int value)
        {
            _index += value;

            if (_index < 0 | _index > _maxIndex)
            {
                _index = _index < 0 ? 0 : _maxIndex;
                return;
            }

            infoText.text = buttonStates[_index];
            onValueChanged.Invoke(_index);
        }

        #endregion

    }

    [Serializable]
    public class OnValueChanged: UnityEvent<int> {}

}

