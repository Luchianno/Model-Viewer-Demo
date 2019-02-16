using UnityEngine;
using Zenject;
using AsImpL;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    int targetFrameRate = 60;

    [SerializeField]
    Transform previewObject;

    [SerializeField]
    AppSettings settings;

    [SerializeField]
    DummyDataList testModelList;

    [SerializeField]
    GameObject modeListItemPrefab;

    public override void InstallBindings()
    {
        // eh, I'll just leave this here
        Application.targetFrameRate = targetFrameRate;

        // settings
        Container.BindInstance<AppSettings>(settings);

        // define game states here. simple.
        Container.Bind(typeof(IInitializable), typeof(GameState)).To<ListGameState>().AsSingle();
        Container.Bind(typeof(IInitializable), typeof(GameState)).To<PreviewGameState>().AsSingle();
        Container.Bind(typeof(IInitializable), typeof(GameState)).To<LoadingGameState>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

        // TODO: change?
        Container.BindInstance<Transform>(previewObject).WithId("PreviewObject");

        // OBJ importer
        Container.Bind<IModelLoader>().To<ModelLoader>().FromComponentInHierarchy().AsSingle();

        // Model List Providers
        //
        Container.BindInstance<IModelListProvider>(testModelList);

        // UI stuff
        Container.BindInterfacesAndSelfTo<DragArea>().FromComponentInHierarchy(true).AsSingle();

        // Views
        Container.BindInterfacesAndSelfTo<DebugView>().FromComponentInHierarchy(true).AsSingle();
        Container.BindInterfacesAndSelfTo<ModelListView>().FromComponentInHierarchy(true).AsSingle();
        Container.BindInterfacesAndSelfTo<PreviewScreenView>().FromComponentInHierarchy(true).AsSingle();
        Container.BindInterfacesAndSelfTo<ProgressMessageView>().FromComponentInHierarchy(true).AsSingle();
        Container.BindInterfacesAndSelfTo<ErrorMessageView>().FromComponentInHierarchy(true).AsSingle();

        Container.BindFactory<ModelListItemView, ModelListItemView.Factory>().
                    FromComponentInNewPrefab(modeListItemPrefab).
                    UnderTransform(x => FindObjectOfType<ModelListView>().listParent); // if anyone wonders wtf is happening here: https://github.com/modesttree/Zenject/blob/master/Documentation/Factories.md

        // controllers
        Container.BindInterfacesAndSelfTo<ModelListController>().AsSingle();
        Container.BindInterfacesAndSelfTo<CameraZoomController>().FromComponentInHierarchy(true).AsSingle();
    }
}