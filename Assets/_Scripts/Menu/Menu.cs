using Resources = SRResources.Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using RSG;

[RequireComponent(typeof(DataController))]
[RequireComponent(typeof(BackendProxy))]
[RequireComponent(typeof(ChangeSceneComponent))]
public class Menu : MonoBehaviourEx
{
    private Menu Initialize()
    {
        InitializeCamera();
        DataController dataController = InitializeDataController();
        InitializeBackendProxy(dataController)
        .InitializeTransition()
        .InitializeUI()
        .InitializeSoundCentralPool()
        .MenuProcess();
        return this;
    }

    private IPromise<string> MenuProcess()
    {
        this.ui.MakeNonInteractable();
        return this.sceneTransition.Enter()
            .Then(() => 
            {
                this.ui.MakeInteractable();
                return this.ui.WaitForNextLevel();
            })
            .Then((world) =>
            {
                this.ui.MakeNonInteractable();
                this.dataController.SetCurrentWorldName(world);
                Messenger.Publish(new ChangeSceneMessage(SRScenes.Game));
            });
    }

    private Menu InitializeCamera()
    {
        this.mainCamera = Resources.Main_Camera.Instantiate().GetComponent<Camera>();
        this.mainCamera.name = "mainCamera";
        this.mainCamera.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private DataController InitializeDataController()
    {
        this.dataController = GetComponent<DataController>();
        this.dataController.Initialize();
        return this.dataController;
    }

    private Menu InitializeBackendProxy(DataController dataController)
    {
        this.backendProxy = GetComponent<BackendProxy>();
        this.backendProxy.Initialize(dataController);
        return this;
    }

    private Menu InitializeTransition()
    {
        GameObject canvas = SRResources.Game.Canvas_Transition.Instantiate();
        canvas.name = "Canvas_Transition";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.sceneTransition = canvas.GetComponentInChildren<SceneTransition>();
        this.sceneTransition.Initialize();
        GetComponent<ChangeSceneComponent>().setAction((onEnd) => this.sceneTransition.Exit().Then(onEnd));
        return this;
    }

    private Menu InitializeUI()
    {
        GameObject canvas = Resources.Ui.Canvas.Instantiate();
        canvas.name = "Canvas";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.ui = canvas.GetComponent<MenuUi>();
        this.ui.Initialize();
        if (EventSystem.current == null)
        {
            GameObject eventSystem = Resources.Ui.EventSystem.Instantiate();
            eventSystem.name = "EventSystemIn";
            eventSystem.transform.SetParent(this.transform, false);
        }
        return this;
    }

    private Menu InitializeSoundCentralPool()
    {
        this.soundCentralPool = SRResources.Audio.SoundCentralPool.Instantiate().GetComponent<SoundCentralPool>();
        this.soundCentralPool.name = "SoundCentralPool";
        this.soundCentralPool.Initialize();
        this.soundCentralPool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private void Start()
    {
        Initialize();
    }

    private MenuUi ui;
    private Camera mainCamera;
    private DataController dataController;
    private SceneTransition sceneTransition;
    private SoundCentralPool soundCentralPool;
    private BackendProxy backendProxy;
}
