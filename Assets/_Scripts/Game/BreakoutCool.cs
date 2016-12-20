using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using Resources = SRResources.Game;
using PathologicalGames;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(InputDetector))]
[RequireComponent(typeof(LevelCreator))]
public class BreakoutCool : MonoBehaviourEx, IHandle<PlayerDeadMessage>
{
    private void Initialize()
    {
        InitializeCamera()
        .InitializeTransition()
        .InitializeUI()
        .InitializeLevelCreator()
        .InitializeInputDetector()
        .InitializeScenario()
        .InitializePlayer()
        .InitializeBallPool()
        .SetReferences()
        .SetCollisionsBetweenLayers()
        .SetExitAction();

        // change this.currentLevelId for id in unity prefs
        this.currentLevel = this.GenerateAndAddLevel(this.currentLevelId);

        this.sceneTransition.Enter(() => StartNewGame());
    }

    public void Handle(PlayerDeadMessage message)
    {
        EndGame();
    }

    private BreakoutCool LevelCleared()
    {
        WinGame();
        return this;
    }

    private BreakoutCool StartNewGame()
    {
        this.inputDetector.EnableInput();
        this.ui.StartCountDown(Config.GameFlow.countDownTime, () => EndGame());
        return this;
    }

    private BreakoutCool StartNewLevel()
    {
        this.inputDetector.EnableInput();
        this.ui.ReStartCountDown();
        return this;
    }

    private BreakoutCool WinGame()
    {
        this.currentLevel.EnableIgnoreCollisionResult();
        this.inputDetector.DisableInput();
        this.currentLevel.DestroyPickUps();
        this.ui.StopCountDown();
        // dirty check last level
        if (this.currentLevelId < 3)
        {
            this.ui.ShowWinLevel();
        }
        else
        {
            int time = this.ui.GetTimeSpent();
            this.ui.SetWinGameInfo(time);
            this.ui.ShowWinGame();
        }       
        return this;
    }

    private BreakoutCool EndGame()
    {
        this.currentLevel.EnableIgnoreCollisionResult();
        this.inputDetector.DisableInput();
        this.currentLevel.DestroyPickUps();
        this.ui.ShowEnd();
        this.ui.StopCountDown();
        return this;
    }

    private BreakoutCool ReStart()
    {
        FullReset();
        // we start always at lvl 1 when we restart
        this.currentLevelId = 1;
        this.currentLevel = GenerateAndAddLevel(this.currentLevelId);
        StartNewGame();
        return this;
    }

    private BreakoutCool LoadNextLevel()
    {
        NextLevelReset();
        this.currentLevelId++;
        this.currentLevel = GenerateAndAddLevel(this.currentLevelId);
        return this;
    }

    private BreakoutCool FullReset()
    {
        this.ballPool.DespawnAll();
        this.player.FullReset();
        this.ui.FullReset();
        this.currentLevel.Destroy();
        this.currentLevel = null;
        return this;
    }

    private BreakoutCool NextLevelReset()
    {
        this.ballPool.DespawnAll();
        this.player.NextLevelReset();
        this.ui.NextLevelReset();
        this.currentLevel.Destroy();
        this.currentLevel = null;
        return this;
    }

    private BreakoutCool SetReferences()
    {
        this.ui.SetCamera(this.mainCamera);
        this.player.SetBallPool(this.ballPool);
        return this;
    }

    private BreakoutCool SetCollisionsBetweenLayers()
    {
        Physics2D.IgnoreLayerCollision(SRLayers.Pickups, SRLayers.Balls, true);
        Physics2D.IgnoreLayerCollision(SRLayers.Pickups, SRLayers.Pickups, true);
        Physics2D.IgnoreLayerCollision(SRLayers.Balls, SRLayers.Balls, true);
        Physics2D.IgnoreLayerCollision(SRLayers.Blocks, SRLayers.Pickups, true);
        return this;
    }

    private BreakoutCool SetExitAction()
    {
        GetComponent<ChangeSceneComponent>().setAction((onEnd) => this.sceneTransition.Exit(onEnd));
        return this;
    }

    private BreakoutCool InitializeTransition()
    {
        GameObject canvas = SRResources.Game.Canvas_Transition.Instantiate();
        canvas.name = "Canvas_Transition";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.sceneTransition = canvas.GetComponentInChildren<SceneTransition>();
        this.sceneTransition.Initialize();
        return this;
    }

    private BreakoutCool InitializeUI()
    {
        //TODO Callback Hell :(
        Action nextLevelFlow = () =>
        {
            this.sceneTransition.Exit(() =>
            {
                LoadNextLevel();
                this.sceneTransition.Enter(() => StartNewLevel());
            });
        };

        GameObject canvas = SRResources.Game.Ui.Canvas.Instantiate();
        canvas.name = "Canvas";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.ui = canvas.GetComponent<GameUi>();
        this.ui.Initialize(() => ReStart(), nextLevelFlow, Config.Player.InitialHealth);
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
        return this;
    }

    private BreakoutCool InitializeBallPool()
    {
        this.ballPool = Resources.BallPool.Instantiate().GetComponent<SpawnPool>();
        this.ballPool.name = "BallPool";
        this.ballPool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private Level GenerateAndAddLevel(int id)
    {
        Level currentLevel = this.levelCreator.GenerateLevel(id).GetComponent<Level>();
        currentLevel.Initialize(() => LevelCleared());
        currentLevel.name = "Level-X";
        currentLevel.transform.SetParent(this.gameObject.transform, false);
        return currentLevel;
    }

    private void Start()
    {
        Initialize();
    }

    private int currentLevelId = 1;
    private GameUi ui;
    private InputDetector inputDetector;
    private Level currentLevel;
    private LevelCreator levelCreator;
    private Camera mainCamera;
    private Player player;
    private SpawnPool ballPool;
    private SceneTransition sceneTransition;
}
