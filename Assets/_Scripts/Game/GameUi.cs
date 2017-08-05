using RSG;
using System;
using System.Collections;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Resources = SRResources.Game.Ui;
using System.Linq;

public class GameUi : MonoBehaviourEx, IHandle<PlayerChangeHealthMessage>, IHandle<ModifyTimeMessage>
{
    public GameUi Initialize(int initialHealth)
    {
        this.initialHealth = initialHealth;
        this.canvasGroup = GetComponent<CanvasGroup>();
        InitializeHealth(initialHealth)
        .InitializeTimer()
        .InitializeGameOverScreen()
        .InitializeWinWorld()
        .InitializeWinLevel()
        .InitializeWinGame();
        return this;
    }

    public void Handle(ModifyTimeMessage message)
    {
        ModifyTime(message.Time);
    }

    public void Handle(PlayerChangeHealthMessage message)
    {
        SetHearts(message.Health, true);
    }

    public GameUi MakeInteractable()
    {
        this.canvasGroup.interactable = true;
        return this;
    }

    public GameUi MakeNonInteractable()
    {
        this.canvasGroup.interactable = false;
        return this;
    }

    public GameUi FullReset()
    {
        SetHearts(this.initialHealth);
        HideEnd();
        HideWinLevel();
        HideWinWorld();
        return this;
    }

    public GameUi NextLevelReset()
    {
        HideEnd();
        HideWinLevel();
        HideWinWorld();
        return this;
    }

    public GameUi StartCountDown(int time, Action onEnd)
    {
        Action<int> onTimerTick = value =>
        {
            this.timeText.text = value.ToString();
        };
        this.timer.StartTimer(time, onTimerTick, onEnd);
        return this;
    }

    public GameUi StopCountDown()
    {
        this.timer.StopTimer();
        return this;
    }

    public GameUi ReStartCountDown()
    {
        this.timer.RestartTimer();
        return this;
    }

    public int GetTimeSpent()
    {
        return this.timer.GetTimeSpent();
    }

    public int GetTimeLeft()
    {
        return this.timer.GetTimeLeft();
    }

    public int GetHeartsLeft()
    {
        return this.hearts.Aggregate(0, (acc, heart) => acc + (heart.activeSelf ? 1 : 0));
    }

    public IPromise<bool> ShowEnd()
    {
        return this.gameOverScreen.Show();
    }

    public GameUi HideEnd()
    {
        this.gameOverScreen.Hide();
        return this;
    }

    public IPromise ShowWinWorld(int time, int health, int score)
    {
        return this.winWorldScreen.Show(time, health, score);
    }

    public GameUi HideWinWorld()
    {
        this.winWorldScreen.Hide();
        return this;
    }

    public IPromise ShowWinLevel()
    {
         return this.winLevel.Show();
    }

    public GameUi HideWinLevel()
    {
        this.winLevel.Hide();
        return this;
    }

    public IPromise ShowWinGame()
    {
        return this.winGameScreen.Show();
    }

    public GameUi HideWinGame()
    {
        this.winGameScreen.Hide();
        return this;
    }

    public GameUi SetWinGameInfo(int time, int tries)
    {
        this.winGameScreen.SetInfo(time, tries);
        return this;
    }

    public GameUi SetCamera(Camera camera)
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = camera;
        // set canvas layer, needs camera
        canvas.sortingLayerID = SRSortingLayers.UI;
        return this;
    }

    private GameUi InitializeGameOverScreen()
    {
        this.gameOverScreen = Resources.GameOverScreen.Instantiate().GetComponent<GameOverScreen>();
        Utils.SetNameAndParent("GameOverScreen", this.gameOverScreen.gameObject, this.gameObject);
        this.gameOverScreen.Initialize();
        this.gameOverScreen.Hide();
        return this;
    }

    private GameUi InitializeWinLevel()
    {
        this.winLevel = Resources.WinLevel.Instantiate().GetComponent<WinLevel>();
        Utils.SetNameAndParent("WinLevel", this.winLevel.gameObject, this.gameObject);
        this.winLevel.Initialize();
        this.winLevel.Hide();
        return this;
    }

    private GameUi InitializeWinWorld()
    {
        this.winWorldScreen = Resources.WinWorld.Instantiate().GetComponent<WinWorldScreen>();
        Utils.SetNameAndParent("WinWorld", this.winWorldScreen.gameObject, this.gameObject);
        this.winWorldScreen.Initialize();
        this.winWorldScreen.Hide();
        return this;
    }

    private GameUi InitializeWinGame()
    {
        this.winGameScreen = Resources.WinGameScreen.Instantiate().GetComponent<WinGameScreen>();
        this.winGameScreen.name = "WinGame";
        this.winGameScreen.transform.SetParent(this.gameObject.transform, false);
        this.winGameScreen.Initialize();
        this.winGameScreen.Hide();
        return this;
    }

    private GameUi InitializeHealth(int initialHealth)
    {
        this.health = Resources.Health.Instantiate();
        this.health.name = "Health";
        this.health.transform.SetParent(this.gameObject.transform, false);
        this.hearts = this.health.GetComponentsInChildren<Transform>()
                            .Select((Transform heartTransform) => heartTransform.gameObject)
                            .Where(heart => heart.CompareTag(SRTags.UiHeart))
                            .ToArray();
        foreach (var heart in hearts)
        {
            heart.SetActive(false);
        }
        
        SetHearts(initialHealth);
        return this;
    }

    private GameUi InitializeTimer()
    {
        GameObject time = Resources.Time.Instantiate();
        this.timer = time.GetComponent<TimerComponent>();
        this.timer.name = "Time";
        this.timer.transform.SetParent(this.gameObject.transform, false);
        this.timeText = time.GetComponentInChildren<Text>();
        this.timeText.text = "";
        return this;
    }

    private void OnMenu()
    {
        Messenger.Publish(new ChangeSceneMessage(SRScenes.Intro));
    }

    private GameUi SetHearts(int health, bool pickHeart = false)
    {
        GameObject[] changedHearts = {};
        int countArray = 0;

        changedHearts = this.hearts.Select((heart) =>
        {
            bool heartState = heart.activeSelf;

            heart.SetActive(countArray < health);

            countArray++;

            if (heart.activeSelf != heartState && countArray <= health)
            {
                return heart;
            }
            return null;
        })
        .Where((heart) => heart != null)
        .ToArray();

        if (changedHearts != null)
        {
            if (!pickHeart)
            {
                animateHearts(changedHearts, 1.0f);
            }
            else
            {
                animateHearts(changedHearts);
            }
        }
        
        return this;
    }

    private GameUi animateHearts(GameObject[] copyHearts, float animationDelay = 0)
    {
        TweenEaseAnimationScaleComponent.CreateSequence("hearts", copyHearts, null, 0.5f, animationDelay);
        return this;
    }

    private GameUi ModifyTime(int time)
    {
        int timeLeft = this.timer.GetTimeLeft();
        this.timer.SetTimeLeft(timeLeft + time);
        TweenEaseAnimationScaleComponent.CreateSequence("timer", new GameObject[] { this.timer.gameObject });

        return this;
    }

    private CanvasGroup canvasGroup;
    private GameOverScreen gameOverScreen;
    private WinLevel winLevel;
    private WinWorldScreen winWorldScreen;
    private WinGameScreen winGameScreen;
    private GameObject health;
    private TimerComponent timer;
    private Text timeText;
    private GameObject[] hearts;
    private int initialHealth;
}
