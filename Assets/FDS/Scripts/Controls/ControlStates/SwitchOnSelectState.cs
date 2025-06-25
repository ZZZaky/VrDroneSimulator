using FDS.StateMachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Zenject;

namespace FDS.Controls.ControlStates
{
    public class SwitchOnSelectState : State
    {
        #region Fields
        private GameObject player;

        [SerializeField]
        private List<Behaviour> switchingObjects;

        #endregion

        #region Constructors

        [Inject]
        private void Constuct(PlayerLinker playerLinker)
        {
            Debug.Log(defaultOutput.ToString());

            StateType type = GetTypeOfControlState();

            switch (type)
            {
                case StateType.Drone:
                    player = playerLinker.Player;

                    switchingObjects.Add(player.GetComponent<ActionBasedSnapTurnProvider>());
                    switchingObjects.Add(player.GetComponent<LocomotionSystem>());
                    switchingObjects.Add(player.GetComponent<TeleportationProvider>());
                    switchingObjects.Add(player.GetComponent<ActionBasedContinuousTurnProvider>());
                    switchingObjects.Add(player.GetComponent<DynamicMoveProvider>());
                    break;
                case StateType.Character:
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Public Methods

        public override void OnEnter()
        {
            Debug.Log("ON");
            SwitchAll();
        }

        public override void OnExit()
        {
            SwitchAll();
            Debug.Log("OFF");
        }

        #endregion

        #region Private Methods

        private void SwitchAll()
        {
            foreach (var switchingObject in switchingObjects)
                switchingObject.enabled = !switchingObject.enabled;
        }

        #endregion
    }
}