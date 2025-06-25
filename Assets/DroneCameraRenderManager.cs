using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCameraRenderManager : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Camera droneCamera;

    #endregion

    #region Properties

    public bool MonitorState;

    public bool HadsetState;

    #endregion

    #region Public Methods
    public void StateCheck()
    {
        if (!HadsetState && !MonitorState)
        {
            droneCamera.gameObject.SetActive(false);
        }
        else
        {
            droneCamera.gameObject.SetActive(true);
        }
    }
    #endregion
}
