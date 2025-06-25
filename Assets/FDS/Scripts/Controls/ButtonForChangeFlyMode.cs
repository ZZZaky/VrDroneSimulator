using FDS.Drone.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForChangeFlyMode : BaseButton
{
    #region Fields

    [SerializeField]
    private FlyingModeChanger changer;

    [SerializeField]
    [Range(-1, 1)]
    private int value;

    #endregion

    #region Public Metods
    public void ChangeFlyMode()
    {
        changer.OpenChangeFlyMode(value);
    }

    #endregion
}
