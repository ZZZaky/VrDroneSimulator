using FDS.Interfaces;
using System.Collections;
using TMPro;
using UnityEngine;

namespace FDS.FlightScenarios.ScenarioActions
{
    public class ShowMessageAction : ScenarioAction, IResetable
    {
        #region Fields

        [SerializeField]
        private TextMeshProUGUI label;

        [SerializeField]
        private float appearanceTimeSeconds;

        [SerializeField]
        private float appearanceSteps;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            label.gameObject.SetActive(false);
        }

        #endregion

        #region Public Methods

        public override void ExecuteAction()
        {
            StartCoroutine(Appearance());
        }

        #endregion

        #region Private Methods

        private IEnumerator Appearance()
        {
            label.gameObject.SetActive(true);
            var color = label.color;
            var standartColorAlpha = color.a;
            var progress = 0f;
            var stepProgress = 1 / appearanceSteps;
            var stepTime = appearanceTimeSeconds * stepProgress;

            for (int i = 0; i < appearanceSteps; i++)
            {
                color.a = Mathf.Lerp(0, standartColorAlpha, progress);
                progress += stepProgress;
                label.color = color;
                yield return new WaitForSeconds(stepTime);
            }
        }

        void IResetable.Reset()
        {
            StopAllCoroutines();
            label.gameObject.SetActive(false);
        }

        #endregion
    }
}