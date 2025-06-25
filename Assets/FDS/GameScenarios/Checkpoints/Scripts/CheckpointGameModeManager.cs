using FDS.UI.Views;
using SceneController;
using UnityEngine;
using Zenject;

namespace FDS.GameScenarios.Checkpoints
{
    public class CheckpointGameModeManager : MonoBehaviour
    {

        #region Fields

        [SerializeField] private TimerUI timerUI;
        [SerializeField] private CheckpointRouteController checkpointRouteController;

        [Header("Race settings")]
        [SerializeField] private int maxLaps;
        [SerializeField] private CheckpointList checkpointList;
        [SerializeField] private RaceType raceType;

        [Header("MapIndex")]
        [SerializeField] private int mapIndex;

        private UIElementsHolder _uiElementsHolder;
        private SceneLoader _sceneLoader;
        private FlyResultsContainer _saveResult;

        #endregion

        #region Constructors

        [Inject]
        private void Construct(UIElementsHolder uiElementsHolder, SceneLoader sceneLoader, FlyResultsContainer saveResult)
        {
            _uiElementsHolder = uiElementsHolder;
            _sceneLoader = sceneLoader;
            _saveResult = saveResult;
        }

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            _sceneLoader.OnGameModeExited += ExitGameMode;
        }

        private void OnDisable()
        {
            _sceneLoader.OnGameModeExited -= ExitGameMode;
        }

        private void Awake()
        {
            checkpointRouteController.RouteCompleteMessage = _uiElementsHolder.RouteCompleteMessage;
            checkpointRouteController.RaceType = raceType;
            checkpointRouteController.MaxLaps = maxLaps;
            checkpointRouteController.CurrentCheckpointList = checkpointList;
            checkpointRouteController.HomeArrow = _uiElementsHolder.Arrow;

            timerUI.TimerText = _uiElementsHolder.ScenarioTimerText;
            timerUI.CheckpointRouteController = checkpointRouteController;

            _uiElementsHolder.Arrow.ToggleArrow(true);
            _uiElementsHolder.Arrow.SetTarget(checkpointList.GetCheckpoint(0).gameObject.transform.position);
        }

        #endregion

        #region Private Methods

        private void ExitGameMode()
        {
            if (checkpointRouteController.IsMapComplete())
            {
                _saveResult.AddRecord(mapIndex, checkpointRouteController.GetGameModeTime().ToString());
            }
            checkpointRouteController.ExitGameMode();
            timerUI.ClearTimerUI();
            _uiElementsHolder.Arrow.ToggleArrow(false);
        } 

        #endregion

        #region Nested types

        public enum RaceType
        {
            ENDLESS, CYCLED, REGULAR
        }

        #endregion

    }
}

