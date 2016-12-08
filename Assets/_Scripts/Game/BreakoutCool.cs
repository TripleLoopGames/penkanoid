using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using Resources = SRResources.Game;
using PathologicalGames;

[RequireComponent(typeof(InputDetector))]
[RequireComponent(typeof(LevelCreator))]
public class BreakoutCool : MonoBehaviourEx, IHandle<PlayerDeadMessage>
{
    private void Initialize()
    {
        InitializeCamera()
        .InitializeUI()
        .InitializeLevelCreator()
        .InitializeInputDetector()
        .InitializeScenario()
        .InitializePlayer()
        .InitializeBallPool()
        .SetReferences();
        this.inputDetector.EnableInput();
        this.ui.StartCountDown(Config.GameFlow.countDownTime, () => EndGame());
    }

    private BreakoutCool EndGame()
    {
        this.inputDetector.DisableInput();
        this.ui.ShowEnd();
        return this;
    }
    public void Handle(PlayerDeadMessage message)
    {
        EndGame();
    }

    public BreakoutCool ReStart()
    {
        Reset();
        this.inputDetector.EnableInput();
        return this;
    }

    private BreakoutCool Reset()
    {
        this.ballPool.DespawnAll();
        this.player.Reset();
        this.ui.HideEnd();
        this.levelCreator.Reset();
        this.ui.Reset();
        this.levelCreator.GenerateLevel();
        return this;
    }

    private BreakoutCool SetReferences()
    {
        this.ui.SetCamera(this.mainCamera);
        this.player.SetBallPool(this.ballPool);
        return this;
    }

    private BreakoutCool InitializeUI()
    {
        GameObject canvas = SRResources.Game.Ui.Canvas.Instantiate();
        canvas.name = "Canvas";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.ui = canvas.GetComponent<GameUi>();
        this.ui.Initialize(() => ReStart(), Config.Player.InitialHealth);
        if (EventSystem.current == null)
        {
            GameObject eventSystem = SRResources.Game.Ui.EventSystem.Instantiate();
            eventSystem.name = "EventSystemIn";
            eventSystem.transform.SetParent(this.transform, false);
        }
        return this;
    }

    private BreakoutCool InitializePlayer()
    {
        this.player = Resources.Player.Instantiate().GetComponent<Player>();
        this.player.name = "player";
        this.player.transform.SetParent(this.gameObject.transform, false);
        this.player.Initialize();
        return this;
    }

    private BreakoutCool InitializeScenario()
    {
        GameObject scenario = Resources.Scenario.Instantiate();
        scenario.name = "scenario";
        scenario.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private BreakoutCool InitializeCamera()
    {
        this.mainCamera = SRResources.Game.Main_Camera.Instantiate().GetComponent<Camera>();
        this.mainCamera.name = "mainCamera";
        this.mainCamera.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private BreakoutCool InitializeInputDetector()
    {
        this.inputDetector = GetComponent<InputDetector>();
        this.inputDetector.Initialize();
        return this;
    }

    private BreakoutCool InitializeLevelCreator()
    {
        this.levelCreator = GetComponent<LevelCreator>();
        this.levelCreator.Initialize();
        GameObject blocks = this.levelCreator.GenerateLevel();
        blocks.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private BreakoutCool InitializeBallPool()
    {
        this.ballPool = Resources.BallPool.Instantiate().GetComponent<SpawnPool>();
        this.ballPool.name = "BallPool";
        this.ballPool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private void Start()
    {
        Initialize();
    }

    private GameUi ui;
    private InputDetector inputDetector;
    private LevelCreator levelCreator;
    private Camera mainCamera;
    private Player player;
    private SpawnPool ballPool;
}
