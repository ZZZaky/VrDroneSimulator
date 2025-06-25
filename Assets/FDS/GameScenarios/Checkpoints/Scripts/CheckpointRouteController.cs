using FDS.FlightScenarios.ScenarioActions;
using FDS.UI;
using Tools.Timer;
using UnityEngine;
using Zenject;

namespace FDS.GameScenarios.Checkpoints
{
    public class CheckpointRouteController : MonoBehaviour
    {

        #region Fields

        private Checkpoint _currentCheckpoint;
        private bool _isRaceStarted = false;
        private bool _isRaceFinished = false;
        private int _currentLap = 0;
        private SceneResetAction _sceneResetAction;
        private Timer _timer;

        #endregion

        #region Constructors

        [Inject]
        private void Construct(SceneResetAction sceneResetAction)
        {
            _sceneResetAction = sceneResetAction;
        }

        #endregion

        #region Properties

        public GameObject RouteCompleteMessage { get; set; }

        public CheckpointGameModeManager.RaceType RaceType { get; set; }

        public CheckpointList CurrentCheckpointList { get; set; }

        public int MaxLaps { get; set; }

        public HomeArrow HomeArrow { get; set; }

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            _sceneResetAction.OnSceneRestarted += ResetCheckpoints;
        }

        private void OnDisable()
        {
            _sceneResetAction.OnSceneRestarted -= ResetCheckpoints;
        }

        private void Start()
        {
            if (RouteCompleteMessage.activeInHierarchy)  RouteCompleteMessage.SetActive(false); 
            InitializeCheckpoints();
            _timer = new Timer();
        }

        #endregion

        #region Public Methods

        public TimerTime GetGameModeTime()
        {
            return _timer.GetTime();
        }

        public bool IsMapComplete()
        {
            return _isRaceFinished;
        }

        public void ExitGameMode()
        {
            _timer.StopTimer();
            if (_currentCheckpoint != null)
            {
                _currentCheckpoint.OnFlight -= CheckpointPass;
                _currentCheckpoint.IsActive = false;
            }
            RouteCompleteMessage.SetActive(false);
        }

        #endregion

        #region Private Methods

        private void InitializeCheckpoints()
        {
            int id = 0;
            foreach (var checkpoint in CurrentCheckpointList.CheckpointsList)
            {
                checkpoint.ID = id;
                checkpoint.IsActive = false;
                id++;
            }
            CurrentCheckpointList.CheckpointsList[0].IsActive = true;
            CurrentCheckpointList.CheckpointsList[0].OnFlight += CheckpointPass;
            _currentCheckpoint = CurrentCheckpointList.CheckpointsList[0];
        }

        private void CheckpointPass(Checkpoint checkpoint)
        {

            int nextCheckpointIndex = (checkpoint.ID + 1) % CurrentCheckpointList.CheckpointsList.Count;

            if (!_isRaceStarted)
            {
                _isRaceStarted = true;
                _timer.StartTimer();
            }

            if (nextCheckpointIndex == 0)
            {
                switch (RaceType)
                {
                    case CheckpointGameModeManager.RaceType.CYCLED:
                        {
                            _currentLap++;
                            _timer.NewIteration(true);
                            if(_currentLap >= MaxLaps)
                            {
                                EndRace();
                                return;
                            }
                            break;
                        }
                    case CheckpointGameModeManager.RaceType.ENDLESS:
                        {
                            _timer.NewIteration(true);
                            break;
                        }
                    case CheckpointGameModeManager.RaceType.REGULAR:
                        {
                            EndRace();
                            return;
                        }

                }
            }

            checkpoint.OnFlight -= CheckpointPass;
            checkpoint.IsActive = false;

            _currentCheckpoint = CurrentCheckpointList.GetCheckpoint(nextCheckpointIndex);
            _currentCheckpoint.IsActive = true;
            _currentCheckpoint.OnFlight += CheckpointPass;

            HomeArrow.SetTarget(_currentCheckpoint.gameObject.transform.position);

        }

        private void ResetCheckpoints()
        {
            if (_isRaceFinished) return;

            _currentLap = 0;
            _isRaceStarted = false;
            _timer.ResetTimer();

            if (_currentCheckpoint == null) return;

            _currentCheckpoint.OnFlight -= CheckpointPass;
            _currentCheckpoint.IsActive = false;

            _currentCheckpoint = CurrentCheckpointList.GetCheckpoint(0);
            _currentCheckpoint.IsActive = true;
            _currentCheckpoint.OnFlight += CheckpointPass;

            HomeArrow.SetTarget(_currentCheckpoint.gameObject.transform.position);
        }

        private void EndRace()
        {
            _currentCheckpoint.OnFlight -= CheckpointPass;
            _currentCheckpoint.IsActive = false;
            _isRaceFinished = true;
            RouteCompleteMessage.SetActive(true);
            _timer.StopTimer();
        }

        #endregion

    }
}

