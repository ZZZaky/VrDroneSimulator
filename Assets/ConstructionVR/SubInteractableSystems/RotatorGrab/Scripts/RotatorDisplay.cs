using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ConstructionVR.SubInteractableSystem.RotatorGrab
{
    public class RotatorDisplay : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Rotator rotator;

        [Header("UI")]
        [SerializeField]
        private GameObject displayPage;

        [SerializeField]
        private TextMeshProUGUI angleText;

        [SerializeField]
        private AnimationCurve showingAnimation;

        private float _currentTimeKey = 0;

        #endregion

        #region Properties

        public float CurrentTime
        {
            get { return _currentTimeKey; }
            private set
            {
                _currentTimeKey = value;
                if (_currentTimeKey == showingAnimation.keys[0].time)
                {
                    OnHide?.Invoke();
                }
                else if (_currentTimeKey == showingAnimation.keys[showingAnimation.keys.Length - 1].time)
                {
                    OnShow?.Invoke();
                }
            }
        }

        #endregion

        #region Events

        public UnityEvent OnShow;
        public UnityEvent OnHide;

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            rotator.OnUpdateAngles.AddListener(UpdateAngles);
            rotator.OnStartRotation.AddListener(StartRotation);
            rotator.OnStopRotation.AddListener(StopRotation);
        }

        private void OnDisable()
        {
            rotator.OnUpdateAngles.RemoveListener(UpdateAngles);
        }

        #endregion

        #region PrivateMethods

        private void StartRotation()
        {
            ShowDisplayPageWithAnimation();
        }

        private void StopRotation()
        {
            HideDisplayPageWithAnimation();
        }

        private void UpdateAngles(float angle)
        {
            if (angle > 180)
                angle = 360 - angle;

            angle = Mathf.Abs(angle);
            angleText.text = angle.ToString("f0") + "°";
        }

        private void ShowDisplayPageWithAnimation()
        {
            var startTime = showingAnimation.keys[0].time;
            var endTime = showingAnimation.keys[showingAnimation.keys.Length - 1].time;

            StopAllCoroutines();
            StartCoroutine(ScalingDisplayPageWithAnimation(endTime, startTime, endTime));
        }

        private void HideDisplayPageWithAnimation()
        {
            var startTime = showingAnimation.keys[0].time;
            var endTime = showingAnimation.keys[showingAnimation.keys.Length - 1].time;

            StopAllCoroutines();
            StartCoroutine(ScalingDisplayPageWithAnimation(startTime, startTime, endTime));
        }

        private IEnumerator ScalingDisplayPageWithAnimation(float targetTime, float startTime, float endTime)
        {
            targetTime = Mathf.Clamp(targetTime, startTime, endTime);
            var dir = targetTime < CurrentTime ? -1 : 1;

            while (CurrentTime != targetTime)
            {
                CurrentTime += Time.deltaTime * dir;
                CurrentTime = Mathf.Clamp(CurrentTime, startTime, endTime);

                var newScale = showingAnimation.Evaluate(CurrentTime);
                displayPage.transform.localScale = new Vector3(newScale, newScale, newScale);

                yield return new WaitForEndOfFrame();
            }

        }

        #endregion
    }
}