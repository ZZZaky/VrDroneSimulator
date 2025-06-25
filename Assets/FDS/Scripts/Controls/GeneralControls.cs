using UnityEngine;
using UnityEngine.InputSystem;

namespace FDS.Controls
{
    [RequireComponent(typeof(PlayerInput))]
    public class GeneralControls : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private InputActionReference lisenPressA;

        [SerializeField]
        private StateMachine.StateMachine controlsStateMachine;

        [SerializeField]
        private GameObject DroneSetupUi;

        [SerializeField] 
        private GameObject gameSettings;

        [SerializeField]
        private DroneCameraRenderManager forCheckHadsetState;

        #endregion

        private void OnEnable()
        {
            lisenPressA.action.performed += Action_performed;
        }

        private void OnDisable() 
        { 
            lisenPressA.action.performed -= Action_performed;
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            if (!forCheckHadsetState.HadsetState) { OnSelectMovement(); }
        }


        #region Private Methods

        private void OnShowSetup()
        {
            DroneSetupUi.SetActive(!DroneSetupUi.activeSelf);
            if (gameSettings.activeSelf)
            {
                gameSettings.SetActive(!gameSettings.activeSelf);
            }
        }

        private void OnShowSettings()
        {
            gameSettings.SetActive(!gameSettings.activeSelf);
            if (DroneSetupUi.activeSelf)
            {
                DroneSetupUi.SetActive(!DroneSetupUi.activeSelf);
            }
        }

        private void OnSelectMovement()
        {
            controlsStateMachine.NextDefaultState();
            Debug.Log("OnSelectMovement");
        }

        #endregion
    }
}