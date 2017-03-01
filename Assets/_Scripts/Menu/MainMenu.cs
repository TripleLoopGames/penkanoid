using UnityEngine;
using UnityEngine.EventSystems;
using Resources = SRResources.Menu;
using PathologicalGames;

[RequireComponent(typeof(ChangeSceneComponent))]
public class MainMenu : MonoBehaviour
{
    private MainMenu Initialize()
    {
        InitializeCamera()
        .InitializeBackendProxy()
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
        .SetExitAction();
        this.backendProxy.Authenticate()
        .Then(() => this.sceneTransition.Enter())
        .Then(() => StartMenu());
        return this;
    }

    private MainMenu StartMenu()
    {
        this.inputDetector.EnableInput();
        this.player.BlockInteractions();
        return this;
    }

    private MainMenu InitializeTransition()
    {
        GameObject canvas = SRResources.Game.Canvas_Transition.Instantiate();
        canvas.name = "Canvas_Transition";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.sceneTransition = canvas.GetComponentInChildren<SceneTransition>();
        this.sceneTransition.Initialize();
        return this;
    }

    private MainMenu InitializeUI()
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

    private MainMenu InitializeCamera()
    {
        this.mainCamera = Resources.Main_Camera.Instantiate().GetComponent<Camera>();
        this.mainCamera.name = "mainCamera";
        this.mainCamera.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private MainMenu InitializePlayer()
    {
        this.player = SRResources.Game.Player.Instantiate(new Vector2()).GetComponent<Player>();
        this.player.name = "player";
        this.player.transform.SetParent(this.gameObject.transform, false);
        this.player.Initialize();
        return this;
    }

    private MainMenu InitializeInputDetector()
    {
        this.inputDetector = GetComponent<InputDetector>();
        this.inputDetector.Initialize();
        return this;
    }

    private MainMenu InitializeBallPool()
    {
        this.ballPool = SRResources.Game.BallPool.Instantiate().GetComponent<SpawnPool>();
        this.ballPool.name = "BallPool";
        this.ballPool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private MainMenu InitializeBallParticlePool()
    {
        this.ballParticlePool = SRResources.Game.BallParticlePool.Instantiate().GetComponent<SpawnPool>();
        this.ballParticlePool.name = "BallParticlePool";
        this.ballParticlePool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private MainMenu InitializeSoundCentralPool()
    {
        this.soundCentralPool = SRResources.Audio.SoundCentralPool.Instantiate().GetComponent<SoundCentralPool>();
        this.soundCentralPool.name = "SoundCentralPool";
        this.soundCentralPool.Initialize();
        this.soundCentralPool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private MainMenu InitializeScenario()
    {
        GameObject scenario = this.levelFactory.CreateSceneario("basic","basic");
        scenario.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private MainMenu InitializeLevelFactory()
    {
        this.levelFactory = GetComponent<LevelFactory>();
        return this;
    }

    private MainMenu InitializeStartBlock()
    {
        GameObject scenario = Resources.StartBlock.Instantiate();
        scenario.name = "startBlock";
        scenario.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private MainMenu InitializeBackendProxy()
    {
        // TODO: should only be called once per game!
        this.backendProxy = GetComponent<BackendProxy>();
        this.backendProxy.Initialize();
        return this;
    }

    private MainMenu SetReferences()
    {
        this.ui.SetCamera(this.mainCamera);
        this.player.SetBallPool(this.ballPool);
        this.player.SetBallParticlePool(this.ballParticlePool);
        return this;
    }

    private MainMenu SetCollisionsBetweenLayers()
    {
        Physics2D.IgnoreLayerCollision(SRLayers.Balls, SRLayers.Balls, true);
        return this;
    }

    private MainMenu SetExitAction()
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
    private MenuUi ui;
    private Camera mainCamera;
    private SpawnPool ballPool;
    private SpawnPool ballParticlePool;
    private SceneTransition sceneTransition;
    private BackendProxy backendProxy;
    private LevelFactory levelFactory;

}
