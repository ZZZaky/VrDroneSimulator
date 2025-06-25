using System;
using System.Collections.Generic;
using ConstructionVR.Assembly.Data;
using UnityEngine;
using WireVR.Wires;

namespace WireVR.Connection
{
    public class ErrorsCollector : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private List<InputManager> inputManagers;

        [SerializeField]
        private bool show;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            foreach(var inputManager in FindObjectsOfType<InputManager>())
                if (!inputManagers.Contains(inputManager))
                    inputManagers.Add(inputManager);
        }

        private void OnEnable()
        {
            foreach(var inputManager in inputManagers)
                if (inputManager != null)
                    inputManager.WireInput.onChangeCurrentWireConnected += OnChangeInputConnected;
        }
        
        private void OnDisable()
        {
            foreach(var inputManager in inputManagers)
                if (inputManager != null)
                    inputManager.WireInput.onChangeCurrentWireConnected -= OnChangeInputConnected;
        }

        private void Update()
        {
            if (show)
            {
                show = false;

                foreach(var inputManager in inputManagers)
                    Debug.Log(
                        $"[{inputManager.CurrentDetail.name} разьём для [{inputManager.WireInput.AllowDetail.name}]]: Подключенность-{!inputManager.TryGetError(out var errorMessage)} | {errorMessage}"
                    );
            }
        }

        #endregion

        #region Private Methods
        private void OnChangeInputConnected(WireInput input, Wire wire, Detail detail)
        {
            Debug.Log($"Прогресс подключения: {CalculateConnectedProgress()*100f}%");
        }

        private float CalculateConnectedProgress()
        {
            float connectedCount = 0;

            foreach(var inputManager in inputManagers)
                if (!inputManager.TryGetError(out var message))
                    connectedCount++;
                    
            return connectedCount/inputManagers.Count;
        }

        #endregion
    }
}