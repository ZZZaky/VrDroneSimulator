using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForOnTutorial : MonoBehaviour
{
    #region Fileds
    [SerializeField] private GameObject tutorial;
    #endregion
    #region PublicMethods
    public void TurnOnTutorial()
    {
       tutorial.SetActive(!tutorial.activeInHierarchy);
    }
    #endregion
}
