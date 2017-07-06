using UnityEngine;
using UnityEngine.EventSystems;
using Resources = SRResources.Intro;
using Exceptions = Config.Exceptions;
using PathologicalGames;
using RSG;
using System;

[RequireComponent(typeof(ChangeSceneComponent))]
[RequireComponent(typeof(InputDetector))]
[RequireComponent(typeof(LevelFactory))]
[RequireComponent(typeof(DataController))]
[RequireComponent(typeof(BackendProxy))]
public class Intro : MonoBehaviourEx
{
    private Intro Initialize()
    {
        InitializeCamera()
        .InitializeDataController();
        DataController dataController = InitializeDataController();
        InitializeBackendProxy(dataController)
        .InitializeTransition()
        .InitializeUI()
        .InitializeSoundCentralPool()
        .SetReferences()
        .IntroStartProcess();

        SoundData playVulkanoid = new SoundData(GetInstanceID(), SRResources.Audio.Music.Volkanoid2MenuTheme, true);
        Messenger.Publish(new PlayMusicMessage(playVulkanoid));
        return this;
    }

    private IPromise IntroStartProcess()
    {
        return new Promise((resolve, reject) =>
        {
            this.backendProxy.Authenticate()
            .Then(() => resolve())
            .Catch((exception) =>
            {
                string exceptionName = exception.Message;
                if (exceptionName == Exceptions.FailedLogin)
                {
                    resolve();
                    return;
                }
                if (exceptionName == Exceptions.RefusedLogin)
                {
                    resolve();
                    return;
                }
                Debug.Log("Unknown error Main Menu" + exceptionName);
            });
        })
        .Then(() => this.holeTransition.Enter())
        .Then(() => this.ui.Show()
        .Then(() => this.holeTransition.Exit())
        .Then(() => Messenger.Publish(new ChangeSceneMessage(SRScenes.Menu))))
        .Then(() =>
        {
            this.inputDetector.EnableInput();
        });
    }

    private Intro InitializeTransition()
    {
        GameObject canvas = SRResources.Game.Canvas_Transition.Instantiate();
        canvas.name = "Canvas_Transition";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.holeTransition = canvas.GetComponentInChildren<HoleTransition>();
        this.holeTransition.Initialize(Color.black, false);
        return this;
    }

    private Intro InitializeUI()
    {
        GameObject canvas = Resources.Ui.Canvas.Instantiate();
        canvas.name = "Canvas";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.ui = canvas.GetComponent<IntroUi>();
        this.ui.Initialize();

        if (EventSystem.current == null)
        {
            GameObject eventSystem = Resources.Ui.EventSystem.Instantiate();
            eventSystem.name = "EventSystemIn";
            eventSystem.transform.SetParent(this.transform, false);
        }
        return this;
    }

    private Intro InitializeCamera()
    {
        this.mainCamera = Resources.Main_Camera.Instantiate().GetComponent<Camera>();
        this.mainCamera.name = "mainCamera";
        this.mainCamera.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private Intro InitializeSoundCentralPool()
    {
        this.soundCentralPool = SRResources.Audio.SoundCentralPool.Instantiate().GetComponent<SoundCentralPool>();
        this.soundCentralPool.name = "SoundCentralPool";
        this.soundCentralPool.Initialize();
        this.soundCentralPool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private Intro InitializeBackendProxy(DataController dataController)
    {
        // TODO: should only be called once per game!
        this.backendProxy = GetComponent<BackendProxy>();
        this.backendProxy.Initialize(dataController);
        return this;
    }

    private DataController InitializeDataController()
    {
        this.dataController = GetComponent<DataController>();
        this.dataController.Initialize();
        return this.dataController;
    }

    private Intro SetReferences()
    {
        this.ui.SetCamera(this.mainCamera);
        return this;
    }

    private void Start()
    {
        Initialize();
    }

    private SoundCentralPool soundCentralPool;
    private Player player;
    private InputDetector inputDetector;
    private IntroUi ui;
    private Camera mainCamera;
    private SpawnPool ballPool;
    private SpawnPool ballParticlePool;
    private HoleTransition holeTransition;
    private BackendProxy backendProxy;
    private LevelFactory levelFactory;
    private DataController dataController;

}
