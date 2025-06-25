using SceneController;
using UnityEngine;
using Zenject;

public class SceneLoaderHotKey : MonoBehaviour
{
    #region Fields

    private HotKey inputActions;
    private SceneLoader _sceneLoader;

    #endregion

    #region Constructors

    [Inject]
    private void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    #endregion

    #region LifeCycle

    private void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new HotKey();
        }

        inputActions.Enable();
        inputActions.HotKeyActionMap.LoadOne.performed += LoadOne_performed;
        inputActions.HotKeyActionMap.LoadTwo.performed += LoadTwo_performed;
        inputActions.HotKeyActionMap.LoadOneGM.performed += LoadOneGM_performed;
        inputActions.HotKeyActionMap.LoadTwoGM.performed += LoadTwoGM_performed;
        inputActions.HotKeyActionMap.ExitFromGameMode.performed += ExitFromGameMode;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.HotKeyActionMap.LoadOne.performed -= LoadOne_performed;
        inputActions.HotKeyActionMap.LoadTwo.performed -= LoadTwo_performed;
        inputActions.HotKeyActionMap.LoadOneGM.performed -= LoadOneGM_performed;
        inputActions.HotKeyActionMap.LoadTwoGM.performed -= LoadTwoGM_performed;
        inputActions.HotKeyActionMap.ExitFromGameMode.performed += ExitFromGameMode;
    }

    #endregion

    #region Private Methods

    private void LoadTwo_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _sceneLoader.LoadGameMapScene(3);
    }

    private void LoadOne_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _sceneLoader.LoadGameMapScene(2);
    }

    private void LoadOneGM_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _sceneLoader.LoadGameModeScene(4);
    }

    private void LoadTwoGM_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _sceneLoader.LoadGameModeScene(5);
    }

    private void ExitFromGameMode(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _sceneLoader.UnloadScene(5);
    }

    #endregion

}
