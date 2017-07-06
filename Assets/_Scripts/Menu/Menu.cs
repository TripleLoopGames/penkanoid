using Resources = SRResources.Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using RSG;
using DG.Tweening;
using System;

[RequireComponent(typeof(DataController))]
[RequireComponent(typeof(BackendProxy))]
[RequireComponent(typeof(ChangeSceneComponent))]
public class Menu : MonoBehaviourEx
{
    private Menu Initialize()
    {
        InitializeCamera();
        InitializeTransition();
        DataController dataController = InitializeDataController();
        BackendProxy backendProxy = InitializeBackendProxy(dataController);
        WorldSave[] worldSaves = dataController.GetWorldSaves();
        InitializeUI(worldSaves,
                     () => Application.Quit(),
                     () => backendProxy.ShowLeaderboard()
                    );
        InitializeSoundCentralPool();
        this.MenuProcess();

        SoundData playVulkanoid = new SoundData(GetInstanceID(), SRResources.Audio.Music.Volkanoid2MenuTheme, true);
        Messenger.Publish(new PlayMusicMessage(playVulkanoid));
        return this;
    }

    private Menu MenuProcess()
    {
        this.ui.MakeNonInteractable();
        Promise.All
            (
            this.fadeTransition.Enter(),
            this.ui.EnterAnimation()
            )
            .Then(() =>
            {
                this.ui.MakeInteractable();
                return this.ui.WaitForNextLevel();
            })
            .Then((world) =>
            {
                this.ui.MakeNonInteractable();
                this.dataController.SetCurrentWorldName(world);
                this.fadeTransition.SetColor(Color.white);
                return Promise.All
                    (
                    this.ui.ZoomIn(1.5f),
                    this.fadeTransition.Exit(1.5f)
                    );
            })
            .Then(() => Messenger.Publish(new ChangeSceneMessage(SRScenes.Game)));
        return this;
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

    private BackendProxy InitializeBackendProxy(DataController dataController)
    {
        this.backendProxy = GetComponent<BackendProxy>();
        return this.backendProxy.Initialize(dataController);
    }

    private Menu InitializeTransition()
    {
        GameObject canvas = SRResources.Game.Canvas_Transition.Instantiate();
        canvas.name = "Canvas_Transition";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.fadeTransition = canvas.GetComponentInChildren<FadeTransition>();
        this.fadeTransition.Initialize(Color.black, false);
        this.holeTransition = canvas.GetComponentInChildren<HoleTransition>();
        this.holeTransition.Initialize(Color.black, true);
        return this;
    }

    private Menu InitializeUI(WorldSave[] worldSaves, Action closeGame, Action openLeaderboard)
    {
        GameObject canvas = Resources.Ui.Canvas.Instantiate();
        canvas.name = "Canvas";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.ui = canvas.GetComponent<MenuUi>();
        this.ui.Initialize(worldSaves, closeGame, openLeaderboard);
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
    private HoleTransition holeTransition;
    private FadeTransition fadeTransition;
    private SoundCentralPool soundCentralPool;
    private BackendProxy backendProxy;
}
