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
        .InitializeTransition()
        .InitializeUI()
        .InitializeInputDetector()
        .InitializeScenario()
        .InitializePlayer()
        .InitializeBallPool()
        .InitializeStartBlock()
        .SetReferences()
        .SetCollisionsBetweenLayers()
        .SetExitAction();
        this.sceneTransition.Enter(() => StartMenu());
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
        this.player = SRResources.Game.Player.Instantiate().GetComponent<Player>();
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

    private MainMenu InitializeScenario()
    {
        GameObject scenario = Resources.Scenario.Instantiate();
        scenario.name = "scenario";
        scenario.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private MainMenu InitializeStartBlock()
    {
        GameObject scenario = Resources.StartBlock.Instantiate();
        scenario.name = "startBlock";
        scenario.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private MainMenu SetReferences()
    {
        this.ui.SetCamera(this.mainCamera);
        this.player.SetBallPool(this.ballPool);
        return this;
    }

    private MainMenu SetCollisionsBetweenLayers()
    {
        Physics2D.IgnoreLayerCollision(SRLayers.Balls, SRLayers.Balls, true);
        return this;
    }

    private MainMenu SetExitAction()
    {
        GetComponent<ChangeSceneComponent>().setAction((onEnd) => this.sceneTransition.Exit(onEnd));
        return this;
    }

    private void Start()
    {
        Initialize();
    }

    private Player player;
    private InputDetector inputDetector;
    private MenuUi ui;
    private Camera mainCamera;
    private SpawnPool ballPool;
    private SceneTransition sceneTransition;

}
