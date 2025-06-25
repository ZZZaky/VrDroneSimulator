using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class LayoutRefresher : MonoBehaviour
{
    #region Fields

    private RectTransform _rectTransform;

    #endregion

    #region LifeCycle

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
    }

    #endregion
}
