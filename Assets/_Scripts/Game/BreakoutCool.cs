using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Resources = SRResources.Game;
using PathologicalGames;
using DG.Tweening;
using RSG;
using Exceptions = Config.Exceptions;

[RequireComponent(typeof(InputDetector))]
[RequireComponent(typeof(LevelFactory))]
[RequireComponent(typeof(ChangeSceneComponent))]
[RequireComponent(typeof(DataController))]
[RequireComponent(typeof(BackendProxy))]
public class BreakoutCool : MonoBehaviourEx, IHandle<PlayerDeadMessage>
{
    private void Initialize()
    {
        InitializeCamera();
        DataController dataController = InitializeDataController();
        InitializeBackendProxy(dataController)
        .InitializeTransition()
        .InitializeUI()
        .InitializeLevelCreator()
        .InitializeInputDetector()
        .InitializeSoundCentralPool()
        .InitializePlayer()
        .InitializeBallPool()
        .InitializeBallParticlePool()
        .SetReferences()
        .SetCollisionsBetweenLayers()
        .InitializeWorldProgress()
        .SetExitAction();

        // create world
        this.worldStage = this.worldProgress.GetFirstStage(this.dataController.GetCurrentWorldName());
        this.currentLevel = this.GenerateAndAddLevel(this.worldStage);
        // Init (ingored if multipel times)
        DOTween.Init();
        this.sceneTransition.Enter().Then(() => StartNewGame());
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
        SoundData playVulkanoid = new SoundData(GetInstanceID(), SRResources.Audio.Music.VolkanoidTheme, true);
        Messenger.Publish(new PlayMusicMessage(playVulkanoid));
        this.dataController.AddPlayerGameTries(1);
        this.inputDetector.EnableInput();
        this.gameUI.StartCountDown(Config.GameFlow.countDownTime, () => EndGame());
        return this;
    }

    private BreakoutCool StartNewLevel()
    {
        this.inputDetector.EnableInput();
        this.gameUI.ReStartCountDown();
        return this;
    }

    private BreakoutCool WinGame()
    {
        this.currentLevel.EnableIgnoreCollisionResult();
        this.inputDetector.DisableInput();
        this.currentLevel.DestroyPickUps();
        this.gameUI.StopCountDown();
        this.player.StopInvulerability();
        this.player.BlockInteractions();
        if (!this.worldStage.IsLast)
        {
            this.gameUI.ShowWinLevel()
                .Then(() =>
                {
                    this.gameUI.MakeNonInteractable();
                    return this.sceneTransition.Exit();
                })
                .Then(() =>
                {
                    LoadNextLevel();
                    return this.sceneTransition.Enter();
                })
                .Then(() =>
                {
                    this.gameUI.MakeInteractable();
                    StartNewLevel();
                });
            return this;
        }
        SoundData stopVulkanoid = new SoundData(GetInstanceID(), SRResources.Audio.Music.VolkanoidTheme);
        Messenger.Publish(new StopMusicMessage(stopVulkanoid));
        SoundData playVictory = new SoundData(GetInstanceID(), SRResources.Audio.Music.VictoryTheme);
        Messenger.Publish(new PlayMusicMessage(playVictory));
        int timeSpent = this.gameUI.GetTimeSpent();
        int tries = this.dataController.GetPlayerGameTries();
        // reset so when player tries again tries re-start
        this.dataController.ResetPlayerGameTries();
        this.gameUI.SetWinGameInfo(timeSpent, tries);
        Promise.All(new Promise((resolve, reject) =>
        {
            this.backendProxy.PublishScore(timeSpent)
            .Then(() => resolve())
            .Catch((exception) =>
            {
                string exceptionName = exception.Message;
                if (exceptionName == Exceptions.FailedPublishScore)
                {
                    resolve();
                    return;
                }
                if (exceptionName == Exceptions.RefusedLogin)
                {
                    resolve();
                    return;
                }
                if (exceptionName == Exceptions.NotLoggedIn)
                {
                    resolve();
                    return;
                }
                Debug.Log($"Unknown error {exceptionName}");
            });
        }), this.gameUI.ShowWinGame())
            .Then(() => this.ReStart());
        return this;
    }

    private BreakoutCool EndGame()
    {
        SoundData stopVulkanoid = new SoundData(GetInstanceID(), SRResources.Audio.Music.VolkanoidTheme);
        Messenger.Publish(new StopMusicMessage(stopVulkanoid));
        SoundData playDefeat = new SoundData(GetInstanceID(), SRResources.Audio.Music.DefeatTheme);
        Messenger.Publish(new PlayMusicMessage(playDefeat));
        this.currentLevel.EnableIgnoreCollisionResult();
        this.inputDetector.DisableInput();
        this.currentLevel.DestroyPickUps();
        this.player.BlockInteractions();
        this.player.StopInvulerability();
        this.gameUI.StopCountDown();
        this.gameUI.ShowEnd()
            .Then(() => ReStart());
        return this;
    }

