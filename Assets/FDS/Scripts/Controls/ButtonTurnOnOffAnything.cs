using UnityEngine;

public class ButtonTurnOnOffAnything : BaseButton
{
    #region Fields

    [SerializeField]
    private GameObject objForTurn;

    [SerializeField]
    private DroneCameraRenderManager cameraManager;

    #endregion

    #region Public Methods

    public void TurnObj()
    {
        bool turnValue = objForTurn.activeInHierarchy ? false : true;
        objForTurn.SetActive(turnValue);
    }

    public void SendStateToManager()
    {
        if (!cameraManager.MonitorState)
        {
            cameraManager.MonitorState = true;

        } else { cameraManager.MonitorState = false; }
    }

    public void UpdateState()
    {
        cameraManager.StateCheck();
    }

    #endregion
    
}
