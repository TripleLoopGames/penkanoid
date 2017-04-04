using UnityEngine;
using UnityEngine.EventSystems;
using Resources = SRResources.Intro;
using Exceptions = Config.Exceptions;
using PathologicalGames;
using RSG;

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
        .InitializeInputDetector()
        .InitializeSoundCentralPool()
        .InitializeLevelFactory()
        .InitializeScenario()
        .InitializePlayer()
        .InitializeBallPool()
        .InitializeBallParticlePool()
        .InitializeStartBlock()
        .SetReferences()
        .SetCollisionsBetweenLayers()
        .SetExitAction()
        .IntroStartProcess();
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
        .Then(() => this.sceneTransition.Enter())
        .Then(() =>
        {
            this.inputDetector.EnableInput();
            this.player.BlockInteractions();
        });
    }

    private Intro InitializeTransition()
    {
        GameObject canvas = SRResources.Game.Canvas_Transition.Instantiate();
        canvas.name = "Canvas_Transition";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.sceneTransition = canvas.GetComponentInChildren<SceneTransition>();
        this.sceneTransition.Initialize();
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

    private Intro InitializePlayer()
    {
        this.player = SRResources.Game.Player.Instantiate(new Vector2()).GetComponent<Player>();
        this.player.name = "player";
        this.player.transform.SetParent(this.gameObject.transform, false);
        this.player.Initialize();
        return this;
    }

    private Intro InitializeInputDetector()
    {
        this.inputDetector = GetComponent<InputDetector>();
        this.inputDetector.Initialize();
        return this;
    }

    private Intro InitializeBallPool()
    {
        this.ballPool = SRResources.Game.BallPool.Instantiate().GetComponent<SpawnPool>();
        this.ballPool.name = "BallPool";
        this.ballPool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private Intro InitializeBallParticlePool()
    {
        this.ballParticlePool = SRResources.Game.BallParticlePool.Instantiate().GetComponent<SpawnPool>();
        this.ballParticlePool.name = "BallParticlePool";
        this.ballParticlePool.transform.SetParent(this.gameObject.transform, false);
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

    private Intro InitializeScenario()
    {
        GameObject scenario = this.levelFactory.CreateSceneario("basic", "basic");
        scenario.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private Intro InitializeLevelFactory()
    {
        this.levelFactory = GetComponent<LevelFactory>();
        return this;
    }

    private Intro InitializeStartBlock()
    {
        StartBlock startBlock = Resources.StartBlock.Instantiate().GetComponent<StartBlock>();
        startBlock.Initialize(() => Messenger.Publish(new ChangeSceneMessage(SRScenes.Menu)));
        startBlock.name = "startBlock";
        startBlock.transform.SetParent(this.gameObject.transform, false);
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
        this.player.SetBallPool(this.ballPool);
        this.player.SetBallParticlePool(this.ballParticlePool);
        return this;
    }

    private Intro SetCollisionsBetweenLayers()
    {
        Physics2D.IgnoreLayerCollision(SRLayers.Balls, SRLayers.Balls, true);
        return this;
    }

    private Intro SetExitAction()
    {
        GetComponent<ChangeSceneComponent>().setAction((onEnd) => this.sceneTransition.Exit().Then(onEnd));
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
    private SceneTransition sceneTransition;
    private BackendProxy backendProxy;
    private LevelFactory levelFactory;
    private DataController dataController;

}
