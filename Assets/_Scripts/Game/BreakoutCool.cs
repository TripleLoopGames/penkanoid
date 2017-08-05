using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Resources = SRResources.Game;
using DG.Tweening;
using PathologicalGames;
using RSG;
using Exceptions = Config.Exceptions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Analytics;

[RequireComponent(typeof (InputDetector))]
[RequireComponent(typeof (LevelFactory))]
[RequireComponent(typeof (ChangeSceneComponent))]
[RequireComponent(typeof (DataController))]
[RequireComponent(typeof (BackendProxy))]
public class BreakoutCool : MonoBehaviourEx, IHandle<PlayerDeadMessage> {
  private void Initialize() {
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
      .InitializeWorldProgress();

    // create world
    this.worldStage = this.worldProgress.GetFirstStage(this.dataController.GetCurrentWorldName());
    this.currentLevel = this.GenerateAndAddLevel(this.worldStage);
    // Init (ingored if multipel times)
    DOTween.Init();
    this.fadeTransition.Enter().Then(() => StartNewGame());
  }

  public void Handle(PlayerDeadMessage message) {
    EndGame();
  }

  private BreakoutCool LevelCleared() {
    WinGame();
    return this;
  }

  private BreakoutCool StartNewGame() {
    SoundData playVulkanoid = new SoundData(GetInstanceID(), SRResources.Audio.Music.VolkanoidTheme, true);
    Messenger.Publish(new PlayMusicMessage(playVulkanoid));
    string currentWorldName = this.dataController.GetCurrentWorldName();
    int tries = this.dataController.GetWorldGameTries(currentWorldName);
    this.dataController.SetWorldGameTries(currentWorldName, (tries + 1));
    this.inputDetector.EnableInput();
    this.gameUI.StartCountDown(worldProgress.GetWorldTime(this.worldStage.World), () => EndGame());
    return this;
  }

  private BreakoutCool StartNewLevel() {
    this.inputDetector.EnableInput();
    this.gameUI.ReStartCountDown();
    return this;
  }

  private BreakoutCool StartNewWorld() {
    this.inputDetector.EnableInput();
    this.gameUI.StartCountDown(worldProgress.GetWorldTime(this.worldStage.World), () => EndGame());
    return this;
  }

  private BreakoutCool WinGame() {
    this.currentLevel.EnableIgnoreCollisionResult();
    this.inputDetector.DisableInput();
    this.currentLevel.DestroyPickUps();
    this.gameUI.StopCountDown();
    this.player.StopInvulerability();
    this.player.BlockInteractions();
    //this.worldStage.Level
    int timeSpentInLevel = this.gameUI.GetTimeSpent() - previousLevelTime;
    previousLevelTime += timeSpentInLevel;
    TrackLevelFinished(this.worldStage.Id + 1, this.worldStage.World, timeSpentInLevel, this.gameUI.GetHeartsLeft());
    if (!this.worldStage.IsLast) {
      this.gameUI.ShowWinLevel()
        .Then(() => {
          this.gameUI.MakeNonInteractable();
          return this.holeTransition.Exit();
        })
        .Then(() => {
          LoadNextLevel();
          return this.holeTransition.Enter();
        })
        .Then(() => {
          this.gameUI.MakeInteractable();
          StartNewLevel();
        });
      return this;
    }
    TrackWorldFinished(this.worldStage.World, this.gameUI.GetTimeSpent(), this.gameUI.GetHeartsLeft());

    // stop background music and play win music
    SoundData stopVulkanoid = new SoundData(GetInstanceID(), SRResources.Audio.Music.VolkanoidTheme);
    Messenger.Publish(new StopMusicMessage(stopVulkanoid));
    SoundData playVictory = new SoundData(GetInstanceID(), SRResources.Audio.Music.VictoryTheme);
    Messenger.Publish(new PlayMusicMessage(playVictory));
    string currentWorldName = this.dataController.GetCurrentWorldName();

    // check and save highscore if necessary

    int levelScore = this.gameUI.GetTimeLeft() + (this.gameUI.GetHeartsLeft() * Config.Score.HeartValue);
    int highScore = this.dataController.GetHighScore(currentWorldName);
    if (levelScore > highScore) {
      this.dataController.SetHighScore(currentWorldName, levelScore);
    }
    int tries = this.dataController.GetWorldGameTries(currentWorldName);
    // reset so when player tries again tries are set to 0
    this.dataController.SetWorldGameTries(currentWorldName, 0);
    int timeSpent = this.gameUI.GetTimeSpent();
    this.gameUI.SetWinGameInfo(timeSpent, tries);

    this.gameUI.ShowWinWorld(this.gameUI.GetTimeLeft(), this.gameUI.GetHeartsLeft(), levelScore)
      .Then(() => {
        string nextWorldName = this.worldProgress.GetNextWorld(this.worldStage.World);
        if (nextWorldName == null) {
          BackToMenu();
          return;
        }
        this.dataController.SetWorldLocking(nextWorldName, true);
        NextWorldReset();
        this.worldStage = this.worldProgress.GetFirstStage(nextWorldName);
        this.currentLevel = this.GenerateAndAddLevel(this.worldStage);
        StartNewWorld();
        return;
      });
    return this;
  }

