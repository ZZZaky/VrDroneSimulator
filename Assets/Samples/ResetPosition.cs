using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform offset;

    private HotKey inputActions;

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new HotKey();
        }

        inputActions.Enable();
        inputActions.HotKeyActionMap.ResetPosition.performed += ResetPosition_performed;

    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.HotKeyActionMap.ResetPosition.performed -= ResetPosition_performed;
    }

    private void ResetPosition_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        target.position = offset.position;
        target.rotation = offset.rotation;
    }

}
