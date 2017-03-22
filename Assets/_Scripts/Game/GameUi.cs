using RSG;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Resources = SRResources.Game.Ui;

public class GameUi : MonoBehaviourEx, IHandle<PlayerChangeHealthMessage>, IHandle<ModifyTimeMessage>
{
    public GameUi Initialize(int initialHealth, int startTime)
    {
        this.easeScaleAnimation = GetComponent<TweenEaseAnimationScaleComponent>();
        this.initialHealth = initialHealth;
        this.canvasGroup = GetComponent<CanvasGroup>();
        InitializeHealth(initialHealth)
        .InitializeTimer(startTime)
        .InitializeGameOverScreen()
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
        SetHearts(message.Health);
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
        HideWinGame();
        return this;
    }

    public GameUi NextLevelReset()
    {
        HideEnd();
        HideWinLevel();
        HideWinGame();
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

    public IPromise ShowEnd()
    {
        return this.gameOverScreen.Show();
    }

    public GameUi HideEnd()
    {
        this.gameOverScreen.Hide();
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
        SetHearts(initialHealth);
        return this;
    }

    private GameUi InitializeTimer(int startTime)
    {
        GameObject time = Resources.Time.Instantiate();
        this.timer = time.GetComponent<TimerComponent>();
        this.timer.name = "Time";
        this.timer.transform.SetParent(this.gameObject.transform, false);
        this.timeText = time.GetComponentInChildren<Text>();
        this.timeText.text = startTime.ToString();
        return this;
    }

    private void OnMenu()
    {
        Messenger.Publish(new ChangeSceneMessage(SRScenes.Menu));
    }

    private GameUi SetHearts(int health)
    {
        Sequence mySequence = DOTween.Sequence();
        float insertAnimationTime = 0;

        for (int index = 0; index < this.hearts.Length; index++)
        {
            bool isActive = this.hearts[index].activeSelf;

            this.hearts[index].SetActive(index < health);

            if (isActive != this.hearts[index].activeSelf)
            {
                mySequence.Insert(insertAnimationTime, easeScaleAnimation.createTweenEaseAnimation(this.hearts[index].gameObject));
                insertAnimationTime += 0.05f;
            }
        }
        return this;
    }

    private GameUi ModifyTime(int time)
    {
        int timeLeft = this.timer.GetTimeLeft();
        this.timer.SetTimeLeft(timeLeft + time);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Insert(0, easeScaleAnimation.createTweenEaseAnimation(this.timer.gameObject));
        return this;
    }

    private CanvasGroup canvasGroup;
    private GameOverScreen gameOverScreen;
    private WinLevel winLevel;
    private WinGameScreen winGameScreen;
    private GameObject health;
    private TimerComponent timer;
    private Text timeText;
    private GameObject[] hearts;
    private int initialHealth;

    private TweenEaseAnimationScaleComponent easeScaleAnimation;
}
