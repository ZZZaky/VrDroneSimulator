using FDS.FlightScenarios.ScenarioActions;
using FDS.UI.Views;
using UnityEngine;
using Zenject;

public class FlightDroneMapInstaller : MonoInstaller
{
    [SerializeField] private SceneResetAction sceneResetAction;
    [SerializeField] private UIElementsHolder routeCompleteMessage;
    public override void InstallBindings()
    {
        Container.Bind<SceneResetAction>().FromInstance(sceneResetAction).AsSingle().NonLazy();
        Container.Bind<UIElementsHolder>().FromInstance(routeCompleteMessage).AsSingle().NonLazy();
    }
}
