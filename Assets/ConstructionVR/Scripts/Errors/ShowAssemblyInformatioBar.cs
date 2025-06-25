using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowAssemblyInformatioBar : MonoBehaviour
{
    [SerializeField]
    private AssemblyBaseLogic assemblyBaseLogic;

    [Header("Pages")]
    [SerializeField]
    private GameObject assemblyProgressPage;

    [SerializeField]
    private GameObject errorTestPage;

    [SerializeField]
    private GameObject errorInformationPage;

    [Header("Text link")]
    [SerializeField]
    private TextMeshProUGUI errorText;

    [Header("Assembly progress")]
    [SerializeField]
    private TextMeshProUGUI assemblyProgressText;

    [SerializeField]
    private Image loadBarAssemblyImage;

    [Header("Wire Connection progress")]
    [SerializeField]
    private TextMeshProUGUI connectionProgressText;

    [SerializeField]
    private Image loadBarConnectionImage;

    private Dictionary<string, GameObject> _pages;

    private bool _hasAssemblyFinish = false;
    private bool _hasConnectionFinish = false;

    #region LifeCycle

    private void Awake()
    {
        _pages = new Dictionary<string, GameObject>(3);
        _pages.Add(nameof(assemblyProgressPage), assemblyProgressPage);
        _pages.Add(nameof(errorTestPage), errorTestPage);
        _pages.Add(nameof(errorInformationPage), errorInformationPage);
    }

    private void OnEnable()
    {
        assemblyBaseLogic.OnChangeAssemblyProgress.AddListener(OnUpdateAssemblyProgress);
        assemblyBaseLogic.OnChangeConnectionProgress.AddListener(OnUpdateConnectionProgress);
    }

    private void OnDisable()
    {
        assemblyBaseLogic.OnChangeAssemblyProgress.RemoveListener(OnUpdateAssemblyProgress);
    }

    #endregion

    public void ShowMessageErrors()
    {
       // errorText.text = message;
        ShowPage(nameof(errorInformationPage));
    }

    public void ShowAssemblyProgress(float progress)
    {
        assemblyProgressText.text = (progress * 100).ToString("f0") + "%";
        loadBarAssemblyImage.fillAmount = Mathf.Clamp01(progress);
    }

    public void ShowConnectionProgress(float progress)
    {
        connectionProgressText.text = (progress * 100).ToString("f0") + "%";
        loadBarConnectionImage.fillAmount = Mathf.Clamp01(progress);
    }

    #region PrivateMethods

    private void OnUpdateConnectionProgress(float arg0)
    {
        ShowConnectionProgress(arg0);

        _hasConnectionFinish = arg0 >= 1;
        CheckFinish();
    }

    private void OnUpdateAssemblyProgress(float progress)
    {
        ShowAssemblyProgress(progress);

        _hasAssemblyFinish = progress >= 1;
        CheckFinish();
    }

    private void CheckFinish()
    {
        if(_hasConnectionFinish && _hasAssemblyFinish)
        {
            ShowPage(nameof(errorTestPage));
        }
        else
        {
            ShowPage(nameof(assemblyProgressPage));
        }
    }

    private void ShowPage(string pageName)
    {
        if (!_pages.ContainsKey(pageName))
        {
            Debug.LogError("Not found page!");
            return;
        }

        foreach (var page in _pages)
        {
            if (page.Key == pageName)
            {
                page.Value.gameObject.SetActive(true);
            }
            else
            {
                page.Value.gameObject.SetActive(false);
            }
        }

    }

    #endregion
}
