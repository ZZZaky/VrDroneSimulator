using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HeightSetting : MonoBehaviour
{
    #region Fields

    private XROrigin playerParams;
    [SerializeField] private Slider slider;

    #endregion

    #region Constructors

    [Inject]
    private void Constuct(PlayerLinker playerLink)
    {
        playerParams = playerLink.Player.GetComponent<XROrigin>();
    }

    #endregion

    #region LifeCycle

    private void Start()
    {
        float heightValue = PlayerPrefs.GetFloat("Height", 0.0f);
        playerParams.CameraYOffset = heightValue;
        if (slider != null )
        {
            slider.value = heightValue;
        }
    }

    #endregion

    #region PublicMethods
    public void ValueChanger()
    {
        playerParams.CameraYOffset = slider.value;
        PlayerPrefs.SetFloat("Height", playerParams.CameraYOffset);
    }

    #endregion
}