    private BreakoutCool ReStart()
    {
        FullReset();
        this.worldStage = this.worldProgress.GetFirstStage(this.dataController.GetCurrentWorldName());
        this.currentLevel = this.GenerateAndAddLevel(this.worldStage);
        StartNewGame();
        return this;
    }

    private BreakoutCool LoadNextLevel()
    {
        NextLevelReset();
        this.worldStage = this.worldProgress.GetNextStage(this.worldStage);
        this.currentLevel = this.GenerateAndAddLevel(this.worldStage);
        return this;
    }

    private BreakoutCool FullReset()
    {
        this.ballPool.DespawnAll();
        this.player.FullReset();
        this.gameUI.FullReset();
        this.currentLevel.Destroy();
        this.currentLevel = null;
        return this;
    }

    private BreakoutCool NextLevelReset()
    {
        this.ballPool.DespawnAll();
        this.player.NextLevelReset();
        this.gameUI.NextLevelReset();
        this.currentLevel.Destroy();
        this.currentLevel = null;
        return this;
    }

    private BreakoutCool SetReferences()
    {
        this.gameUI.SetCamera(this.mainCamera);
        this.player.SetBallPool(this.ballPool);
        this.player.SetBallParticlePool(this.ballParticlePool);
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
        GetComponent<ChangeSceneComponent>().setAction((onEnd) => this.sceneTransition.Exit().Then(onEnd));
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
        GameObject canvas = SRResources.Game.Ui.Canvas.Instantiate();
        canvas.name = "Canvas";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.gameUI = canvas.GetComponent<GameUi>();
        this.gameUI.Initialize(Config.Player.InitialHealth, Config.GameFlow.countDownTime);
        if (EventSystem.current == null)
        {
            GameObject eventSystem = SRResources.Game.Ui.EventSystem.Instantiate();
            eventSystem.name = "EventSystemIn";
            eventSystem.transform.SetParent(this.transform, false);
        }
        return this;
    }

    private DataController InitializeDataController()
    {
        this.dataController = GetComponent<DataController>();
        return this.dataController.Initialize();
    }

    private BreakoutCool InitializePlayer()
    {
        this.player = Resources.Player.Instantiate().GetComponent<Player>();
        this.player.name = "player";
        this.player.transform.SetParent(this.gameObject.transform, false);
        this.player.Initialize();
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
        this.levelFactory = GetComponent<LevelFactory>();
        return this;
    }

    private BreakoutCool InitializeBackendProxy(DataController dataController)
    {
        this.backendProxy = GetComponent<BackendProxy>();
        this.backendProxy.Initialize(dataController);
        return this;
    }

    private BreakoutCool InitializeBallPool()
    {
        this.ballPool = Resources.BallPool.Instantiate().GetComponent<SpawnPool>();
        this.ballPool.name = "BallPool";
        this.ballPool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private BreakoutCool InitializeBallParticlePool()
    {
        this.ballParticlePool = Resources.BallParticlePool.Instantiate().GetComponent<SpawnPool>();
        this.ballParticlePool.name = "BallParticlePool";
        this.ballParticlePool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private BreakoutCool InitializeSoundCentralPool()
    {
        this.soundCentralPool = SRResources.Audio.SoundCentralPool.Instantiate().GetComponent<SoundCentralPool>();
        this.soundCentralPool.name = "SoundCentralPool";
        this.soundCentralPool.Initialize();
        this.soundCentralPool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private BreakoutCool InitializeWorldProgress()
    {
        this.worldProgress = new WorldProgress();
        return this;
    }

    private Level GenerateAndAddLevel(WorldStage worldStage)
    {
        Level currentLevel = this.levelFactory.CreateLevel(worldStage);
        currentLevel.SetLevelCleared(() => LevelCleared());
        currentLevel.transform.SetParent(this.gameObject.transform, false);
        return currentLevel;
    }

    private void Start()
    {
        Initialize();
    }

    private GameUi gameUI;
    private SoundCentralPool soundCentralPool;
    private InputDetector inputDetector;
    private Level currentLevel;
    private LevelFactory levelFactory;
    private Camera mainCamera;
    private Player player;
    private SpawnPool ballPool;
    private SpawnPool ballParticlePool;
    private SceneTransition sceneTransition;
    private DataController dataController;
    private BackendProxy backendProxy;
    private WorldProgress worldProgress;
    private WorldStage worldStage;
}
