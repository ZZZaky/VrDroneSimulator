using FDS.FlightScenarios.ScenarioActions;
using FDS.Quadrocopter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FlightMapSceneContext : MonoInstaller
{
    [SerializeField] private Quadracopter quadracopter;
    [SerializeField] private SceneResetAction sceneResetAction;

    public override void InstallBindings()
    {
        Container.Bind<Quadracopter>().FromInstance(quadracopter).AsSingle().NonLazy();
        Container.Bind<SceneResetAction>().FromInstance(sceneResetAction).AsSingle().NonLazy();
    }
}
