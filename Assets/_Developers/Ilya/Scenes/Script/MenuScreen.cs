using FDS.SelectGamemodeSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuScreen : MonoBehaviour
{
    #region Fileds
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject menuBackground;
    [SerializeField] private InputActionReference lisenPressB;
    #endregion
    #region LifeCycle
    private void OnEnable()
    {
        lisenPressB.action.performed += Action_performed;
    }

    private void OnDisable()
    {
        lisenPressB.action.performed -= Action_performed;
    }
    #endregion
    #region PublicMethods
    public void ShowMenuScreen()
    {
        menuScreen.SetActive(true);
    }
    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    

    public void TurnMenuScreen()
    {
        menuBackground.SetActive(!menuBackground.activeInHierarchy);
    }
    #endregion
    #region PrivateMethods
    private void Action_performed(InputAction.CallbackContext obj)
    {
        TurnMenuScreen();
    }
    #endregion
}