  private BreakoutCool EndGame() {
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
      .Then((restart) => {
        if (!restart) {
          BackToMenu();
        }
        ReStart();
      });
    return this;
  }

  private BreakoutCool ReStart() {
    this.holeTransition.Exit().Then(() => {
      FullReset();
      this.worldStage = this.worldProgress.GetFirstStage(this.worldStage.World);
      this.currentLevel = this.GenerateAndAddLevel(this.worldStage);
      this.holeTransition.Enter().Then(() => StartNewGame());
    });
    return this;
  }

  private BreakoutCool BackToMenu() {
    this.holeTransition.Exit()
      .Then(() => Messenger.Publish(new ChangeSceneMessage(SRScenes.Menu)));
    return this;
  }

  private BreakoutCool LoadNextLevel() {
    NextLevelReset();
    this.worldStage = this.worldProgress.GetNextStage(this.worldStage);
    this.currentLevel = this.GenerateAndAddLevel(this.worldStage);
    return this;
  }

  private BreakoutCool FullReset() {
    previousLevelTime = 0;
    this.ballPool.DespawnAll();
    this.player.FullReset();
    this.gameUI.FullReset();
    this.currentLevel.Destroy();
    this.currentLevel = null;
    return this;
  }

  private BreakoutCool NextWorldReset() {
    previousLevelTime = 0;
    this.ballPool.DespawnAll();
    this.player.NextLevelReset();
    this.gameUI.NextLevelReset();
    this.currentLevel.Destroy();
    this.currentLevel = null;
    return this;
  }

  private BreakoutCool NextLevelReset() {
    this.ballPool.DespawnAll();
    this.player.NextLevelReset();
    this.gameUI.NextLevelReset();
    this.currentLevel.Destroy();
    this.currentLevel = null;
    return this;
  }

  private BreakoutCool SetReferences() {
    this.gameUI.SetCamera(this.mainCamera);
    this.player.SetBallPool(this.ballPool);
    this.player.SetBallParticlePool(this.ballParticlePool);
    return this;
  }

  private BreakoutCool SetCollisionsBetweenLayers() {
    Physics2D.IgnoreLayerCollision(SRLayers.Pickups, SRLayers.Balls, true);
    Physics2D.IgnoreLayerCollision(SRLayers.Pickups, SRLayers.Pickups, true);
    Physics2D.IgnoreLayerCollision(SRLayers.Balls, SRLayers.Balls, true);
    Physics2D.IgnoreLayerCollision(SRLayers.Blocks, SRLayers.Pickups, true);
    return this;
  }

  private BreakoutCool InitializeTransition() {
    GameObject canvas = SRResources.Game.Canvas_Transition.Instantiate();
    canvas.name = "Canvas_Transition";
    canvas.transform.SetParent(this.gameObject.transform, false);
    this.holeTransition = canvas.GetComponentInChildren<HoleTransition>();
    this.holeTransition.Initialize(Color.black, true);
    this.fadeTransition = canvas.GetComponentInChildren<FadeTransition>();
    this.fadeTransition.Initialize(Color.white, false);
    this.uiDamageFeed = canvas.GetComponentInChildren<UIDamageFeed>();
    this.uiDamageFeed.Initialize();
    return this;
  }

