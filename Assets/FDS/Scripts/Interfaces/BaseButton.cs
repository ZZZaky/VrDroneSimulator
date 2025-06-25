using FDS.Headset;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseButton : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private UnityEvent onPress;
    
    [SerializeField]
    private UnityEvent onRelease;

    [SerializeField]
    private GameObject button;

    [SerializeField]
    private FpfHeadsetController headsetController;

    private bool _canBePressed = true;

    private GameObject _presser;
    private AudioSource _sound;
    private bool _isPressed;
    protected bool wasPressed;

    #endregion

    #region Life Cycle
    void Start()
    {
        _sound = GetComponent<AudioSource>();
        _isPressed = false;
        wasPressed = false;
    }


    private void OnEnable()
    {
        headsetController.OnHelmetStateChanged += ChangePressState;
    }

    private void OnDisable()
    {
        headsetController.OnHelmetStateChanged -= ChangePressState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canBePressed && !_isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.0209772401f, 0);
            _presser = other.gameObject;
            onPress.Invoke();
            _sound.Play();
            _isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_canBePressed && other.gameObject == _presser)
        {
            button.transform.localPosition = new Vector3(0, 0.0416614823f, 0);
            onRelease.Invoke();
            _isPressed = false;
            if (!wasPressed)
            {
                wasPressed = true;
            }
            else { wasPressed = false; }
        }
    }

    #endregion

    #region Private Methods

    private void ChangePressState(bool value)
    {
        _canBePressed = value;
    }

    #endregion

}
