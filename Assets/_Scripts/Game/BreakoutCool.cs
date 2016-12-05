using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using Resources = SRResources.Game;

[RequireComponent(typeof(InputDetector))]
[RequireComponent(typeof(LevelCreator))]
public class BreakoutCool : MonoBehaviourEx, IHandle<PlayerDeadMessage>
{
    public void Handle(PlayerDeadMessage message)
    {
        this.inputDetector.DisableInput();
        this.ui.ShowEnd();
    }

    public BreakoutCool ReStart()
    {
        Debug.Log("Restarting...");
        return this;
    }

    private void Initialize()
    {
        InitializeCamera()
        .InitializeUI()
        .InitializeLevelCreator()
        .InitializeInputDetector()
        .InitializeScenario()
        .InitializePlayer()
        .SetReferences();
        this.inputDetector.EnableInput();
    }

    private BreakoutCool Reset()
    {
        this.player.Reset();
        return this;
    }

    private BreakoutCool SetReferences()
    {
        this.ui.SetCamera(this.mainCamera);
        return this;
    }

    private BreakoutCool InitializeUI()
    {
        GameObject canvas = SRResources.Game.Ui.Canvas.Instantiate();
        canvas.name = "Canvas";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.ui = canvas.GetComponent<GameUi>();
        this.ui.Initialize(() => ReStart());
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
        GameObject blocks = this.levelCreator.InitializeBlocks();
        blocks.transform.SetParent(this.gameObject.transform, false);
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
}
