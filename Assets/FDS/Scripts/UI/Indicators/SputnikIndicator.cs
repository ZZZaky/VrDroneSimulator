using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FDS.UI.Indicators
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SputnikIndicator : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private int averageValue;

        [SerializeField]
        [Range(0, 1)]
        private float changeChance;

        [SerializeField]
        [Min(0)]
        private float changeIntervalSeconds;

        private TextMeshProUGUI _label;
        private int _value;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();
            _value = averageValue;
            StartCoroutine(SetSputnikCountCoroutine());
        }

        #endregion

        #region Private Methods

        private IEnumerator SetSputnikCountCoroutine()
        {
            while (true)
            {
                if (Random.Range(0, 1f) >= changeChance)
                {
                    if (_value > averageValue)
                        _value--;
                    else if (_value < averageValue)
                        _value++;
                    else
                        _value += Random.Range(-1, 2);
                    _label.SetText(_value.ToString());
                }
                yield return new WaitForSeconds(changeIntervalSeconds);
            }
        }

        #endregion
    }
}
