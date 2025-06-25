using SceneController;
using UnityEngine;
using Zenject;

public class BootSceneInstaller : MonoInstaller
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private FlyResultsContainer saveResult;
    [SerializeField] private PlayerLinker linkPlayer;
    [SerializeField] private PlayerChildrenObjLinker childrenObjLinker;
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle().NonLazy();
        Container.Bind<FlyResultsContainer>().FromInstance(saveResult).AsSingle().NonLazy();
        Container.Bind<PlayerLinker>().FromInstance(linkPlayer).AsSingle().NonLazy();
        Container.Bind<PlayerChildrenObjLinker>().FromInstance(childrenObjLinker).AsSingle().NonLazy();
    }
}
