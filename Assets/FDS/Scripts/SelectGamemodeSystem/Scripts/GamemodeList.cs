using UnityEngine;
using Zenject;
using System.Collections.Generic;
using FDS.SelectGamemodeSystem;
using FDS.UI;

public class GamemodeList : MonoBehaviour
{
    #region Fields

    [SerializeField] private GamemodeData[] data;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject backButtonPrefab;
    [SerializeField] private UISceneMonitor uiSceneMonitor;
    [SerializeField] private LevelLoader levelLoader;

    private FlyResultsContainer _flyResultsContainer;
    private List<GamemodeButton> _buttons = new List<GamemodeButton>();

    #endregion

    #region Constructors

    [Inject]
    private void Construct(FlyResultsContainer flyResultsContainer)
    {
        _flyResultsContainer = flyResultsContainer; 
    }

    #endregion

    #region LifeCycle

    private void OnEnable()
    {
        _flyResultsContainer.OnValueChanged += UpdateRecordValues;
    }

    private void OnDisable()
    {
        _flyResultsContainer.OnValueChanged += UpdateRecordValues;
    }

    private void Start()
    {
        for(int i = 0; i < data.Length; i++)
        {
            GameObject currentButton = Instantiate(buttonPrefab.gameObject, transform);
            if(currentButton.TryGetComponent(out GamemodeButton gamemodeButton))
            {
                gamemodeButton.ButtonText = data[i].name;
                gamemodeButton.ButtonScene = data[i].scene;
                gamemodeButton.sceneID = data[i].sceneID;
                gamemodeButton.RecordText = _flyResultsContainer.GetMapRecord(gamemodeButton.sceneID);
                gamemodeButton.LvlLoader = levelLoader;
                gamemodeButton.UIController = uiSceneMonitor;
                _buttons.Add(gamemodeButton);
            }
        }
        var backButton = Instantiate(backButtonPrefab, transform);
        if(backButton.TryGetComponent(out BackButton bb))
        {
            bb.UIController = uiSceneMonitor;
        }
    }

    #endregion

    #region PrivateMethods

    private void UpdateRecordValues()
    {
        foreach (var button in _buttons)
        {
            button.RecordText = _flyResultsContainer.GetMapRecord(button.sceneID);
        }
    }

    #endregion
}
