using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerChildrenObjLinker : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor playerSocket;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject menuScreen;
    public XRSocketInteractor Socket { get { return playerSocket; } }
    public Camera Camera { get { return playerCamera; } }
    public GameObject LoadingScreen { get { return loadingScreen; } }

    public GameObject MenuScreen { get { return loadingScreen; } }
}