  private BreakoutCool InitializeUI() {
    GameObject canvas = SRResources.Game.Ui.Canvas.Instantiate();
    canvas.name = "Canvas";
    canvas.transform.SetParent(this.gameObject.transform, false);
    this.gameUI = canvas.GetComponent<GameUi>();
    this.gameUI.Initialize(Config.Player.InitialHealth);
    if (EventSystem.current == null) {
      GameObject eventSystem = SRResources.Game.Ui.EventSystem.Instantiate();
      eventSystem.name = "EventSystemIn";
      eventSystem.transform.SetParent(this.transform, false);
    }
    return this;
  }

  private DataController InitializeDataController() {
    this.dataController = GetComponent<DataController>();
    return this.dataController.Initialize();
  }

  private BreakoutCool InitializePlayer() {
    this.player = Resources.Player.Instantiate().GetComponent<Player>();
    this.player.name = "player";
    this.player.transform.SetParent(this.gameObject.transform, false);
    this.player.Initialize();
    return this;
  }

  private BreakoutCool InitializeCamera() {
    this.mainCamera = SRResources.Game.Main_Camera.Instantiate().GetComponent<Camera>();
    this.mainCamera.name = "mainCamera";
    this.mainCamera.transform.SetParent(this.gameObject.transform, false);
    return this;
  }

  private BreakoutCool InitializeInputDetector() {
    this.inputDetector = GetComponent<InputDetector>();
    this.inputDetector.Initialize();
    return this;
  }

  private BreakoutCool InitializeLevelCreator() {
    this.levelFactory = GetComponent<LevelFactory>();
    return this;
  }

  private BreakoutCool InitializeBackendProxy(DataController dataController) {
    this.backendProxy = GetComponent<BackendProxy>();
    this.backendProxy.Initialize(dataController);
    return this;
  }

  private BreakoutCool InitializeBallPool() {
    this.ballPool = Resources.BallPool.Instantiate().GetComponent<SpawnPool>();
    this.ballPool.name = "BallPool";
    this.ballPool.transform.SetParent(this.gameObject.transform, false);
    return this;
  }

  private BreakoutCool InitializeBallParticlePool() {
    this.ballParticlePool = Resources.BallParticlePool.Instantiate().GetComponent<SpawnPool>();
    this.ballParticlePool.name = "BallParticlePool";
    this.ballParticlePool.transform.SetParent(this.gameObject.transform, false);
    return this;
  }

  private BreakoutCool InitializeSoundCentralPool() {
    this.soundCentralPool = SRResources.Audio.SoundCentralPool.Instantiate().GetComponent<SoundCentralPool>();
    this.soundCentralPool.name = "SoundCentralPool";
    this.soundCentralPool.Initialize();
    this.soundCentralPool.transform.SetParent(this.gameObject.transform, false);
    return this;
  }

  private BreakoutCool InitializeWorldProgress() {
    this.worldProgress = new WorldProgress();
    return this;
  }

  private Level GenerateAndAddLevel(WorldStage worldStage) {
    Level currentLevel = this.levelFactory.CreateLevel(worldStage);
    currentLevel.SetLevelCleared(() => LevelCleared());
    currentLevel.transform.SetParent(this.gameObject.transform, false);
    return currentLevel;
  }

  private BreakoutCool TrackLevelFinished(int level, string world, int timeTook, int healthLeft) {
    Dictionary<string, object> dataToSend = new Dictionary<string, object> { { "timeTook", timeTook },
      { "health", healthLeft },
    };
    Analytics.CustomEvent("Finished Level: " + level + " World: " + world, dataToSend);
    return this;
  }

  private BreakoutCool TrackWorldFinished(string world, int timeTook, int healthLeft) {
    Dictionary<string, object> dataToSend = new Dictionary<string, object> { { "timeTook", timeTook },
      { "health", healthLeft },
    };
    Analytics.CustomEvent("Finished World: " + world, dataToSend);
    return this;
  }

  private void Start() {
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
  private HoleTransition holeTransition;
  private FadeTransition fadeTransition;
  private UIDamageFeed uiDamageFeed;
  private DataController dataController;
  private BackendProxy backendProxy;
  private WorldProgress worldProgress;
  private WorldStage worldStage;
  private int previousLevelTime = 0;
}