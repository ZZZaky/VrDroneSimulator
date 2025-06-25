using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Zenject;

public class StanPlayerMainMenu : MonoBehaviour
{
    #region Fields

    private GameObject player;
    
    [SerializeField]
    private List<Behaviour> moveSystems;

    #endregion

    #region Constructors

    [Inject]
    private void Construct(PlayerLinker playerLink)
    {
        player = playerLink.Player;

        moveSystems.Add(player.GetComponent<ActionBasedSnapTurnProvider>());
        moveSystems.Add(player.GetComponent<DynamicMoveProvider>());
        moveSystems.Add(player.GetComponent<TeleportationProvider>());
        moveSystems.Add(player.GetComponent<LocomotionSystem>());
    }

    #endregion

    #region LifeCycle
    private void OnEnable()
    {
        SwitchMove(false);
    }

    private void OnDisable()
    {
        SwitchMove(true);
    }

    #endregion

    #region Private Methods

    private void SwitchMove(bool switchMove)
    {
        foreach (var movmentSys in moveSystems)
            if (movmentSys != null)
                movmentSys.enabled = switchMove;
    }

    #endregion


}
