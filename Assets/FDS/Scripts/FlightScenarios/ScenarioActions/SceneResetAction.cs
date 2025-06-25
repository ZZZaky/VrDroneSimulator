using FDS.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FDS.FlightScenarios.ScenarioActions
{
    public class SceneResetAction : ScenarioAction
    {
        #region Fields

        [SerializeField]
        private float resetDelaySeconds;

        [SerializeField]
        private List<ResetObjectsProvider> resetObjectProviders;

        [SerializeField]
        private List<Transform> resetObjects;

        private Dictionary<Transform, ResetComponentData> _startValues;

        #endregion

        #region Events

        public event Action OnSceneRestarted;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _startValues = new Dictionary<Transform, ResetComponentData>();
            resetObjects.AddRange(resetObjectProviders.SelectMany(e => e.GetResetComponents()));

            foreach (var resetable in resetObjects)
            {
                RegisterResetableObject(resetable);
            }
        }

        #endregion

        #region Public Methods

        public override void ExecuteAction()
        {
            StartCoroutine(ResetCoroutine());
        }

        public void RegisterResetableObject(Transform resetable)
        {
            if (_startValues.ContainsKey(resetable))
            {
                Debug.LogError($"{nameof(SceneResetAction)}-{gameObject.name}:объект {resetable} уже добавлен");
                return;
            }
            if (!resetObjects.Contains(resetable)) resetObjects.Add(resetable);
            _startValues.Add(resetable,
                new ResetComponentData(resetable.transform.localPosition,
                resetable.transform.localRotation,
                resetable.GetComponent<Rigidbody>(),
                resetable.GetComponents<IResetable>()));
        }

        public void UnregisterResetableObject(Transform resetable)
        {
            _startValues.Remove(resetable);
            resetObjects.Remove(resetable);
        }

        #endregion

        #region Private Methods

        private void ResetScene()
        {
            OnSceneRestarted?.Invoke();
            foreach (var component in resetObjects)
            {
                ResetObject(component);
            }
        }

        private void ResetObject(Transform component)
        {
            component.gameObject.SetActive(false);
            var values = _startValues[component];

            component.transform.localPosition = values.StartPosition;
            component.transform.localRotation = values.Rotation;

            if (values.Resetables != null)
            {
                foreach (var resetable in values.Resetables)
                    resetable.Reset();
            }

            if (values.Rigidbody != null)
            {
                values.Rigidbody.velocity = Vector3.zero;
                values.Rigidbody.angularVelocity = Vector3.zero;
            }

            component.gameObject.SetActive(true);
        }

        private IEnumerator ResetCoroutine()
        {
            yield return new WaitForSeconds(resetDelaySeconds);
            ResetScene();
        }

        #endregion
    }
}
